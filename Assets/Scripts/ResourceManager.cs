using UnityEngine;
using TMPro;

public class ResourceManager : MonoBehaviour
{
    public MechanicItem mechanicItem;

    public bool OwnsMechanicItem()
    {
        return mechanicItem != null && mechanicItem.purchased;
    }

    public int totalOutput;
    public int waterWheelOutput;
    public int income;
    public int availMoney;
    public int bikeOutput;

    public float disasterMultiplier = 1.0f;
    public float volatility;

    public GameObject waterWheelUpgradeButton;

    private Bike bike;
    private Disasters disaster;

    // Variables for tracking time and perSecond.
    private float elapsedTime = 0f;
    public float perSecond = 1f; // Update every 1 second

    // Start is called before the first frame update
    void Start()
    {
        // Initialize the values
        totalOutput = 1;
        income = 1;
        availMoney = 100;
        volatility = 0.0f;
        waterWheelOutput = 0;
        bikeOutput = 0;

        bike = GetComponent<Bike>();
        disaster = GetComponent<Disasters>();
    }

    // Update is called once per frame
    void Update()
    {
        incomeStarter();

    }

    public void SaveGame()
    {

        GameManager.SaveGame(this);
    }

    private void OnApplicationQuit()
    {
        // Save the game data when the application is about to quit
        SaveGame();
        Debug.Log("this should save.");
    }

    // This method should be called when the upgrade button is clicked.
    public void UpgradeButtonClicked()
    {
        bike.UpgradePowerGenerator();
    }

    public void ReduceVolatility()
    {
        // Reduce volatility by 50%
        volatility *= 0.5f;

    }

    public void incomeStarter()
    {
        // Update the elapsed time
        elapsedTime += Time.deltaTime;

        // Check if it's time to update the values (once per second)
        if (elapsedTime >= perSecond)
        {
            // For example, you can increase income by income int
            availMoney += income;

            if (availMoney >= 1000 && !waterWheelUpgradeButton.activeSelf) //if the players income is over 1000, button unhides.
            {
                waterWheelUpgradeButton.SetActive(true);
            }

            // Reset the elapsed time
            elapsedTime = 0f;
        }
    }
    public void BuyMechanicItem()
    {
        if (!OwnsMechanicItem() && availMoney >= disaster.mechanicItemCost)
        {
            mechanicItem.Buy(); // Buy the MechanicItem
            mechanicItem.uses = 1; // Set uses to 1 after purchase
                                   // Deduct the cost from the player's money
            availMoney -= disaster.mechanicItemCost;
            // Update your UI to reflect the purchase
            // Hide the buy button
        }
        else if (OwnsMechanicItem())
        {
            // Player already owns MechanicItem, allow rebuy if they have enough money
            if (availMoney >= disaster.mechanicItemCost)
            {
                mechanicItem.uses++; // Increase the uses
                                     // Deduct the cost from the player's money
                availMoney -= disaster.mechanicItemCost;
                // Update your UI to reflect the rebuy
            }
        }
    }



}
