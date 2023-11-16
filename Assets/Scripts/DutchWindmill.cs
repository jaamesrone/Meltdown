using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DutchWindmill : MonoBehaviour
{
    public int dutchOutput = 0; // Initial power generation per second (twice as much as the original).
    public int buttonClicked = 0; //how many times you click the button
    public float upgradeCost = 2000;  // Initial upgrade cost.
    public int income = 0;        // Initial income per second.
    public GameObject dutchUI;
    
    private ResourceManager resourceManager;
    public TextMeshProUGUI dutchUpgradeCost;

    private void Start()
    {
        resourceManager = GetComponent<ResourceManager>();
    }

    private void Update()
    {
        if (dutchUpgradeCost != null)
            dutchUpgradeCost.text = "$" + upgradeCost;
    }

    public void resetProgress()
    {
        dutchOutput = 0;
        buttonClicked = 0;
        upgradeCost = 2000;
        income = 0;
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
        upgradeProgress();
                          
        if (resourceManager.volatility != 100.0f)
        {
            resourceManager.volatility += 0.6f; //0.6f
            while (resourceManager.volatility >= 100.1f)
            {
                resourceManager.volatility -= 0.1f;
            }
        }
    }

    public void upgradeProgress()
    {
        income = Mathf.FloorToInt(dutchOutput * resourceManager.disasterMultiplier);
        dutchOutput += 4;
        resourceManager.dutchOutput = dutchOutput;
        resourceManager.totalOutput += 4;
        income += 4;
        resourceManager.income += 4;
        buttonClicked++;
        resourceManager.Money -= upgradeCost;
        upgradeCost *= 1.5f; // upgrade cost for the next level
    }
}
