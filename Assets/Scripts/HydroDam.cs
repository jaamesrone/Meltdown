using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HydroDam : MonoBehaviour
{
    public int hydroOutput = 0; // Initial power generation per second (twice as much as the original).
    public int buttonClicked = 0; //how many times you click the button
    public int income = 0;        // Initial income per second.

    public float upgradeCost = 300;  // Initial upgrade cost.
    public float textSizeIncreaseFactor = 1.5f; // Adjust the factor to control the size increase

    public bool isPurchasedHydroElectricDam = false;

    public GameObject hydroUI;
    public TextMeshProUGUI hydroUpgradeCost;

    private ResourceManager resourceManager;

    public GameObject hydroModel;

    private void Start()
    {
        resourceManager = GetComponent<ResourceManager>();
    }

    void Update()
    {
        if (hydroUpgradeCost != null)
            hydroUpgradeCost.text = "$" + upgradeCost;
        if (buttonClicked > 0)
        {
            hydroModel.SetActive(true);
        }
    }

    public void resetProgress()
    {
        resourceManager.totalOutput -= hydroOutput;
        resourceManager.volatility -= 2.5f * buttonClicked;
        buttonClicked = 0;
        hydroOutput = 0;
        resourceManager.hydroOutput = hydroOutput;
        hydroOutput = 0;
        buttonClicked = 0;
        income = 0;
        upgradeCost = 300;
        hydroModel.SetActive(false);
    }
    public void UpgradeHydroGenerator()
    {
        if (hydroUI != null)
        {
            hydroUI.SetActive(true);
            if (buttonClicked >= 10)
            {
                return;
            }
            else
            {
                if (buttonClicked <= 0)
                {
                    hydroModel.SetActive(true);
                }
                upgradeOutcomeHydro();
            }
        }

    }

    public void upgradeOutcomeHydro()
    {
        if(!isPurchasedHydroElectricDam)
        {
            isPurchasedHydroElectricDam = true;
        }
        else
        {
            return;
        }
        income = Mathf.FloorToInt(hydroOutput * resourceManager.disasterMultiplier);
        hydroOutput += 50;
        resourceManager.hydroOutput = hydroOutput;
        resourceManager.totalOutput += 50;
        income += 50;
        resourceManager.income += 50;
        buttonClicked++;
        resourceManager.Money -= upgradeCost;
        upgradeCost *= 2f; // upgrade cost for the next level
        if (resourceManager.volatility != 100.0f)
        {
            resourceManager.volatility += 2.5f; //2.5f
            while (resourceManager.volatility >= 100.1f)
            {
                resourceManager.volatility -= 0.1f;
            }
        };
        StartCoroutine(AnimateTextSize());
        Debug.Log("condition: " + isPurchasedHydroElectricDam);
    }

    private bool popUpOn = false;

    IEnumerator AnimateTextSize()
    {
        if (!popUpOn)
        {
            popUpOn = true; // Animation begins

            float originalSize = hydroUpgradeCost.fontSize;
            float targetSize = originalSize * 2;
            float animationTime = 0.5f; // Total animation time in seconds

            float elapsedTime = 0f;

            while (elapsedTime < animationTime)
            {
                float newSize = Mathf.Lerp(originalSize, targetSize, elapsedTime / animationTime);
                hydroUpgradeCost.fontSize = (int)newSize;

                elapsedTime += Time.deltaTime;
                yield return null; // Wait for the next frame
            }

            // Ensure the final size is exactly the target size
            hydroUpgradeCost.fontSize = (int)targetSize;

            // Pause for a short duration before reverting back
            yield return new WaitForSeconds(0.5f);

            elapsedTime = 0f;

            while (elapsedTime < animationTime)
            {
                float newSize = Mathf.Lerp(targetSize, originalSize, elapsedTime / animationTime);
                hydroUpgradeCost.fontSize = (int)newSize;

                elapsedTime += Time.deltaTime;
                yield return null; // Wait for the next frame
            }

            // Ensure the final size is exactly the original size
            hydroUpgradeCost.fontSize = (int)originalSize;

            popUpOn = false; // Animation ends
        }
    }
}