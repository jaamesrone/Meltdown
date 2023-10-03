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
    public int PowerBreakerCost = 100;

    public float availMoney;
    public float disasterMultiplier = 1.0f;
    public float volatility;
    public float powerIncreaseDuration = 30.0f; // Duration of the power increase in seconds

    public GameObject PowerBreakerButton;
    public GameObject MechanicButton;
    public GameObject waterWheelUpgradeButton;

    private bool isPowerBreakerActive = false;
    private bool isRandomEventHappening = false;

    private float powerBreakerEndTime = 0.0f;
    private float randomEventDuration = 300f; //5 minute timer
    private float randomEventTimer = 0f;

    private Bike bike;
    private Disasters disaster;

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
        bikeOutput = 0;

        bike = GetComponent<Bike>();
        disaster = GetComponent<Disasters>();
    }

    // Update is called once per frame
    void Update()
    {
        incomeStarter();
        CheckRandomEvent();
        CheckDurationPowerBreaker();
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

            // Reset the elapsed time
            elapsedTime = 0f;
        }
    }

    public void MechanicButtonVisibile()
    {
        MechanicButton.SetActive(!MechanicButton.activeSelf);
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

            // Activate the power breaker and set the end time
            isPowerBreakerActive = true;
            powerBreakerEndTime = Time.time + powerIncreaseDuration;

            // Apply the power increase effect here (e.g., increase total power output)
            IncreaseTotalPowerOutput();
        }
        else
        {
            isPurchaseMechanicItem = false;
        }
    }

    private void IncreaseTotalPowerOutput()
    {
        totalOutput *= 4; // Double the power output during the power breaker period
        waterWheelOutput *= 4;
        bikeOutput *= 4;
    }

    private void ResetPowerBreakerEffects()
    {
        totalOutput /= 4; // Restore the original power output
        waterWheelOutput /= 4;
        bikeOutput /= 4;
    }

    private void CheckDurationPowerBreaker()
    {
        // Check if the power breaker is active and has expired
        if (isPowerBreakerActive && Time.time >= powerBreakerEndTime)
        {
            // Deactivate the power breaker and reset any effects
            isPowerBreakerActive = false;
            ResetPowerBreakerEffects();
        }
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
                    waterWheelOutput *= 2;
                    bikeOutput *= 2;
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
        waterWheelOutput *= 2;
        bikeOutput *= 2;
        isRandomEventHappening = true;
        randomEventDuration = 600f; // 10 minutes
    }

    private void EndPowerSurgeEvent()
    {
        totalOutput /= 2;
        waterWheelOutput /= 2;
        bikeOutput /= 2;
        isRandomEventHappening = false;
        randomEventTimer = 0f;
    }



    public void ShopScene()
    {
        SceneManager.LoadScene("Shop");
    }

    public void GoBack()
    {
        SceneManager.LoadScene("James'_Test_Scene");
    }

}
