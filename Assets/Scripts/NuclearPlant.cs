using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NuclearPlant : MonoBehaviour
{
    public int nuclearOutput = 0; // Initial power generation per second (twice as much as the original).
    public int buttonClicked = 0; //how many times you click the button
    public float upgradeCost = 6000;  // Initial upgrade cost.
    public int income = 0;        // Initial income per second.
    public GameObject nuclearUI;

    private ResourceManager resourceManager;
    public TextMeshProUGUI nuclearUpgradeCost;

    private void Start()
    {
        resourceManager = GetComponent<ResourceManager>();
    }

    void Update()
    {
        if (nuclearUpgradeCost != null)
            nuclearUpgradeCost.text = "$" + upgradeCost;
    }

    public void resetProgress()
    {
        nuclearOutput = 0;
        buttonClicked = 0;
        upgradeCost = 6000;
        income = 0;
    }

    public void UpgradeNuclearGenerator()
    {
        if (nuclearUI != null)
        {
            nuclearUI.SetActive(true);
            if (buttonClicked >= 10)
            {
                return;
            }
            else
            {
                upgradeOutcomeNuclear();
            }
        }

    }

    public void upgradeOutcomeNuclear()
    {
        income = Mathf.FloorToInt(nuclearOutput * resourceManager.disasterMultiplier);
        nuclearOutput -= 1;
        resourceManager.nuclearOutput = nuclearOutput;
        resourceManager.totalOutput -= 1;
        income -= 1;
        resourceManager.income -= 1;
        buttonClicked++;
        resourceManager.Money -= upgradeCost;
        upgradeCost *= 1.5f; // upgrade cost for the next level
        resourceManager.volatility += 2; //Increases volatility by 2
    }
}
