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
        if (resourceManager.availMoney >= upgradeCost)
        {
            //powerOut = 1
            //income = 1
            //upgradeCost = 5; if you change them, these should be the games values
            bikeOutput ++; // increase the powerOutput.
            resourceManager.totalOutput++;
            resourceManager.bikeOutput = bikeOutput;
            income += 1;     // Double the income.
            resourceManager.income += 1;
            buttonClicked++;
            resourceManager.availMoney -= upgradeCost;
            upgradeCost *= 2; // upgrade cost for the next level.
            
            
        }
    }
}
