using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoolingSystem : MonoBehaviour
{
    public int coolingOutput = 0; // Initial power generation per second (twice as much as the original).
    public int buttonClicked = 0; //how many times you click the button
    public float upgradeCost = 3500;  // Initial upgrade cost.
    public int income = 0;        // Initial income per second.
    public GameObject coolingUI;

    private ResourceManager resourceManager;
    public TextMeshProUGUI coolingUpgradeCost;

    private void Start()
    {
        resourceManager = GetComponent<ResourceManager>();
    }

    void Update()
    {
        if (coolingUpgradeCost != null)
            coolingUpgradeCost.text = "$" + upgradeCost;
    }

    public void resetProgress()
    {
        coolingOutput = 0;
        buttonClicked = 0;
        upgradeCost = 3500;
        income = 0;
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
        resourceManager.coolingOutput = coolingOutput;
        resourceManager.totalOutput -= 1;
        income -= 1;
        resourceManager.income -= 1;
        buttonClicked++;
        resourceManager.Money -= upgradeCost;
        upgradeCost *= 1.5f; // upgrade cost for the next level
        resourceManager.volatility -= 0.5f; //Lowers volatility by 0.5f
    }
}
