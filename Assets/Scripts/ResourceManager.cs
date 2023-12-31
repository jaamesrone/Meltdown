using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System;
using System.Collections;

[Serializable]
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
    public int nuclearOutput;
    public int dysonOutput;
    public int holeOutput;
    public int hamsterOutput;
    public int insuranceCost = 100;
    public int PowerBreakerCost = 100;
    public int LunchRoomCost = 500;

    public float availMoney;
    public float disasterMultiplier = 1.0f;
    public float volatility;
    public float DurationPowerBreakerVisibility = 5.0f; // Duration of the power increase in seconds
    public float messageDuration = 3f;

    public bool backUpGeneratorBought = false;

    public GameObject InsuranceButton; 
    public GameObject UpgradeWaterButton;
    public GameObject UpgradeBikeButton;
    public GameObject UpgradeCoalButton;
    public GameObject ShopGO;
    public GameObject waterOutputGO;
    public GameObject dutchOutputGO;
    public GameObject coalOutputGO;
    public GameObject coolingOutputGO;
    public GameObject hydroOutputGO;
    public GameObject electricalOutputGO;
    public GameObject solarOutputGO;
    public GameObject nuclearOutputGO;
    public GameObject dysonOutputGO;
    public GameObject holeOutputGO;
    public GameObject hamsterOutputGO;
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
    public GameObject nuclearUpgradeButton;
    public GameObject dysonUpgradeButton;
    public GameObject holeUpgradeButton;
    public GameObject hamsterUpgradeButton;
    public GameObject ShopPanel;
    public GameObject TutorialPanel;
    public GameObject tutorialGO;
    public GameObject UpgradeWaterCost;
    public GameObject UpgradeDutchCost;
    public GameObject UpgradeCoalCost;
    public GameObject UpgradeCoolingCost;
    public GameObject UpgradeHydroCost;
    public GameObject UpgradeElectricalCost;
    public GameObject UpgradeSolarCost;
    public GameObject UpgradeNuclearCost;
    public GameObject UpgradeDysonCost;
    public GameObject UpgradeHoleCost;
    public GameObject UpgradeHamsterCost;
    public GameObject ScrollMenu;
    public GameObject TotalOutput;
    public GameObject AvailableMoney;
    public GameObject VolatilityUI;
    public GameObject GeneratorsProg;
    public GameObject SaveButton;
    public GameObject LoadButton;
    public GameObject ResetButton;
    public GameObject VerifyButton;

    public TextMeshProUGUI eventText; // Reference to your TextMeshProUGUI component
    public TextMeshProUGUI Data;

    private bool isEventTextVisible = false;
    private bool insuranceItemBought = false;
    private bool isPowerBreakerActive = false;
    private bool isRandomEventHappening = false;
    private bool isBlinkActive = false;

    private float blinkerTimer = 0f;
    private float blinkerInterval = 0.5f;
    private float powerBreakerEndTime = 0.0f;
    private float randomEventDuration = 180f; 
    private float randomEventTimer = 0f;
    private float thirdRandomEventTimer = 0f;
    private float thirdRandomEventDuratio = 180f;

    private Bike bike;
    private Disasters disaster;
    private LunchRoom lunchRoom;
    private CoolingSystem coolSystem;
    private CoalPowerPlant coal;
    private DutchWindmill dutch;
    private WaterWheel water;
    private HydroDam hydro;
    private SolarArray solar;
    private ElectricalWindmill electric;
    private NuclearPlant nuclear;
    private DysonSphere dyson;
    private BlackHoleHarvester hole;
    private DivineHamsterWheel hamster;

    public BackupGenerator backupGenerator;

    // Variables for tracking time and perSecond.
    private float elapsedTime = 0f;
    public float perSecond = 1f; // Update every 1 second

    private bool isThirdRandomEventHappening = false;
    private bool isPurchaseMechanicItem = false;
    private bool isPurchasePowerBreaker = false;

    // Start is called before the first frame update

    public void SavePlayerData()
    {
        PlayerPrefs.SetInt("totalOutput", totalOutput);
        PlayerPrefs.SetInt("waterWheelOutput", waterWheelOutput);
        PlayerPrefs.SetInt("income", income);
        PlayerPrefs.SetInt("bikeOutput", bikeOutput);
        PlayerPrefs.SetInt("dutchOutput", dutchOutput);
        PlayerPrefs.SetInt("coalOutput", coalOutput);
        PlayerPrefs.SetInt("coolingOutput", coolingOutput);
        PlayerPrefs.SetInt("hydroOutput", hydroOutput);
        PlayerPrefs.SetInt("electricalOutput", electricalOutput);
        PlayerPrefs.SetInt("solarOutput", solarOutput);
        PlayerPrefs.SetInt("nuclearOutput", nuclearOutput);
        PlayerPrefs.SetInt("dysonOutput", dysonOutput);
        PlayerPrefs.SetInt("holeOutput", holeOutput);
        PlayerPrefs.SetInt("hamsterOutput", hamsterOutput);

        PlayerPrefs.SetInt("bikeButton", bike.buttonClicked);
        PlayerPrefs.SetInt("waterButton", water.buttonClicked);
        PlayerPrefs.SetInt("dutchButton", dutch.buttonClicked);
        PlayerPrefs.SetInt("coalButton", coal.buttonClicked);
        PlayerPrefs.SetInt("coolingButton", coolSystem.buttonClicked);
        PlayerPrefs.SetInt("hydroButton", hydro.buttonClicked);
        PlayerPrefs.SetInt("electricalButton", electric.buttonClicked);
        PlayerPrefs.SetInt("solarButton", solar.buttonClicked);
        PlayerPrefs.SetInt("nuclearButton", nuclear.buttonClicked);
        PlayerPrefs.SetInt("dysonButton", dyson.buttonClicked);
        PlayerPrefs.SetInt("holeButton", hole.buttonClicked);
        PlayerPrefs.SetInt("hamsterButton", hamster.buttonClicked);

        PlayerPrefs.SetFloat("availMoney", availMoney);
        PlayerPrefs.SetFloat("volatility", volatility);

        PlayerPrefs.SetString("saveDateTime", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

        PlayerPrefs.Save();
        Debug.Log("Player data saved");

        StartCoroutine(ShowDataUI("Your Data Has Been Saved!"));
    }

    public void LoadPlayerData()
    {
        totalOutput = PlayerPrefs.GetInt("totalOutput", totalOutput);
        waterWheelOutput = PlayerPrefs.GetInt("waterWheelOutput", waterWheelOutput);
        income = PlayerPrefs.GetInt("income", income);
        bikeOutput = PlayerPrefs.GetInt("bikeOutput", bikeOutput);
        dutchOutput = PlayerPrefs.GetInt("dutchOutput", dutchOutput);
        coalOutput = PlayerPrefs.GetInt("coalOutput", coalOutput);
        coolingOutput = PlayerPrefs.GetInt("coolingOutput", coolingOutput);
        hydroOutput = PlayerPrefs.GetInt("hydroOutput", hydroOutput);
        electricalOutput = PlayerPrefs.GetInt("electricalOutput", electricalOutput);
        solarOutput = PlayerPrefs.GetInt("solarOutput", solarOutput);
        nuclearOutput = PlayerPrefs.GetInt("nuclearOutput", nuclearOutput);
        dysonOutput = PlayerPrefs.GetInt("dysonOutput", dysonOutput);
        holeOutput = PlayerPrefs.GetInt("holeOutput", holeOutput);
        hamsterOutput = PlayerPrefs.GetInt("hamsterOutput", hamsterOutput);

        bike.buttonClicked = PlayerPrefs.GetInt("bikeButton", bike.buttonClicked);
        water.buttonClicked = PlayerPrefs.GetInt("waterButton", water.buttonClicked);
        dutch.buttonClicked = PlayerPrefs.GetInt("dutchButton", dutch.buttonClicked);
        coal.buttonClicked = PlayerPrefs.GetInt("coalButton", coal.buttonClicked);
        coolSystem.buttonClicked = PlayerPrefs.GetInt("coolingButton", coolSystem.buttonClicked);
        hydro.buttonClicked = PlayerPrefs.GetInt("hydroButton", hydro.buttonClicked);
        electric.buttonClicked = PlayerPrefs.GetInt("electricalButton", electric.buttonClicked);
        solar.buttonClicked = PlayerPrefs.GetInt("solarButton", solar.buttonClicked);
        nuclear.buttonClicked = PlayerPrefs.GetInt("nuclearButton", nuclear.buttonClicked);
        dyson.buttonClicked = PlayerPrefs.GetInt("dysonButton", dyson.buttonClicked);
        hole.buttonClicked = PlayerPrefs.GetInt("holeButton", hole.buttonClicked);
        hamster.buttonClicked = PlayerPrefs.GetInt("hamsterButton", hamster.buttonClicked);

        availMoney = PlayerPrefs.GetFloat("availMoney", availMoney);
        volatility = PlayerPrefs.GetFloat("volatility", volatility);

        string savedDateTimeString = PlayerPrefs.GetString("saveDateTime", "");
        if (!string.IsNullOrEmpty(savedDateTimeString))
        {
            DateTime savedDateTime = DateTime.ParseExact(savedDateTimeString, "yyyy-MM-dd HH:mm:ss", null);
            TimeSpan timeDifference = DateTime.Now - savedDateTime;
            float moneyEarned = (float)(timeDifference.TotalSeconds * totalOutput);
            availMoney += moneyEarned;
        }

        Debug.Log("Player data loaded");

        StartCoroutine(ShowDataUI("Your Data Has Been Loaded!"));
    }

    private IEnumerator ShowDataUI(string message)
    {
        Data.text = message;

        // Display the message for a specific duration
        yield return new WaitForSeconds(messageDuration);

        // Hide the message
        Data.text = "";
    }

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
        nuclearOutput = 0;
        dysonOutput = 0;
        holeOutput = 0;
        hamsterOutput = 0;
        DurationPowerBreakerVisibility = 90;
        bike = GetComponent<Bike>();
        coolSystem = GetComponent<CoolingSystem>();
        coal = GetComponent<CoalPowerPlant>();
        dutch = GetComponent<DutchWindmill>();
        water = GetComponent<WaterWheel>();
        hydro = GetComponent<HydroDam>();
        solar = GetComponent<SolarArray>();
        electric = GetComponent<ElectricalWindmill>();
        nuclear = GetComponent<NuclearPlant>();
        dyson = GetComponent<DysonSphere>();
        hole = GetComponent<BlackHoleHarvester>();
        hamster = GetComponent<DivineHamsterWheel>();
        disaster = GetComponent<Disasters>();
        InvokeRepeating("CheckRandomEvent", 1, 3f);
        InvokeRepeating("CheckSecondRandomEvent", 1, 5f);
        InvokeRepeating("CheckRandomThirdEvent", 1, 7f);
    }

    // Update is called once per frame
    void Update()
    {
        incomeStarter();
        EndRandomEvent();
        EndThirdRandomEvent();
        BlinkerEffect();
    }

    public float Money
    {
        set { availMoney = value; }
        get { return availMoney; }
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
            if (availMoney >= 75 && !waterWheelUpgradeButton.activeSelf && (bikeOutput > 0)) //if the players income is over 75, button unhides.
            {
                waterWheelUpgradeButton.SetActive(true);
                waterOutputGO.SetActive(true);
                UpgradeWaterCost.SetActive(true);
            }

            if (availMoney >= 100 && !dutchUpgradeButton.activeSelf && (waterWheelOutput > 0)) //if the players income is over 100, button unhides.
            {
                dutchUpgradeButton.SetActive(true);
                dutchOutputGO.SetActive(true);
                UpgradeDutchCost.SetActive(true);
            }

            if (availMoney >= 150 && !coalUpgradeButton.activeSelf && (dutchOutput > 0)) //if the players income is over 150, button unhides.
            {
                coalUpgradeButton.SetActive(true);
                coalOutputGO.SetActive(true);
                UpgradeCoalCost.SetActive(true);
            }

            if (availMoney >= 200 && !coolingUpgradeButton.activeSelf && (coalOutput > 0)) //if the players income is over 200, button unhides.
            {
                coolingUpgradeButton.SetActive(true);
                coolingOutputGO.SetActive(true);
                UpgradeCoolingCost.SetActive(true);
            }
            if (availMoney >= 300 && !hydroUpgradeButton.activeSelf && (coalOutput > 0)) //if the players income is over 4000, button unhides.
            {
                hydroUpgradeButton.SetActive(true);
                hydroOutputGO.SetActive(true);
                UpgradeHydroCost.SetActive(true);
            }
            if (availMoney >= 400 && !electricalUpgradeButton.activeSelf && (hydroOutput > 0)) //if the players income is over 4500, button unhides.
            {
                electricalUpgradeButton.SetActive(true);
                electricalOutputGO.SetActive(true);
                UpgradeElectricalCost.SetActive(true);
            }
            if (availMoney >= 500 && !solarUpgradeButton.activeSelf && (electricalOutput > 0)) //if the players income is over 5000, button unhides.
            {
                solarUpgradeButton.SetActive(true);
                solarOutputGO.SetActive(true);
                UpgradeSolarCost.SetActive(true);
            }
            if (availMoney >= 600 && !nuclearUpgradeButton.activeSelf && (solarOutput > 0)) //if the players income is over 6000, button unhides.
            {
                nuclearUpgradeButton.SetActive(true);
                nuclearOutputGO.SetActive(true);
                UpgradeNuclearCost.SetActive(true);
            }
            if (availMoney >= 800 && !dysonUpgradeButton.activeSelf && (nuclearOutput > 0)) //if the players income is over 6000, button unhides.
            {
                dysonUpgradeButton.SetActive(true);
                dysonOutputGO.SetActive(true);
                UpgradeDysonCost.SetActive(true);
            }
            if (availMoney >= 1000 && !holeUpgradeButton.activeSelf && (dysonOutput > 0)) //if the players income is over 6000, button unhides.
            {
                holeUpgradeButton.SetActive(true);
                holeOutputGO.SetActive(true);
                UpgradeHoleCost.SetActive(true);
            }
            if (availMoney >= 2000 && !hamsterUpgradeButton.activeSelf && (holeOutput > 0)) //if the players income is over 6000, button unhides.
            {
                hamsterUpgradeButton.SetActive(true);
                hamsterOutputGO.SetActive(true);
                UpgradeHamsterCost.SetActive(true);
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

    
    private void EndRandomEvent()
    {
        if (isRandomEventHappening)
        {
            randomEventTimer += Time.deltaTime;

            if (randomEventTimer >= randomEventDuration)
            {
                EndPowerSurgeEvent();
                Debug.Log("ended power surge");
            }
        }
    }


    private void CheckRandomEvent()
    {
        Debug.Log("calling a firstrandomEvent");
        if (!isRandomEventHappening)
        {
            if (UnityEngine.Random.Range(1, 101) <= 1) // 1% chance of it hitting
            {
                StartPowerSurgeEvent();
                StartCoroutine(ShowEventText("Power Surge Event Started!", 5f));
                Debug.Log("first randomEvent started");
            }
        }
    }


    private void CheckSecondRandomEvent()
    {
        Debug.Log("calling secondRandomEvent");
        if (UnityEngine.Random.Range(1, 101) <= 2) //2% chance of hitting
        {
            int generatorChoice = UnityEngine.Random.Range(0, 7);

            // Chooses a number that is tied to a resetted generator progression
            switch (generatorChoice)
            {
                case 0:
                    bike.resetProgress();
                    StartCoroutine(ShowEventText("Generator Reset: Bike", 5f));
                    Debug.Log("bike gen got reseted");
                    break;
                case 1:
                    water.resetProgress();
                    StartCoroutine(ShowEventText("Generator Reset: Water", 5f));
                    Debug.Log("water gen got reseted");
                    break;
                case 2:
                    dutch.resetProgress();
                    StartCoroutine(ShowEventText("Generator Reset: Dutch", 5f));
                    Debug.Log("dutch gen got reseted");
                    break;
                case 3:
                    coal.resetProgress();
                    StartCoroutine(ShowEventText("Generator Reset: Coal", 5f));
                    Debug.Log("coal gen got reseted");
                    break;
                case 4:
                    coolSystem.resetProgress();
                    StartCoroutine(ShowEventText("Generator Reset: Cooling System", 5f));
                    Debug.Log("coolSystem gen got reseted");
                    break;
                case 5:
                    hydro.resetProgress();
                    StartCoroutine(ShowEventText("Generator Reset: Hydro", 5f));
                    Debug.Log("hydro gen got reseted");
                    break;
                case 6:
                    electric.resetProgress();
                    StartCoroutine(ShowEventText("Generator Reset: Electric", 5f));
                    Debug.Log("electric gen got reseted");
                    break;
                case 7:
                    solar.resetProgress();
                    StartCoroutine(ShowEventText("Generator Reset: Solar", 5f));
                    Debug.Log("solar gen got reseted");
                    break;
                case 8:
                    nuclear.resetProgress();
                    StartCoroutine(ShowEventText("Generator Reset: Nuclear", 5f));
                    Debug.Log("nuclear gen got reseted");
                    break;
                case 9:
                    dyson.resetProgress();
                    StartCoroutine(ShowEventText("Generator Reset: Dyson", 5f));
                    Debug.Log("dyson gen got reseted");
                    break;
                case 10:
                    hole.resetProgress();
                    StartCoroutine(ShowEventText("Generator Reset: Hole", 5f));
                    Debug.Log("hole gen got reseted");
                    break;
                default:
                    hamster.resetProgress();
                    StartCoroutine(ShowEventText("Generator Reset: Hamster", 5f));
                    Debug.Log("hamster gen got reseted");
                    break;
            }

        }
    }

    private void CheckRandomThirdEvent()
    {
        Debug.Log("calling thirdRandomEvent");
        if (UnityEngine.Random.Range(1, 101) <= 3 && !isThirdRandomEventHappening) //3% chance of hitting
        {
            CostOfUpgrades();
            isThirdRandomEventHappening = true;
            thirdRandomEventTimer = 0f;
            StartCoroutine(ShowEventText("Cost of Upgrades Increased!", 5f));
        }
    }

    private void BlinkerEffect()
    {
        if (isBlinkActive)
        {
            // Update the timer
            blinkerTimer += Time.deltaTime;

            // Toggle the visibility of eventText based on the timer and interval
            if (blinkerTimer >= blinkerInterval)
            {
                eventText.enabled = !eventText.enabled;
                blinkerTimer = 0f; // Reset the timer
            }
        }
        else
        {
            // If blinking is not active, keep eventText disabled
            eventText.enabled = false;
        }
    }

    private IEnumerator ShowEventText(string message, float duration)
    {
        // Enable blink effect during ShowEventText
        isBlinkActive = true;

        // Display the message
        eventText.text = message;

        // Enable eventText
        GeneratorsProg.gameObject.SetActive(false);
        eventText.enabled = true;

        // Wait for the specified duration
        yield return new WaitForSeconds(duration);

        // Disable eventText and turn off blink effect
        eventText.enabled = false;
        GeneratorsProg.gameObject.SetActive(true);
        isBlinkActive = false;
    }

    private void EndThirdRandomEvent()
    {
        if (isThirdRandomEventHappening)
        {
            thirdRandomEventTimer += Time.deltaTime;

            if (thirdRandomEventTimer >= thirdRandomEventDuratio)
            {
                EndRandomThirdEvent();
                isThirdRandomEventHappening = false;
            }
        }
    }

    private void EndRandomThirdEvent()
    {
        bike.upgradeCost /= 2;
        water.upgradeCost /= 3;
        dutch.upgradeCost /= 3;
        coal.upgradeCost /= 2;
        coolSystem.upgradeCost /= 3;
        hydro.upgradeCost /= 3;
        electric.upgradeCost /= 3;
        solar.upgradeCost /= 3;
        nuclear.upgradeCost /= 3;
        dyson.upgradeCost /= 3;
        hole.upgradeCost /= 3;
        hamster.upgradeCost /= 3;
    }

    private void CostOfUpgrades()
    {
        bike.upgradeCost *= 2;
        water.upgradeCost *= 3;
        dutch.upgradeCost *= 3;
        coal.upgradeCost *= 2;
        coolSystem.upgradeCost *= 3;
        hydro.upgradeCost *= 3;
        electric.upgradeCost *= 3;
        solar.upgradeCost *= 3;
        nuclear.upgradeCost *= 3;
        dyson.upgradeCost *= 3;
        hole.upgradeCost *= 3;
        hamster.upgradeCost *= 3;
    }

    private void StartPowerSurgeEvent()
    {
        totalOutput *= 2;
        income *= 2;
        isRandomEventHappening = true;
        randomEventTimer = 0f;
    }

    private void EndPowerSurgeEvent()
    {
        totalOutput -= 4;
        income -= 4;
        isRandomEventHappening = false;
        randomEventTimer = 0f;
    }



    public void ShopScene()
    {
        ShopPanel.gameObject.SetActive(!ShopPanel.activeSelf);
        tutorialGO.gameObject.SetActive(!tutorialGO.activeSelf);
        ScrollMenu.gameObject.SetActive(!ScrollMenu.activeSelf);
        TotalOutput.gameObject.SetActive(!TotalOutput.activeSelf);
        AvailableMoney.gameObject.SetActive(!AvailableMoney.activeSelf);
        VolatilityUI.gameObject.SetActive(!VolatilityUI.activeSelf);
        GeneratorsProg.gameObject.SetActive(!GeneratorsProg.activeSelf);
        SaveButton.gameObject.SetActive(!SaveButton.activeSelf);
        LoadButton.gameObject.SetActive(!LoadButton.activeSelf);
    }

    public void TutorialScene()
    {
        TutorialPanel.gameObject.SetActive(!TutorialPanel.activeSelf);
        ShopGO.gameObject.SetActive(!ShopGO.activeSelf);
        ScrollMenu.gameObject.SetActive(!ScrollMenu.activeSelf);
        TotalOutput.gameObject.SetActive(!TotalOutput.activeSelf);
        AvailableMoney.gameObject.SetActive(!AvailableMoney.activeSelf);
        VolatilityUI.gameObject.SetActive(!VolatilityUI.activeSelf);
        GeneratorsProg.gameObject.SetActive(!GeneratorsProg.activeSelf);
        SaveButton.gameObject.SetActive(!SaveButton.activeSelf);
        LoadButton.gameObject.SetActive(!LoadButton.activeSelf);
    }

    public void ResetGameOne()
    {
        ResetButton.gameObject.SetActive(false);
        VerifyButton.gameObject.SetActive(true);
        StartCoroutine(ResetAfterDelay(5f));
    }

    private IEnumerator ResetAfterDelay(float delay)
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(delay);

        // Activate/deactivate the buttons
        ResetButton.gameObject.SetActive(true);
        VerifyButton.gameObject.SetActive(false);
    }

    public void ResetGameTwo()
    {
        VerifyButton.gameObject.SetActive(false);
        ResetButton.gameObject.SetActive(true);
        int startingMoney = totalOutput * 10;
        bike.resetProgress();
        water.resetProgress();
        dutch.resetProgress();
        coal.resetProgress();
        coolSystem.resetProgress();
        hydro.resetProgress();
        electric.resetProgress();
        solar.resetProgress();
        nuclear.resetProgress();
        dyson.resetProgress();
        hole.resetProgress();
        hamster.resetProgress();
        availMoney = startingMoney;
        volatility = 0f;
        totalOutput = 1;
        insuranceItemBought = false;
        isPurchaseMechanicItem = false;
        isPowerBreakerActive = false;
        lunchRoom.purchased = false;
        backUpGeneratorBought = false;
        randomEventTimer = 0;
        elapsedTime = 0;
    }
}
