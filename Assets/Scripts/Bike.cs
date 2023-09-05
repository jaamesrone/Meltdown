using UnityEngine;

public class Bike : MonoBehaviour
{
    public int buttonClicked = 0;
    public int powerOutput = 1;   // Initial power generation per second.
    public int income = 1;        // Initial income per second.
    public int upgradeCost;  // Initial upgrade cost.

    private ResourceManager resourceManager;

    private void Start()
    {
        resourceManager = GetComponent<ResourceManager>();
        resourceManager.powerOutput = powerOutput;
        resourceManager.income += income;
    }

    public void UpgradePowerGenerator()
    {
        if (buttonClicked >= 10)
        {
            return;
        }
        if (resourceManager.availMoney >= upgradeCost)
        {
            //powerOut = 1
            //income = 2
            //upgradeCost = 5; if you change them, these should be the games values
            buttonClicked++;
            resourceManager.availMoney -= upgradeCost;
            powerOutput += 1; // increase the powerOutput.
            income *= 2;     // Double the income.
            upgradeCost *= 5; // upgrade cost for the next level.
            resourceManager.powerOutput = powerOutput;
            resourceManager.income = income;
            
        }
    }
}
