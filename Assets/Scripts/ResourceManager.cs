using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

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
    public int PowerBreakerCost = 100;
    public int LunchRoomCost = 500;

    public float availMoney;
    public float disasterMultiplier = 1.0f;
    public float volatility;
    public float buttonVisibility = 5.0f; // Duration of the power increase in seconds

    public GameObject VolatilityGO;
    public GameObject UpgradeWaterButton;
    public GameObject UpgradeBikeButton;
    public GameObject UpgradeDutchButton;
    public GameObject UpgradeCoalButton;
    public GameObject ShopGO;
    public GameObject TotalOutputGO;
    public GameObject bikeOutputGO;
    public GameObject waterOutputGO;
    public GameObject dutchOutputGO;
    public GameObject coalOutputGO;
    public GameObject PowerBreakerButton;
    public GameObject MechanicButton;
    public GameObject LunchRoomButton;
    public GameObject waterWheelUpgradeButton;
    public GameObject dutchUpgradeButton;
    public GameObject coalUpgradeButton;
    public GameObject ShopPanel;

    private bool isPowerBreakerActive = false;
    private bool isRandomEventHappening = false;

    
    private float powerBreakerEndTime = 0.0f;
    private float randomEventDuration = 300f; //5 minute timer
    private float randomEventTimer = 0f;

    private Bike bike;
    private Disasters disaster;
    private LunchRoom lunchRoom;

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
        buttonVisibility = 5;
        bike = GetComponent<Bike>();
        disaster = GetComponent<Disasters>();
        lunchRoom = GetComponent<LunchRoom>();
    }

    // Update is called once per frame
    void Update()
    {
        incomeStarter();
        CheckRandomEvent();
    //    CheckDurationPowerBreaker();
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
            float randomValue = Random.Range(-0.5f, 0.5f);
            // For example, you can increase income by income int
            availMoney += (income + randomValue);

            if (availMoney >= 1000 && !waterWheelUpgradeButton.activeSelf) //if the players income is over 1000, button unhides.
            {
                waterWheelUpgradeButton.SetActive(true);
            }

            if (availMoney >= 2000 && !dutchUpgradeButton.activeSelf) //if the players income is over 2000, button unhides.
            {
                dutchUpgradeButton.SetActive(true);
            }

            if (availMoney >= 3000 && !dutchUpgradeButton.activeSelf) //if the players income is over 3000, button unhides.
            {
                coalUpgradeButton.SetActive(true);
            }

            // Reset the elapsed time
            elapsedTime = 0f;
        }
    }

    public void MechanicButtonVisibile()
    {
        MechanicButton.SetActive(!MechanicButton.activeSelf);
    }

    public void LunchRoomButtonVisibile()
    {
        LunchRoomButton.SetActive(!LunchRoomButton.activeSelf);
    }

    public void PowerBreakerButtonVisibile()
    {
        PowerBreakerButton.SetActive(!PowerBreakerButton.activeSelf); 
    }

    public void BuyAndUseMechanicItem(bool isBuy)
    {
        if (isBuy&& availMoney >= disaster.mechanicItemCost)   
        {
            isPurchaseMechanicItem = true;
            availMoney -= disaster.mechanicItemCost;
            MechanicButtonVisibile();
          //  Invoke("MechanicButtonVisibile", buttonVisibility);
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
            Invoke("PowerBreakerButtonVisibile", buttonVisibility);

            IncreaseTotalPowerOutput();
            Invoke("ResetPowerBreakerEffects", buttonVisibility);
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
        randomEventTimer += Time.deltaTime;

        if (isRandomEventHappening)
        {
            if (randomEventTimer >= randomEventDuration)
            {
                EndPowerSurgeEvent();
            }
            else
            {
                float randomEvent = Random.Range(0.0f, 1.0f);
                if (randomEvent < 0.5f)
                {
                    // Continue the current event (power surge)
                    totalOutput *= 2;
                }
                else
                {
                    availMoney *= 0.9f;
                }
            }
        }
        else
        {
            if (randomEventTimer >= 100f && Random.Range(1, 101) == 1)
            {
                StartPowerSurgeEvent();
            }
        }
    }


    private void StartPowerSurgeEvent()
    {
        totalOutput *= 2;
        isRandomEventHappening = true;
        randomEventDuration = 600f; // 10 minutes
    }

    private void EndPowerSurgeEvent()
    {
        totalOutput /= 2;
        isRandomEventHappening = false;
        randomEventTimer = 0f;
    }

    public void ShopScene()
    {
        VolatilityGO.gameObject.SetActive(!VolatilityGO.activeSelf);
        UpgradeWaterButton.SetActive(UpgradeWaterButton.activeSelf);
        UpgradeBikeButton.gameObject.SetActive(!UpgradeBikeButton.activeSelf);
        UpgradeDutchButton.gameObject.SetActive(!UpgradeDutchButton.activeSelf);
        UpgradeCoalButton.gameObject.SetActive(!UpgradeCoalButton.activeSelf);
        ShopPanel.gameObject.SetActive(!ShopPanel.activeSelf);
        TotalOutputGO.gameObject.SetActive(!TotalOutputGO.activeSelf);
        waterOutputGO.gameObject.SetActive(!waterOutputGO.activeSelf);
        bikeOutputGO.gameObject.SetActive(!bikeOutputGO.activeSelf);
        dutchOutputGO.gameObject.SetActive(!dutchOutputGO.activeSelf);
        coalOutputGO.gameObject.SetActive(!coalOutputGO.activeSelf);
        ShopGO.gameObject.SetActive(!ShopGO.activeSelf);
    }
}
;