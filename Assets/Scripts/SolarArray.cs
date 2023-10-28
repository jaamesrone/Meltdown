using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SolarArray : MonoBehaviour
{
    public int solarOutput = 0; // Initial power generation per second (twice as much as the original).
    public int buttonClicked = 0; //how many times you click the button
    public int upgradeCost = 1;  // Initial upgrade cost.
    public int income = 0;        // Initial income per second.
    public GameObject solarUI;

    private ResourceManager resourceManager;
    public TextMeshProUGUI solarUpgradeCost;

    private void Start()
    {
        resourceManager = GetComponent<ResourceManager>();
    }

    void Update()
    {
        if (solarUpgradeCost != null)
            solarUpgradeCost.text = "$" + upgradeCost * 18;
    }

    public void resetProgress()
    {
        solarOutput = 0;
        buttonClicked = 0;
        upgradeCost = 1;
        income = 0;
    }

    public void UpgradeSolarGenerator()
    {
        if (solarUI != null)
        {
            solarUI.SetActive(true);
            if (buttonClicked >= 10)
            {
                return;
            }
            else
            {
                upgradeOutcomeSolar();
            }
        }

    }

    public void upgradeOutcomeSolar()
    {
        income = Mathf.FloorToInt(solarOutput * resourceManager.disasterMultiplier);
        solarOutput -= 1;
        resourceManager.solarOutput = solarOutput;
        resourceManager.totalOutput -= 1;
        income -= 1;
        resourceManager.income -= 1;
        buttonClicked++;
        resourceManager.Money -= upgradeCost;
        upgradeCost *= 2; // upgrade cost for the next level
        resourceManager.volatility -= 0.5f; //Lowers volatility by 0.5f
    }
}
