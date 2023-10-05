using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoolingSystem : MonoBehaviour
{
    public int coolingOutput = 0; // Initial power generation per second (twice as much as the original).
    public int buttonClicked = 0; //how many times you click the button
    public int upgradeCost = 1;  // Initial upgrade cost.
    public int income = 0;        // Initial income per second.
    public GameObject coolingUI;
    
    private ResourceManager resourceManager;

    private void Start()
    {
        resourceManager = GetComponent<ResourceManager>();
    }

    public void UpgradeCoolingGenerator()
    {
        if (coolingUI != null)
        {
            coolingUI.SetActive(true);
            if (buttonClicked >= 10)
            {
                return;
            }
            else
            {
                upgradeOutcomeCooling();
            }
        }

    }

    public void upgradeOutcomeCooling()
    {
        income = Mathf.FloorToInt(coolingOutput * resourceManager.disasterMultiplier);
        coolingOutput -= 1;
        resourceManager.waterWheelOutput = coolingOutput;
        resourceManager.totalOutput -= 1;
        income -= 1;
        resourceManager.income -= 1;
        buttonClicked++;
        resourceManager.Money -= upgradeCost;
        upgradeCost *= 2; // upgrade cost for the next level
        resourceManager.volatility -= 0.5f; //Lowers volatility by 0.5f
    }
}
