using UnityEngine;
using TMPro;
using System.Collections;

public class Bike : MonoBehaviour
{
    public int buttonClicked = 0;
    public int bikeOutput = 0;   // Initial power generation per second.
    public int income =0;        // Initial income per second.

    public float upgradeCost = 50;  // Initial upgrade cost.
    public float textSizeIncreaseFactor = 1.5f; // Adjust the factor to control the size increase

    private ResourceManager resourceManager;
    public TextMeshProUGUI bikeUpgradeCost;

    public GameObject bikeModel;

    void Start()
    {
        resourceManager = GetComponent<ResourceManager>();
    }

    void Update()
    {
        if (bikeUpgradeCost != null)
            bikeUpgradeCost.text = "$" + upgradeCost;
        if (buttonClicked > 0)
        {
            bikeModel.SetActive(true);
        }
    }

    public void resetProgress()
    {
        buttonClicked = 0;
        bikeOutput = 0;
        income = 0;
        upgradeCost = 50;
        bikeModel.SetActive(false);
    }

    public void UpgradePowerGenerator()
    {
        if (buttonClicked >= 10)
        {
            return;
        }
        if (resourceManager.Money >= upgradeCost)
        {
            if (buttonClicked <= 0)
            {
                bikeModel.SetActive(true);
            }
            upgradeOutcomeBike();
        }
    }

    public void upgradeOutcomeBike()
    {
        upgradeProgress();
        if (resourceManager.volatility != 100.0f)
        {
            resourceManager.volatility += 0.2f; //0.2f
            while (resourceManager.volatility >= 100.1f)
            {
                resourceManager.volatility -= 0.1f;
            }
        }
    }

    public void upgradeProgress()
    {
        income = Mathf.FloorToInt(bikeOutput * resourceManager.disasterMultiplier);
        bikeOutput++; // increase the powerOutput.
        resourceManager.totalOutput++;
        resourceManager.bikeOutput = bikeOutput;
        income += 1;     // Double the income.
        resourceManager.income += 1;
        buttonClicked++;
        resourceManager.Money -= upgradeCost;
        upgradeCost *= 1.5f; // upgrade cost for the next level.
        StartCoroutine(AnimateTextSize());
    }

    IEnumerator AnimateTextSize()
    {
        // Get the initial size
        float originalSize = bikeUpgradeCost.fontSize;

        // Define the duration of the animation
        float animationDuration = 0.3f;

        // Define the number of steps
        int numSteps = 20; // Adjust this based on the smoothness you desire

        // Calculate the size increase per step
        float sizeIncreasePerStep = (textSizeIncreaseFactor * originalSize - originalSize) / numSteps;

        // Gradually increase the size
        for (int i = 0; i < numSteps; i++)
        {
            bikeUpgradeCost.fontSize = Mathf.RoundToInt(originalSize + i * sizeIncreasePerStep);
            yield return new WaitForSeconds(animationDuration / numSteps);
        }

        // Ensure the final size is exactly the orginal size
        bikeUpgradeCost.fontSize = Mathf.RoundToInt(textSizeIncreaseFactor * originalSize);

        // Wait for a short duration 
        yield return new WaitForSeconds(0.5f);

        // Decrease the size back to the original size
        for (int i = numSteps - 1; i >= 0; i--)
        {
            bikeUpgradeCost.fontSize = Mathf.RoundToInt(originalSize + i * sizeIncreasePerStep);
            yield return new WaitForSeconds(animationDuration / numSteps);
        }

        // making the final size is exactly the original size
        bikeUpgradeCost.fontSize = Mathf.RoundToInt(originalSize);
    }

}
