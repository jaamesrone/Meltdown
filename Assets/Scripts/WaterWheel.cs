using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WaterWheel : MonoBehaviour
{
    public int waterWheelOutput = 0; // Initial power generation per second (twice as much as the original).
    public int buttonClicked = 0; //how many times you click the button
    public float upgradeCost = 1000f;  // Initial upgrade cost.
    public int income = 0;        // Initial income per second.
    public float textSizeIncreaseFactor = 1.5f; // Adjust the factor to control the size increase

    public GameObject waterWheelUI;

    private ResourceManager resourceManager;
    public TextMeshProUGUI waterUpgradeCost;

    private void Start()
    {
        resourceManager = GetComponent<ResourceManager>();
    }

    private void Update()
    {
        if (waterUpgradeCost != null)
            waterUpgradeCost.text = "$" + upgradeCost;
    }

    public void resetProgress()
    {
        waterWheelOutput = 0;
        buttonClicked = 0;
        income = 0;
        upgradeCost = 1000f;
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
                upgradeOutcomeWaterWheel();
            }
        }

    }

    public void upgradeOutcomeWaterWheel()
    {
        upgradeProgress();

        if (resourceManager.volatility != 100.0f)
        {
            resourceManager.volatility += 0.4f; //0.4f
            while (resourceManager.volatility >= 100.1f)
            {
                resourceManager.volatility -= 0.1f;
            }
        }
    }

    public void upgradeProgress()
    {
        income = Mathf.FloorToInt(waterWheelOutput * resourceManager.disasterMultiplier);
        waterWheelOutput += 2;
        resourceManager.waterWheelOutput = waterWheelOutput;
        resourceManager.totalOutput += 2;
        income += 1;
        resourceManager.income += 1;
        buttonClicked++;
        resourceManager.Money -= upgradeCost;
        upgradeCost *= 1.5f; // upgrade cost for the next level
        StartCoroutine(AnimateTextSize());
    }

    IEnumerator AnimateTextSize()
    {
        // Get the initial size
        float originalSize = waterUpgradeCost.fontSize;

        // Define the duration of the animation
        float animationDuration = 0.3f;

        // Define the number of steps
        int numSteps = 20; // Adjust this based on the smoothness you desire

        // Calculate the size increase per step
        float sizeIncreasePerStep = (textSizeIncreaseFactor * originalSize - originalSize) / numSteps;

        // Gradually increase the size
        for (int i = 0; i < numSteps; i++)
        {
            waterUpgradeCost.fontSize = Mathf.RoundToInt(originalSize + i * sizeIncreasePerStep);
            yield return new WaitForSeconds(animationDuration / numSteps);
        }

        // Ensure the final size is exactly the orginal size
        waterUpgradeCost.fontSize = Mathf.RoundToInt(textSizeIncreaseFactor * originalSize);

        // Wait for a short duration 
        yield return new WaitForSeconds(0.5f);

        // Decrease the size back to the original size
        for (int i = numSteps - 1; i >= 0; i--)
        {
            waterUpgradeCost.fontSize = Mathf.RoundToInt(originalSize + i * sizeIncreasePerStep);
            yield return new WaitForSeconds(animationDuration / numSteps);
        }

        // making the final size is exactly the original size
        waterUpgradeCost.fontSize = Mathf.RoundToInt(originalSize);
    }
}
