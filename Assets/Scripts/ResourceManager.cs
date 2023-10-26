using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System;

public class ResourceManager : MonoBehaviour
{

    public bool OwnsMechanicItem()
    {
        return isPurchaseMechanicItem;
    }

    public bool OwnsPowerBreaker()
    {
        return isPurchasePowerBreaker;
    }

    public AudioSource buttonClicked;
    public int totalOutput;
    public int waterWheelOutput;
    public int income;
    public int bikeOutput;
    public int dutchOutput;
    public int coalOutput;
    public int coolingOutput;
    public int hydroOutput;
    public int electricalOutput;
    public int solarOutput;
    public int insuranceCost = 100;
    public int PowerBreakerCost = 100;
    public int LunchRoomCost = 500;

    public float availMoney;
    public float disasterMultiplier = 1.0f;
    public float volatility;
    public float DurationPowerBreakerVisibility = 5.0f; // Duration of the power increase in seconds

    public GameObject InsuranceButton; 
    public GameObject VolatilityGO;
    public GameObject UpgradeWaterButton;
    public GameObject UpgradeBikeButton;
    public GameObject UpgradeDutchButton;
    public GameObject UpgradeCoalButton;
    public GameObject UpgradeCoolingButton;
    public GameObject UpgradeHydroButton;
    public GameObject UpgradeElectricalButton;
    public GameObject UpgradeSolarButton;
    public GameObject ShopGO;
    public GameObject TotalOutputGO;
    public GameObject bikeOutputGO;
    public GameObject waterOutputGO;
    public GameObject dutchOutputGO;
    public GameObject coalOutputGO;
    public GameObject coolingOutputGO;
    public GameObject hydroOutputGO;
    public GameObject electricalOutputGO;
    public GameObject solarOutputGO;
    public GameObject PowerBreakerButton;
    public GameObject MechanicButton;
    public GameObject LunchRoomButton;
    public GameObject waterWheelUpgradeButton;
    public GameObject dutchUpgradeButton;
    public GameObject coalUpgradeButton;
    public GameObject coolingUpgradeButton;
    public GameObject hydroUpgradeButton;
    public GameObject electricalUpgradeButton;
    public GameObject solarUpgradeButton;
    public GameObject ShopPanel;
    public GameObject TutorialPanel;
    public GameObject tutorialGO;
    public GameObject UpgradeWaterCost;
    public GameObject UpgradeBikeCost;
    public GameObject UpgradeDutchCost;
    public GameObject UpgradeCoalCost;
    public GameObject UpgradeCoolingCost;
    public GameObject UpgradeHydroCost;
    public GameObject UpgradeElectricalCost;
    public GameObject UpgradeSolarCost;
    public GameObject windMillCost;
    public GameObject SolarCost;

    public bool backUpGeneratorBought = false;

    private bool insuranceItemBought = false;
    private bool isPowerBreakerActive = false;
    private bool isRandomEventHappening = false;
    

    private float powerBreakerEndTime = 0.0f;
    private float randomEventDuration = 5f; //5 sec for test
    private float randomEventTimer = 0f;

    private Bike bike;
    private Disasters disaster;
    private LunchRoom lunchRoom;
    public BackupGenerator backupGenerator;

    // Variables for tracking time and perSecond.
    private float elapsedTime = 0f;
    public float perSecond = 1f; // Update every 1 second

    private bool isPurchaseMechanicItem = false;
    private bool isPurchasePowerBreaker = false;

    // Start is called before the first frame update
    void Start()
    {
        // Initialize the values
        totalOutput = 1;
        income = 1;
        availMoney = 100.00f;
        volatility = 0.0f;
        waterWheelOutput = 0;
        dutchOutput = 0;
        bikeOutput = 0;
        coalOutput = 0;
        coolingOutput = 0;
        hydroOutput = 0;
        electricalOutput = 0;
        solarOutput = 0;
        DurationPowerBreakerVisibility = 90;
        bike = GetComponent<Bike>();
        disaster = GetComponent<Disasters>();
        InvokeRepeating("CheckRandomEvent", 1, 3f);
        //lunchRoom = GetComponent<LunchRoom>();
    }

    // Update is called once per frame
    void Update()
    {
        incomeStarter();
    }

    public float Money
    {
        set { availMoney = value; }
        get { return availMoney; }
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
        buttonClicked.Play();
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
            // Fluctuates income for added realism
            float randomValue = UnityEngine.Random.Range(-0.5f, 0.5f);
            // For example, you can increase income by income int
            Money += (income + randomValue);
            if (insuranceItemBought)
            {
                Money += (income + volatility + 0.1f * volatility);
            }
            if (availMoney >= 1000 && !waterWheelUpgradeButton.activeSelf) //if the players income is over 1000, button unhides.
            {
                waterWheelUpgradeButton.SetActive(true);
                waterOutputGO.SetActive(true);
                UpgradeWaterCost.SetActive(true);
            }

            if (availMoney >= 2000 && !dutchUpgradeButton.activeSelf) //if the players income is over 2000, button unhides.
            {
                dutchUpgradeButton.SetActive(true);
                dutchOutputGO.SetActive(true);
                UpgradeDutchCost.SetActive(true);
            }

            if (availMoney >= 3000 && !coalUpgradeButton.activeSelf) //if the players income is over 3000, button unhides.
            {
                coalUpgradeButton.SetActive(true);
                coalOutputGO.SetActive(true);
                UpgradeCoalCost.SetActive(true);
            }

            if (availMoney >= 3500 && !coolingUpgradeButton.activeSelf) //if the players income is over 3500, button unhides.
            {
                coolingUpgradeButton.SetActive(true);
                coolingOutputGO.SetActive(true);
                UpgradeCoolingCost.SetActive(true);
            }
            if (availMoney >= 4000 && !hydroUpgradeButton.activeSelf) //if the players income is over 4000, button unhides.
            {
                hydroUpgradeButton.SetActive(true);
                hydroOutputGO.SetActive(true);
                UpgradeHydroCost.SetActive(true);
            }
            if (availMoney >= 4500 && !electricalUpgradeButton.activeSelf) //if the players income is over 4500, button unhides.
            {
                electricalUpgradeButton.SetActive(true);
                electricalOutputGO.SetActive(true);
                UpgradeElectricalCost.SetActive(true);
            }
            if (availMoney >= 5000 && !solarUpgradeButton.activeSelf) //if the players income is over 5000, button unhides.
            {
                solarUpgradeButton.SetActive(true);
                solarOutputGO.SetActive(true);
                UpgradeSolarCost.SetActive(true);
            }
            else
            {
                Money += (income + randomValue);
            }

            Money = (float)Math.Round(Money, 2);

            // Reset the elapsed time
            elapsedTime = 0f;
        }
    }

    public void MechanicButtonVisibile()
    {
        MechanicButton.SetActive(!MechanicButton.activeSelf);
    }

    public void InsuranceItemVisible()
    {
        InsuranceButton.SetActive(!InsuranceButton.activeSelf);
    }

    public void LunchRoomButtonVisibile()
    {
        LunchRoomButton.SetActive(!LunchRoomButton.activeSelf);
    }

    public void PowerBreakerButtonVisibile()
    {
        PowerBreakerButton.SetActive(!PowerBreakerButton.activeSelf); 
    }

    public void BuyInsuranceItem()
    {
        if (availMoney >= insuranceCost)
        {
            insuranceItemBought = true;
            availMoney -= insuranceCost;
            InsuranceItemVisible();
            Debug.Log("bought");
        }
        else
        {
            insuranceItemBought = false;
        }
    }
    public void BuyAndUseMechanicItem(bool isBuy)
    {
        if (isBuy&& availMoney >= disaster.mechanicItemCost)   
        {
            isPurchaseMechanicItem = true;
            availMoney -= disaster.mechanicItemCost;
            MechanicButtonVisibile();
          
        }
        else
        {
            isPurchaseMechanicItem = false;
        }
    }

    public void BuyAndUsePowerBreaker(bool isBuy)
    {
        if (isBuy && availMoney >= PowerBreakerCost && !isPowerBreakerActive)
        {
            isPurchaseMechanicItem = true;
            availMoney -= PowerBreakerCost;
            PowerBreakerButtonVisibile();
            Invoke("PowerBreakerButtonVisibile", DurationPowerBreakerVisibility); //button reappears after duration ends.

            IncreaseTotalPowerOutput();
            Invoke("ResetPowerBreakerEffects", DurationPowerBreakerVisibility); //button reappears after duration ends.
        }
        else
        {
            isPurchaseMechanicItem = false;
        }
    }

    public void BuyLunchRoom(bool isBuy)
    {
        if (isBuy && availMoney >= LunchRoomCost && (lunchRoom.purchased == false))
        {
            lunchRoom.purchased = true;
            availMoney -= LunchRoomCost;
            LunchRoomButtonVisibile();
            //  Invoke("LunchRoomButtonVisibile", buttonVisibility);
        }
        else
        {
            lunchRoom.purchased = false;
        }
    }

    private void IncreaseTotalPowerOutput()
    {
        totalOutput *= 4; // Double the power output during the power breaker period
        income *= 4;
    }

    private void ResetPowerBreakerEffects()
    {
        totalOutput /= 4; // Restore the original power output
        income /= 4;
    }


    private void CheckRandomEvent()
    {
        Debug.Log("calling a randomEvent");
        randomEventTimer += Time.deltaTime;

        if (randomEventTimer >= randomEventDuration)
        {
            Debug.Log("greater");
            
            EndPowerSurgeEvent();
            Debug.Log("ended power surge");
            float randomEvent = UnityEngine.Random.Range(0.0f, 1.0f);
            if (randomEvent < 0.5f)
            {
                // Continue the current event (power surge)
                totalOutput *= 2;
                income *= 2;
            }
            else
            {
                availMoney *= 0.9f;
            }
        }
        else
        {
            
            if (UnityEngine.Random.Range(1, 101) <=1) //1% chance of it hitting
            {
                StartPowerSurgeEvent();
                Debug.Log("randomEvent started");
            }
        }

    }


    private void StartPowerSurgeEvent()
    {
        totalOutput *= 2;
        income *= 2;
        isRandomEventHappening = true;
        randomEventDuration = 5f; // 1 min event
    }

    private void EndPowerSurgeEvent()
    {
        totalOutput /= 2;
        income /= 2;
        isRandomEventHappening = false;
        randomEventTimer = 0f;
    }

    public void ShopScene()
    {
        VolatilityGO.gameObject.SetActive(!VolatilityGO.activeSelf);
        UpgradeWaterButton.SetActive(!UpgradeWaterButton.activeSelf);
        UpgradeBikeButton.gameObject.SetActive(!UpgradeBikeButton.activeSelf);
        UpgradeDutchButton.gameObject.SetActive(!UpgradeDutchButton.activeSelf);
        UpgradeCoalButton.gameObject.SetActive(!UpgradeCoalButton.activeSelf);
        UpgradeCoolingButton.gameObject.SetActive(!UpgradeCoolingButton.activeSelf);
        UpgradeHydroButton.gameObject.SetActive(!UpgradeHydroButton.activeSelf);
        UpgradeElectricalButton.gameObject.SetActive(!UpgradeElectricalButton.activeSelf);
        UpgradeSolarButton.gameObject.SetActive(!UpgradeSolarButton.activeSelf);
        ShopPanel.gameObject.SetActive(!ShopPanel.activeSelf);
        TotalOutputGO.gameObject.SetActive(!TotalOutputGO.activeSelf);
        waterOutputGO.gameObject.SetActive(!waterOutputGO.activeSelf);
        bikeOutputGO.gameObject.SetActive(!bikeOutputGO.activeSelf);
        dutchOutputGO.gameObject.SetActive(!dutchOutputGO.activeSelf);
        coalOutputGO.gameObject.SetActive(!coalOutputGO.activeSelf);
        coolingOutputGO.gameObject.SetActive(!coolingOutputGO.activeSelf);
        hydroOutputGO.gameObject.SetActive(!hydroOutputGO.activeSelf);
        electricalOutputGO.gameObject.SetActive(!electricalOutputGO.activeSelf);
        solarOutputGO.gameObject.SetActive(!solarOutputGO.activeSelf);
        UpgradeBikeCost.gameObject.SetActive(!UpgradeBikeCost.activeSelf);
        UpgradeWaterCost.gameObject.SetActive(!UpgradeWaterCost.activeSelf);
        UpgradeDutchCost.gameObject.SetActive(!UpgradeDutchCost.activeSelf);
        windMillCost.gameObject.SetActive(!windMillCost.activeSelf);
        SolarCost.gameObject.SetActive(!SolarCost.activeSelf);
        tutorialGO.gameObject.SetActive(!tutorialGO.activeSelf);
        ShopGO.gameObject.SetActive(!ShopGO.activeSelf);
    }

    public void TutorialScene()
    {
        VolatilityGO.gameObject.SetActive(!VolatilityGO.activeSelf);
        UpgradeWaterButton.SetActive(!UpgradeWaterButton.activeSelf);
        UpgradeBikeButton.gameObject.SetActive(!UpgradeBikeButton.activeSelf);
        UpgradeDutchButton.gameObject.SetActive(!UpgradeDutchButton.activeSelf);
        UpgradeCoalButton.gameObject.SetActive(!UpgradeCoalButton.activeSelf);
        UpgradeCoolingButton.gameObject.SetActive(!UpgradeCoolingButton.activeSelf);
        UpgradeHydroButton.gameObject.SetActive(!UpgradeHydroButton.activeSelf);
        UpgradeElectricalButton.gameObject.SetActive(!UpgradeElectricalButton.activeSelf);
        UpgradeSolarButton.gameObject.SetActive(!UpgradeSolarButton.activeSelf);
        TutorialPanel.gameObject.SetActive(!TutorialPanel.activeSelf);
        TotalOutputGO.gameObject.SetActive(!TotalOutputGO.activeSelf);
        waterOutputGO.gameObject.SetActive(!waterOutputGO.activeSelf);
        bikeOutputGO.gameObject.SetActive(!bikeOutputGO.activeSelf);
        dutchOutputGO.gameObject.SetActive(!dutchOutputGO.activeSelf);
        coalOutputGO.gameObject.SetActive(!coalOutputGO.activeSelf);
        coolingOutputGO.gameObject.SetActive(!coolingOutputGO.activeSelf);
        hydroOutputGO.gameObject.SetActive(!hydroOutputGO.activeSelf);
        electricalOutputGO.gameObject.SetActive(!electricalOutputGO.activeSelf);
        solarOutputGO.gameObject.SetActive(!solarOutputGO.activeSelf);
        ShopGO.gameObject.SetActive(!ShopGO.activeSelf);
    }
}
;