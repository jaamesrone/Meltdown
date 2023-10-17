using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoalPowerPlant : MonoBehaviour
{
    public int coalOutput = 0; // Initial power generation per second (twice as much as the original).
    public int buttonClicked = 0; //how many times you click the button
    public int upgradeCost = 1;  // Initial upgrade cost.
    public int income = 0;        // Initial income per second.
    public GameObject coalUI;
    
    private ResourceManager resourceManager;
    public TextMeshProUGUI coalUpgradeCost;

    private void Start()
    {
        resourceManager = GetComponent<ResourceManager>();
    }

    private void Update()
    {
        if (coalUpgradeCost != null)
            coalUpgradeCost.text = "$" + upgradeCost;
    }

    public void UpgradeCoalGenerator()
    {
        if (coalUI != null)
        {
            coalUI.SetActive(true);
            if (buttonClicked >= 10)
            {
                return;
            }
            else
            {
                upgradeOutcomeCoal();
            }
        }

    }

    public void upgradeOutcomeCoal()
    {
        income = Mathf.FloorToInt(coalOutput * resourceManager.disasterMultiplier);
        coalOutput += 1;
        resourceManager.waterWheelOutput = coalOutput;
        resourceManager.totalOutput += 1;
        income += 1;
        resourceManager.income += 1;
        buttonClicked++;
        resourceManager.Money -= upgradeCost;
        upgradeCost *= 2; // upgrade cost for the next level

                          //Increase volatility by 1
        if (resourceManager.volatility != 100.0f)
        {
            resourceManager.volatility += 1.0f; //1.0f
            while (resourceManager.volatility >= 100.1f)
            {
                resourceManager.volatility -= 0.1f;
            }
        }
    }
}
