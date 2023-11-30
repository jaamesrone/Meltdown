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
    public float upgradeCost = 3000;  // Initial upgrade cost.

    public TextMeshProUGUI coalUpgradeCost;

    public GameObject coalUI;
    
    private ResourceManager resourceManager;
    

    private void Start()
    {
        resourceManager = GetComponent<ResourceManager>();
    }

    private void Update()
    {
        if (coalUpgradeCost != null)
            coalUpgradeCost.text = "$" + upgradeCost;
    }

    public void resetProgress()
    {
        coalOutput = 0;
        buttonClicked = 0;
        upgradeCost = 3000;
        income = 0;
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
                upgradeOutcomeCoal();
            }
        }

    }

    public void upgradeOutcomeCoal()
    {
        upgradeProgress();

        if (resourceManager.volatility != 100.0f)
        {
            resourceManager.volatility += 1.0f; //1.0f
            while (resourceManager.volatility >= 100.1f)
            {
                resourceManager.volatility -= 0.1f;
            }
        }
    }

    public void upgradeProgress()
    {
        income = Mathf.FloorToInt(coalOutput * resourceManager.disasterMultiplier);
        coalOutput += 1;
        resourceManager.coalOutput = coalOutput;
        resourceManager.totalOutput += 1;
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
        float originalSize = coalUpgradeCost.fontSize;

        // Define the duration of the animation
        float animationDuration = 0.3f;

        // Define the number of steps
        int numSteps = 20; // Adjust this based on the smoothness you desire

        // Calculate the size increase per step
        float sizeIncreasePerStep = (textSizeIncreaseFactor * originalSize - originalSize) / numSteps;

        // Gradually increase the size
        for (int i = 0; i < numSteps; i++)
        {
            coalUpgradeCost.fontSize = Mathf.RoundToInt(originalSize + i * sizeIncreasePerStep);
            yield return new WaitForSeconds(animationDuration / numSteps);
        }

        // Ensure the final size is exactly the orginal size
        coalUpgradeCost.fontSize = Mathf.RoundToInt(textSizeIncreaseFactor * originalSize);

        // Wait for a short duration 
        yield return new WaitForSeconds(0.5f);

        // Decrease the size back to the original size
        for (int i = numSteps - 1; i >= 0; i--)
        {
            coalUpgradeCost.fontSize = Mathf.RoundToInt(originalSize + i * sizeIncreasePerStep);
            yield return new WaitForSeconds(animationDuration / numSteps);
        }

        // making the final size is exactly the original size
        coalUpgradeCost.fontSize = Mathf.RoundToInt(originalSize);
    }
}
