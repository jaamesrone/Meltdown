using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DutchWindmill : MonoBehaviour
{
    public int dutchOutput = 0; // Initial power generation per second (twice as much as the original).
    public int buttonClicked = 0; //how many times you click the button
    public int income = 0;        // Initial income per second.

    public float upgradeCost = 2000;  // Initial upgrade cost.
    public float textSizeIncreaseFactor = 1.5f; // Adjust the factor to control the size increase
    
    public GameObject dutchUI;

    public TextMeshProUGUI dutchUpgradeCost;

    private ResourceManager resourceManager;
    

    private void Start()
    {
        resourceManager = GetComponent<ResourceManager>();
    }

    private void Update()
    {
        if (dutchUpgradeCost != null)
            dutchUpgradeCost.text = "$" + upgradeCost;
    }

    public void resetProgress()
    {
        dutchOutput = 0;
        buttonClicked = 0;
        upgradeCost = 2000;
        income = 0;
    }

    public void UpgradeDutchGenerator()
    {
        if (dutchUI != null)
        {
            dutchUI.SetActive(true);
            if (buttonClicked >= 10)
            {
                return;
            }
            else
            {
                upgradeOutcomeDutch();
            }
        }

    }

    public void upgradeOutcomeDutch()
    {
        upgradeProgress();
                          
        if (resourceManager.volatility != 100.0f)
        {
            resourceManager.volatility += 0.6f; //0.6f
            while (resourceManager.volatility >= 100.1f)
            {
                resourceManager.volatility -= 0.1f;
            }
        }
    }

    public void upgradeProgress()
    {
        income = Mathf.FloorToInt(dutchOutput * resourceManager.disasterMultiplier);
        dutchOutput += 4;
        resourceManager.dutchOutput = dutchOutput;
        resourceManager.totalOutput += 4;
        income += 4;
        resourceManager.income += 4;
        buttonClicked++;
        resourceManager.Money -= upgradeCost;
        upgradeCost *= 1.5f; // upgrade cost for the next level
        StartCoroutine(AnimateTextSize());
    }

    IEnumerator AnimateTextSize()
    {
        // Get the initial size
        float originalSize = dutchUpgradeCost.fontSize;

        // Define the duration of the animation
        float animationDuration = 0.3f;

        // Define the number of steps
        int numSteps = 20; // Adjust this based on the smoothness you desire

        // Calculate the size increase per step
        float sizeIncreasePerStep = (textSizeIncreaseFactor * originalSize - originalSize) / numSteps;

        // Gradually increase the size
        for (int i = 0; i < numSteps; i++)
        {
            dutchUpgradeCost.fontSize = Mathf.RoundToInt(originalSize + i * sizeIncreasePerStep);
            yield return new WaitForSeconds(animationDuration / numSteps);
        }

        // Ensure the final size is exactly the orginal size
        dutchUpgradeCost.fontSize = Mathf.RoundToInt(textSizeIncreaseFactor * originalSize);

        // Wait for a short duration 
        yield return new WaitForSeconds(0.5f);

        // Decrease the size back to the original size
        for (int i = numSteps - 1; i >= 0; i--)
        {
            dutchUpgradeCost.fontSize = Mathf.RoundToInt(originalSize + i * sizeIncreasePerStep);
            yield return new WaitForSeconds(animationDuration / numSteps);
        }

        // making the final size is exactly the original size
        dutchUpgradeCost.fontSize = Mathf.RoundToInt(originalSize);
    }
}
