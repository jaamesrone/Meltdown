using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ElectricalWindmill : MonoBehaviour
{
    public int electricalOutput = 0; // Initial power generation per second (twice as much as the original).
    public int buttonClicked = 0; //how many times you click the button
    public int upgradeCost = 1;  // Initial upgrade cost.
    public int income = 0;        // Initial income per second.
    public GameObject electricalUI;

    private ResourceManager resourceManager;
    public TextMeshProUGUI electricalUpgradeCost;

    private void Start()
    {
        resourceManager = GetComponent<ResourceManager>();
    }

    void Update()
    {
        if (electricalUpgradeCost != null)
            electricalUpgradeCost.text = "$" + upgradeCost * 16;
    }

    public void resetProgress()
    {
        electricalOutput = 0;
        buttonClicked = 0;
        income = 0;
        upgradeCost = 1;
    }

    public void UpgradeElectricalGenerator()
    {
        if (electricalUI != null)
        {
            electricalUI.SetActive(true);
            if (buttonClicked >= 10)
            {
                return;
            }
            else
            {
                upgradeOutcomeElectrical();
            }
        }

    }

    public void upgradeOutcomeElectrical()
    {
        income = Mathf.FloorToInt(electricalOutput * resourceManager.disasterMultiplier);
        electricalOutput -= 1;
        resourceManager.electricalOutput = electricalOutput;
        resourceManager.totalOutput -= 1;
        income -= 1;
        resourceManager.income -= 1;
        buttonClicked++;
        resourceManager.Money -= upgradeCost;
        upgradeCost *= 2; // upgrade cost for the next level
        resourceManager.volatility -= 0.5f; //Lowers volatility by 0.5f
    }
}
