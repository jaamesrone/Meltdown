using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterWheel : MonoBehaviour
{
    public int waterWheelOutput = 0; // Initial power generation per second (twice as much as the original).
    public int buttonClicked = 0; //how many times you click the button
    public int upgradeCost = 1;  // Initial upgrade cost.
    public int income = 0;        // Initial income per second.
    public GameObject waterWheelUI;

    private ResourceManager resourceManager;

    private void Start()
    {
        resourceManager = GetComponent<ResourceManager>();
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
            if (resourceManager.availMoney >= upgradeCost)
            {
                waterWheelOutput += 2;
                resourceManager.waterWheelOutput = waterWheelOutput;
                resourceManager.totalOutput += 2;
                income += 1;
                resourceManager.income += 1;
                buttonClicked++;
                resourceManager.availMoney -= upgradeCost;
                upgradeCost *= 2; // upgrade cost for the next level
            }
        }
    }
}
