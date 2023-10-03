using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DutchWindmill : MonoBehaviour
{
    public int dutchOutput = 0; // Initial power generation per second (twice as much as the original).
    public int buttonClicked = 0; //how many times you click the button
    public int upgradeCost = 1;  // Initial upgrade cost.
    public int income = 0;        // Initial income per second.
    public GameObject dutchUI;
    
    private ResourceManager resourceManager;

    private void Start()
    {
        resourceManager = GetComponent<ResourceManager>();
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
        income = Mathf.FloorToInt(dutchOutput * resourceManager.disasterMultiplier);
        dutchOutput += 4;
        resourceManager.waterWheelOutput = dutchOutput;
        resourceManager.totalOutput += 4;
        income += 4;
        resourceManager.income += 4;
        buttonClicked++;
        resourceManager.Money -= upgradeCost;
        upgradeCost *= 2; // upgrade cost for the next level

                          //Increase volatility by 0.6
        if (resourceManager.volatility != 100.0f)
        {
            resourceManager.volatility += 0.6f; //0.6f
            while (resourceManager.volatility >= 100.1f)
            {
                resourceManager.volatility -= 0.1f;
            }
        }
    }
}
