using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WaterWheel : MonoBehaviour
{
    public int waterWheelOutput = 0; // Initial power generation per second (twice as much as the original).
    public int buttonClicked = 0; //how many times you click the button
    public float upgradeCost = 75f;  // Initial upgrade cost.
    public int income = 0;        // Initial income per second.
    public float textSizeIncreaseFactor = 1.5f; // Adjust the factor to control the size increase

    public GameObject waterWheelUI;

    private ResourceManager resourceManager;
    public TextMeshProUGUI waterUpgradeCost;

    public GameObject waterModel;

    private bool popUpOn = false;

    private void Start()
    {
        resourceManager = GetComponent<ResourceManager>();
    }

    private void Update()
    {
        if (waterUpgradeCost != null)
            waterUpgradeCost.text = "$" + upgradeCost;
        if (buttonClicked > 0)
        {
            waterModel.SetActive(true);
        }
    }

    public void resetProgress()
    {
        resourceManager.totalOutput -= waterWheelOutput;
        resourceManager.volatility -= 0.5f * buttonClicked;
        buttonClicked = 0;
        waterWheelOutput = 0;
        resourceManager.waterWheelOutput = waterWheelOutput;
        waterWheelOutput = 0;
        buttonClicked = 0;
        income = 0;
        upgradeCost = 75f;
        waterModel.SetActive(false);
    }

    public void UpgradeWaterGenerator()
    {
        if (waterWheelUI != null)
        {
            waterWheelUI.SetActive(true);
            if (buttonClicked >= 10)
            {
                return;
            }
            else
            {
                if (buttonClicked <= 0)
                {
                    waterModel.SetActive(true);
                }
                upgradeOutcomeWaterWheel();
            }
        }

    }

    public void upgradeOutcomeWaterWheel()
    {
        upgradeProgress();

        if (resourceManager.volatility != 100.0f)
        {
            resourceManager.volatility += 0.5f; //0.5f
            while (resourceManager.volatility >= 100.1f)
            {
                resourceManager.volatility -= 0.1f;
            }
        }
    }

    public void upgradeProgress()
    {
        income = Mathf.FloorToInt(waterWheelOutput * resourceManager.disasterMultiplier);
        waterWheelOutput += 5;
        resourceManager.waterWheelOutput = waterWheelOutput;
        resourceManager.totalOutput += 5;
        income += 5;
        resourceManager.income += 5;
        buttonClicked++;
        resourceManager.Money -= upgradeCost;
        upgradeCost *= 1.5f; // upgrade cost for the next level
        StartCoroutine(AnimateTextSize());
    }

    IEnumerator AnimateTextSize()
    {
        if (!popUpOn)
        {
            popUpOn = true; // Animation begins

            float originalSize = waterUpgradeCost.fontSize;
            float targetSize = originalSize * 2;
            float animationTime = 0.5f; // Total animation time in seconds

            float elapsedTime = 0f;

            while (elapsedTime < animationTime)
            {
                float newSize = Mathf.Lerp(originalSize, targetSize, elapsedTime / animationTime);
                waterUpgradeCost.fontSize = (int)newSize;

                elapsedTime += Time.deltaTime;
                yield return null; // Wait for the next frame
            }

            // Ensure the final size is exactly the target size
            waterUpgradeCost.fontSize = (int)targetSize;

            // Pause for a short duration before reverting back
            yield return new WaitForSeconds(0.5f);

            elapsedTime = 0f;

            while (elapsedTime < animationTime)
            {
                float newSize = Mathf.Lerp(targetSize, originalSize, elapsedTime / animationTime);
                waterUpgradeCost.fontSize = (int)newSize;

                elapsedTime += Time.deltaTime;
                yield return null; // Wait for the next frame
            }

            // Ensure the final size is exactly the original size
            waterUpgradeCost.fontSize = (int)originalSize;

            popUpOn = false; // Animation ends
        }
    }
}
