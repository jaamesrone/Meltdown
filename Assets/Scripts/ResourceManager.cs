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
    public GameObject UpgradeNuclearButton;
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
    public GameObject nuclearOutputGO;
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
    public GameObject UpgradeNuclearCost;
    public GameObject windMillCost;
    public GameObject SolarCost;
    public GameObject ScrollMenu;

    public TextMeshProUGUI eventText; // Reference to your TextMeshProUGUI component

    public bool backUpGeneratorBought = false;

    private bool isEventTextVisible = false;
    private bool insuranceItemBought = false;
    private bool isPowerBreakerActive = false;
    private bool isRandomEventHappening = false;
    

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
        PlayerPrefs.SetInt("insuranceCost", insuranceCost);
        PlayerPrefs.SetInt("PowerBreakerCost", PowerBreakerCost);
        PlayerPrefs.SetInt("LunchRoomCost", LunchRoomCost);

        PlayerPrefs.SetFloat("availMoney", availMoney);
        PlayerPrefs.SetFloat("disasterMultiplier", disasterMultiplier);
        PlayerPrefs.SetFloat("volatility", volatility);
        PlayerPrefs.SetFloat("DurationPowerBreakerVisibility", DurationPowerBreakerVisibility);

        PlayerPrefs.SetInt("isPurchaseMechanicItem", isPurchaseMechanicItem ? 1 : 0);
        PlayerPrefs.SetInt("isPurchasePowerBreaker", isPurchasePowerBreaker ? 1 : 0);

        PlayerPrefs.Save();
        Debug.Log("Player data saved");
    }

    public void LoadPlayerData()
    {
        totalOutput = PlayerPrefs.GetInt("totalOutput", 1);
        waterWheelOutput = PlayerPrefs.GetInt("waterWheelOutput", 0);
        income = PlayerPrefs.GetInt("income", 1);
        bikeOutput = PlayerPrefs.GetInt("bikeOutput", 0);
        dutchOutput = PlayerPrefs.GetInt("dutchOutput", 0);
        coalOutput = PlayerPrefs.GetInt("coalOutput", 0);
        coolingOutput = PlayerPrefs.GetInt("coolingOutput", 0);
        hydroOutput = PlayerPrefs.GetInt("hydroOutput", 0);
        electricalOutput = PlayerPrefs.GetInt("electricalOutput", 0);
        solarOutput = PlayerPrefs.GetInt("solarOutput", 0);
        nuclearOutput = PlayerPrefs.GetInt("nuclearOutput", 0);
        insuranceCost = PlayerPrefs.GetInt("insuranceCost", 100);
        PowerBreakerCost = PlayerPrefs.GetInt("PowerBreakerCost", 100);
        LunchRoomCost = PlayerPrefs.GetInt("LunchRoomCost", 500);

        availMoney = PlayerPrefs.GetFloat("availMoney", 100.0f);
        disasterMultiplier = PlayerPrefs.GetFloat("disasterMultiplier", 1.0f);
        volatility = PlayerPrefs.GetFloat("volatility", 0.0f);
        DurationPowerBreakerVisibility = PlayerPrefs.GetFloat("DurationPowerBreakerVisibility", 90);

        isPurchaseMechanicItem = PlayerPrefs.GetInt("isPurchaseMechanicItem", 0) == 1;
        isPurchasePowerBreaker = PlayerPrefs.GetInt("isPurchasePowerBreaker", 0) == 1;

        Debug.Log("Player data loaded");
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
        disaster = GetComponent<Disasters>();
        InvokeRepeating("CheckRandomEvent", 1, 3f);
        InvokeRepeating("CheckSecondRandomEvent", 1, 5f);
        InvokeRepeating("CheckRandomThirdEvent", 1, 7f);
        StartCoroutine(BlinkEventText());
        //lunchRoom = GetComponent<LunchRoom>();
    }

    // Update is called once per frame
    void Update()
    {
        incomeStarter();
        EndRandomEvent();
        EndThirdRandomEvent();
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
            if (availMoney >= 6000 && !nuclearUpgradeButton.activeSelf) //if the players income is over 6000, button unhides.
            {
                nuclearUpgradeButton.SetActive(true);
                nuclearOutputGO.SetActive(true);
                UpgradeNuclearCost.SetActive(true);
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
        Debug.Log("calling a randomEvent");
        if (!isRandomEventHappening)
        {
            if (UnityEngine.Random.Range(1, 101) <= 80) // 1% chance of it hitting
            {
                StartPowerSurgeEvent();
                StartCoroutine(ShowEventText("Power Surge Event Started!", 5f));
                Debug.Log("randomEvent started");
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
                default:
                    nuclear.resetProgress();
                    StartCoroutine(ShowEventText("Generator Reset: Nuclear", 5f));
                    Debug.Log("nuclear gen got reseted");
                    break;
            }

        }
    }

    private void CheckRandomThirdEvent()
    {
        Debug.Log("calling secondRandomEvent");
        if (UnityEngine.Random.Range(1, 101) <= 3 && !isThirdRandomEventHappening) //3% chance of hitting
        {
            CostOfUpgrades();
            isThirdRandomEventHappening = true;
            thirdRandomEventTimer = 0f;
            StartCoroutine(ShowEventText("Cost of Upgrades Increased!", 5f));
        }
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

    private IEnumerator BlinkEventText()
    {
        while (true)
        {
            // Toggle the visibility of eventText
            eventText.enabled = isEventTextVisible;

            // Wait for a short duration (you can adjust this value)
            yield return new WaitForSeconds(0.5f);

            // Invert the visibility flag
            isEventTextVisible = !isEventTextVisible;
        }
    }

    private IEnumerator ShowEventText(string message, float duration)
    {
        // Display the message
        eventText.text = message;

        // Enable eventText
        eventText.enabled = true;

        // Wait for the specified duration
        yield return new WaitForSeconds(duration);

        // Disable eventText
        eventText.enabled = false;
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
        UpgradeNuclearButton.gameObject.SetActive(!UpgradeNuclearButton.activeSelf);
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
        nuclearOutputGO.gameObject.SetActive(!nuclearOutputGO.activeSelf);
        UpgradeBikeCost.gameObject.SetActive(!UpgradeBikeCost.activeSelf);
        UpgradeWaterCost.gameObject.SetActive(!UpgradeWaterCost.activeSelf);
        UpgradeDutchCost.gameObject.SetActive(!UpgradeDutchCost.activeSelf);
        windMillCost.gameObject.SetActive(!windMillCost.activeSelf);
        SolarCost.gameObject.SetActive(!SolarCost.activeSelf);
        UpgradeSolarCost.gameObject.SetActive(!UpgradeSolarCost.activeSelf);
        UpgradeNuclearCost.gameObject.SetActive(!UpgradeNuclearCost.activeSelf);
        tutorialGO.gameObject.SetActive(!tutorialGO.activeSelf);
        ShopGO.gameObject.SetActive(!ShopGO.activeSelf);
        ScrollMenu.gameObject.SetActive(!ScrollMenu.activeSelf);
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
        UpgradeNuclearButton.gameObject.SetActive(!UpgradeNuclearButton.activeSelf);
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
        nuclearOutputGO.gameObject.SetActive(!nuclearOutputGO.activeSelf);
        UpgradeBikeCost.gameObject.SetActive(!UpgradeBikeCost.activeSelf);
        UpgradeWaterCost.gameObject.SetActive(!UpgradeWaterCost.activeSelf);
        UpgradeDutchCost.gameObject.SetActive(!UpgradeDutchCost.activeSelf);
        windMillCost.gameObject.SetActive(!windMillCost.activeSelf);
        SolarCost.gameObject.SetActive(!SolarCost.activeSelf);
        UpgradeSolarCost.gameObject.SetActive(!UpgradeSolarCost.activeSelf);
        UpgradeNuclearCost.gameObject.SetActive(!UpgradeNuclearCost.activeSelf);
        ShopGO.gameObject.SetActive(!ShopGO.activeSelf);
    }
}
;