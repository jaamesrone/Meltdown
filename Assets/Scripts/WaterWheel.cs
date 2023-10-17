using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WaterWheel : MonoBehaviour
{
    public int waterWheelOutput = 0; // Initial power generation per second (twice as much as the original).
    public int buttonClicked = 0; //how many times you click the button
    public int upgradeCost = 1;  // Initial upgrade cost.
    public int income = 0;        // Initial income per second.
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
        income = Mathf.FloorToInt(waterWheelOutput * resourceManager.disasterMultiplier);
        waterWheelOutput += 2;
        resourceManager.waterWheelOutput = waterWheelOutput;
        resourceManager.totalOutput += 2;
        income += 1;
        resourceManager.income += 1;
        buttonClicked++;
        resourceManager.Money -= upgradeCost;
        upgradeCost *= 2; // upgrade cost for the next level

                          //Increase volatility by 0.4
        if (resourceManager.volatility != 100.0f)
        {
            resourceManager.volatility += 0.4f; //0.4f
            while (resourceManager.volatility >= 100.1f)
            {
                resourceManager.volatility -= 0.1f;
            }
        }
    }
}
