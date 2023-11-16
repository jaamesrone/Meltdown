using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HydroDam : MonoBehaviour
{
    public int hydroOutput = 0; // Initial power generation per second (twice as much as the original).
    public int buttonClicked = 0; //how many times you click the button
    public float upgradeCost = 4000;  // Initial upgrade cost.
    public int income = 0;        // Initial income per second.
    public GameObject hydroUI;
    public bool isPurchasedHydroElectricDam = false;

    private ResourceManager resourceManager;
    public TextMeshProUGUI hydroUpgradeCost;

    private void Start()
    {
        resourceManager = GetComponent<ResourceManager>();
    }

    void Update()
    {
        if (hydroUpgradeCost != null)
            hydroUpgradeCost.text = "$" + upgradeCost;
    }

    public void resetProgress()
    {
        hydroOutput = 0;
        buttonClicked = 0;
        income = 0;
        upgradeCost = 4000;
    }
    public void UpgradeHydroGenerator()
    {
        if (hydroUI != null)
        {
            hydroUI.SetActive(true);
            if (buttonClicked >= 10)
            {
                return;
            }
            else
            {
                upgradeOutcomeHydro();
            }
        }

    }

    public void upgradeOutcomeHydro()
    {
        if(!isPurchasedHydroElectricDam)
        {
            isPurchasedHydroElectricDam = true;
        }
        else
        {
            return;
        }
        income = Mathf.FloorToInt(hydroOutput * resourceManager.disasterMultiplier);
        hydroOutput -= 1;
        resourceManager.hydroOutput = hydroOutput;
        resourceManager.totalOutput -= 1;
        income -= 1;
        resourceManager.income -= 1;
        buttonClicked++;
        resourceManager.Money -= upgradeCost;
        upgradeCost *= 1.5f; // upgrade cost for the next level
        resourceManager.volatility -= 0.5f; //Lowers volatility by 0.5f
        Debug.Log("condition: " + isPurchasedHydroElectricDam);
    }
}