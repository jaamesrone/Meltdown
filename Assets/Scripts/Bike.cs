using UnityEngine;

public class Bike : MonoBehaviour
{
    public int buttonClicked = 0;
    public int bikeOutput = 0;   // Initial power generation per second.
    public int income =0;        // Initial income per second.
    public int upgradeCost;  // Initial upgrade cost.

    private ResourceManager resourceManager;


    private void Start()
    {
        resourceManager = GetComponent<ResourceManager>();

    }

    public void UpgradePowerGenerator()
    {
        if (buttonClicked >= 10)
        {
            return;
        }
        if (resourceManager.Money >= upgradeCost)
        {
            upgradeOutcomeBike();
        }
    }

    public void upgradeOutcomeBike()
    {
        income = Mathf.FloorToInt(bikeOutput * resourceManager.disasterMultiplier);
        bikeOutput++; // increase the powerOutput.
        resourceManager.totalOutput++;
        resourceManager.bikeOutput = bikeOutput;
        income += 1;     // Double the income.
        resourceManager.income += 1;
        buttonClicked++;
        resourceManager.Money -= upgradeCost;
        upgradeCost *= 2; // upgrade cost for the next level.

                          //Increase volatility by 0.2
        if (resourceManager.volatility != 100.0f)
        {
            resourceManager.volatility += 0.2f; //0.2f
            while (resourceManager.volatility >= 100.1f)
            {
                resourceManager.volatility -= 0.1f;
            }
        }
    }
}
