using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ElectricalWindmill : MonoBehaviour
{
    public int electricalOutput = 0; // Initial power generation per second (twice as much as the original).
    public int buttonClicked = 0; //how many times you click the button
    public int income = 0;        // Initial income per second.

    public float upgradeCost = 400;  // Initial upgrade cost.
    public float textSizeIncreaseFactor = 1.5f; // Adjust the factor to control the size increase

    public GameObject electricalUI;
    public TextMeshProUGUI electricalUpgradeCost;

    private ResourceManager resourceManager;

    public GameObject electricModel;

    private void Start()
    {
        resourceManager = GetComponent<ResourceManager>();
    }

    void Update()
    {
        if (electricalUpgradeCost != null)
            electricalUpgradeCost.text = "$" + upgradeCost;
        if (buttonClicked > 0)
        {
            electricModel.SetActive(true);
        }
    }

    public void resetProgress()
    {
        resourceManager.totalOutput -= electricalOutput;
        resourceManager.volatility += 1f * buttonClicked;
        buttonClicked = 0;
        electricalOutput = 0;
        resourceManager.electricalOutput = electricalOutput;
        electricalOutput = 0;
        buttonClicked = 0;
        income = 0;
        upgradeCost = 400;
        electricModel.SetActive(false);
    }

    public void UpgradeElectricalGenerator()
    {
        if (electricalUI != null)
        {
            electricalUI.SetActive(true);
            if (buttonClicked >= 10)
            {
                return;
            }
            else
            {
                if (buttonClicked <= 0)
                {
                    electricModel.SetActive(true);
                }
                upgradeOutcomeElectrical();
            }
        }

    }

    public void upgradeOutcomeElectrical()
    {
        income = Mathf.FloorToInt(electricalOutput * resourceManager.disasterMultiplier);
        electricalOutput += 75;
        resourceManager.electricalOutput = electricalOutput;
        resourceManager.totalOutput += 75;
        income += 75;
        resourceManager.income += 75;
        buttonClicked++;
        resourceManager.Money -= upgradeCost;
        upgradeCost *= 2.5f; // upgrade cost for the next level
        resourceManager.volatility -= 1f; //Decreases volatility by 1f
        StartCoroutine(AnimateTextSize());
    }

    private bool popUpOn = false;

    IEnumerator AnimateTextSize()
    {
        if (!popUpOn)
        {
            popUpOn = true; // Animation begins

            float originalSize = electricalUpgradeCost.fontSize;
            float targetSize = originalSize * 2;
            float animationTime = 0.5f; // Total animation time in seconds

            float elapsedTime = 0f;

            while (elapsedTime < animationTime)
            {
                float newSize = Mathf.Lerp(originalSize, targetSize, elapsedTime / animationTime);
                electricalUpgradeCost.fontSize = (int)newSize;

                elapsedTime += Time.deltaTime;
                yield return null; // Wait for the next frame
            }

            // Ensure the final size is exactly the target size
            electricalUpgradeCost.fontSize = (int)targetSize;

            // Pause for a short duration before reverting back
            yield return new WaitForSeconds(0.5f);

            elapsedTime = 0f;

            while (elapsedTime < animationTime)
            {
                float newSize = Mathf.Lerp(targetSize, originalSize, elapsedTime / animationTime);
                electricalUpgradeCost.fontSize = (int)newSize;

                elapsedTime += Time.deltaTime;
                yield return null; // Wait for the next frame
            }

            // Ensure the final size is exactly the original size
            electricalUpgradeCost.fontSize = (int)originalSize;

            popUpOn = false; // Animation ends
        }
    }
}
