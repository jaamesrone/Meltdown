using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HydroDam : MonoBehaviour
{
    public int hydroOutput = 0; // Initial power generation per second (twice as much as the original).
    public int buttonClicked = 0; //how many times you click the button
    public int income = 0;        // Initial income per second.

    public float upgradeCost = 4000;  // Initial upgrade cost.
    public float textSizeIncreaseFactor = 1.5f; // Adjust the factor to control the size increase

    public bool isPurchasedHydroElectricDam = false;

    public GameObject hydroUI;
    public TextMeshProUGUI hydroUpgradeCost;

    private ResourceManager resourceManager;

    private void Start()
    {
        resourceManager = GetComponent<ResourceManager>();
    }

    void Update()
    {
        if (hydroUpgradeCost != null)
            hydroUpgradeCost.text = "$" + upgradeCost;
    }

    public void resetProgress()
    {
        hydroOutput = 0;
        buttonClicked = 0;
        income = 0;
        upgradeCost = 4000;
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
        hydroOutput -= 1;
        resourceManager.hydroOutput = hydroOutput;
        resourceManager.totalOutput -= 1;
        income -= 1;
        resourceManager.income -= 1;
        buttonClicked++;
        resourceManager.Money -= upgradeCost;
        upgradeCost *= 1.5f; // upgrade cost for the next level
        resourceManager.volatility -= 0.5f; //Lowers volatility by 0.5f
        StartCoroutine(AnimateTextSize());
        Debug.Log("condition: " + isPurchasedHydroElectricDam);
    }

    IEnumerator AnimateTextSize()
    {
        // Get the initial size
        float originalSize = hydroUpgradeCost.fontSize;

        // Define the duration of the animation
        float animationDuration = 0.3f;

        // Define the number of steps
        int numSteps = 20; // Adjust this based on the smoothness you desire

        // Calculate the size increase per step
        float sizeIncreasePerStep = (textSizeIncreaseFactor * originalSize - originalSize) / numSteps;

        // Gradually increase the size
        for (int i = 0; i < numSteps; i++)
        {
            hydroUpgradeCost.fontSize = Mathf.RoundToInt(originalSize + i * sizeIncreasePerStep);
            yield return new WaitForSeconds(animationDuration / numSteps);
        }

        // Ensure the final size is exactly the orginal size
        hydroUpgradeCost.fontSize = Mathf.RoundToInt(textSizeIncreaseFactor * originalSize);

        // Wait for a short duration 
        yield return new WaitForSeconds(0.5f);

        // Decrease the size back to the original size
        for (int i = numSteps - 1; i >= 0; i--)
        {
            hydroUpgradeCost.fontSize = Mathf.RoundToInt(originalSize + i * sizeIncreasePerStep);
            yield return new WaitForSeconds(animationDuration / numSteps);
        }

        // making the final size is exactly the original size
        hydroUpgradeCost.fontSize = Mathf.RoundToInt(originalSize);
    }
}