using UnityEngine;
using TMPro;

public class Bike : MonoBehaviour
{
    public int buttonClicked = 0;
    public int bikeOutput = 0;   // Initial power generation per second.
    public int income =0;        // Initial income per second.
    public float upgradeCost = 50;  // Initial upgrade cost.

    private ResourceManager resourceManager;
    public TextMeshProUGUI bikeUpgradeCost;

    void Start()
    {
        resourceManager = GetComponent<ResourceManager>();
    }

    void Update()
    {
        if (bikeUpgradeCost != null)
            bikeUpgradeCost.text = "$" + upgradeCost;
    }

    public void resetProgress()
    {
        buttonClicked = 0;
        bikeOutput = 0;
        income = 0;
        upgradeCost = 50;
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
    }
}
