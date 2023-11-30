using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ElectricalWindmill : MonoBehaviour
{
    public int electricalOutput = 0; // Initial power generation per second (twice as much as the original).
    public int buttonClicked = 0; //how many times you click the button
    public int income = 0;        // Initial income per second.

    public float upgradeCost = 4500;  // Initial upgrade cost.
    public float textSizeIncreaseFactor = 1.5f; // Adjust the factor to control the size increase

    public GameObject electricalUI;
    public TextMeshProUGUI electricalUpgradeCost;

    private ResourceManager resourceManager;

    private void Start()
    {
        resourceManager = GetComponent<ResourceManager>();
    }

    void Update()
    {
        if (electricalUpgradeCost != null)
            electricalUpgradeCost.text = "$" + upgradeCost;
    }

    public void resetProgress()
    {
        electricalOutput = 0;
        buttonClicked = 0;
        income = 0;
        upgradeCost = 4500;
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
                upgradeOutcomeElectrical();
            }
        }

    }

    public void upgradeOutcomeElectrical()
    {
        income = Mathf.FloorToInt(electricalOutput * resourceManager.disasterMultiplier);
        electricalOutput -= 1;
        resourceManager.electricalOutput = electricalOutput;
        resourceManager.totalOutput -= 1;
        income -= 1;
        resourceManager.income -= 1;
        buttonClicked++;
        resourceManager.Money -= upgradeCost;
        upgradeCost *= 1.5f; // upgrade cost for the next level
        resourceManager.volatility += 0.5f; //Increases volatility by 0.5f
        StartCoroutine(AnimateTextSize());
    }

    IEnumerator AnimateTextSize()
    {
        // Get the initial size
        float originalSize = electricalUpgradeCost.fontSize;

        // Define the duration of the animation
        float animationDuration = 0.3f;

        // Define the number of steps
        int numSteps = 20; // Adjust this based on the smoothness you desire

        // Calculate the size increase per step
        float sizeIncreasePerStep = (textSizeIncreaseFactor * originalSize - originalSize) / numSteps;

        // Gradually increase the size
        for (int i = 0; i < numSteps; i++)
        {
            electricalUpgradeCost.fontSize = Mathf.RoundToInt(originalSize + i * sizeIncreasePerStep);
            yield return new WaitForSeconds(animationDuration / numSteps);
        }

        // Ensure the final size is exactly the orginal size
        electricalUpgradeCost.fontSize = Mathf.RoundToInt(textSizeIncreaseFactor * originalSize);

        // Wait for a short duration 
        yield return new WaitForSeconds(0.5f);

        // Decrease the size back to the original size
        for (int i = numSteps - 1; i >= 0; i--)
        {
            electricalUpgradeCost.fontSize = Mathf.RoundToInt(originalSize + i * sizeIncreasePerStep);
            yield return new WaitForSeconds(animationDuration / numSteps);
        }

        // making the final size is exactly the original size
        electricalUpgradeCost.fontSize = Mathf.RoundToInt(originalSize);
    }
}
