using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoalPowerPlant : MonoBehaviour
{
    public int coalOutput = 0; // Initial power generation per second (twice as much as the original).
    public int buttonClicked = 0; //how many times you click the button
    public int income = 0;        // Initial income per second.

    public float textSizeIncreaseFactor = 1.5f; // Adjust the factor to control the size increase
    public float upgradeCost = 150;  // Initial upgrade cost.

    public TextMeshProUGUI coalUpgradeCost;

    public GameObject coalUI;
    
    private ResourceManager resourceManager;

    public GameObject coalModel;

    private bool popUpOn = false;

    private void Start()
    {
        resourceManager = GetComponent<ResourceManager>();
    }

    private void Update()
    {
        if (coalUpgradeCost != null)
            coalUpgradeCost.text = "$" + upgradeCost;
        if (buttonClicked > 0)
        {
            coalModel.SetActive(true);
        }
    }

    public void resetProgress()
    {
        resourceManager.totalOutput -= coalOutput;
        resourceManager.volatility -= 2f * buttonClicked;
        buttonClicked = 0;
        coalOutput = 0;
        resourceManager.coalOutput = coalOutput;
        coalOutput = 0;
        buttonClicked = 0;
        upgradeCost = 150;
        income = 0;
        coalModel.SetActive(false);
    }

    public void UpgradeCoalGenerator()
    {
        if (coalUI != null)
        {
            coalUI.SetActive(true);
            if (buttonClicked >= 10)
            {
                return;
            }
            else
            {
                if (buttonClicked <= 0)
                {
                    coalModel.SetActive(true);
                }
                upgradeOutcomeCoal();
            }
        }

    }

    public void upgradeOutcomeCoal()
    {
        upgradeProgress();

        if (resourceManager.volatility != 100.0f)
        {
            resourceManager.volatility += 2.0f; //2.0f
            while (resourceManager.volatility >= 100.1f)
            {
                resourceManager.volatility -= 0.1f;
            }
        }
    }

    public void upgradeProgress()
    {
        income = Mathf.FloorToInt(coalOutput * resourceManager.disasterMultiplier);
        coalOutput += 20;
        resourceManager.coalOutput = coalOutput;
        resourceManager.totalOutput += 20;
        income += 20;
        resourceManager.income += 20;
        buttonClicked++;
        resourceManager.Money -= upgradeCost;
        upgradeCost *= 2f; // upgrade cost for the next level
        StartCoroutine(AnimateTextSize());
    }

    IEnumerator AnimateTextSize()
    {
        if (!popUpOn)
        {
            popUpOn = true; // Animation begins

            float originalSize = coalUpgradeCost.fontSize;
            float targetSize = originalSize * 2;
            float animationTime = 0.5f; // Total animation time in seconds

            float elapsedTime = 0f;

            while (elapsedTime < animationTime)
            {
                float newSize = Mathf.Lerp(originalSize, targetSize, elapsedTime / animationTime);
                coalUpgradeCost.fontSize = (int)newSize;

                elapsedTime += Time.deltaTime;
                yield return null; // Wait for the next frame
            }

            // Ensure the final size is exactly the target size
            coalUpgradeCost.fontSize = (int)targetSize;

            // Pause for a short duration before reverting back
            yield return new WaitForSeconds(0.5f);

            elapsedTime = 0f;

            while (elapsedTime < animationTime)
            {
                float newSize = Mathf.Lerp(targetSize, originalSize, elapsedTime / animationTime);
                coalUpgradeCost.fontSize = (int)newSize;

                elapsedTime += Time.deltaTime;
                yield return null; // Wait for the next frame
            }

            // Ensure the final size is exactly the original size
            coalUpgradeCost.fontSize = (int)originalSize;

            popUpOn = false; // Animation ends
        }
    }
}
