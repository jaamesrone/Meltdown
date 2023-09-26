using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ResourceManager : MonoBehaviour
{
    public MechanicItem mechanicItem;

    public bool OwnsMechanicItem()
    {
        return isPurchaseMechanicItem;
    }

    public AudioSource buttonClicked;
    public int totalOutput;
    public int waterWheelOutput;
    public int income;
    public float availMoney;
    public int bikeOutput;

    public float disasterMultiplier = 1.0f;
    public float volatility;

    public GameObject MechanicButton;
    public GameObject waterWheelUpgradeButton;

    private bool isRandomEventHappening = false;
    private float randomEventDuration = 300f; //5 minute timer
    private float randomEventTimer = 0f;
    private Bike bike;
    private Disasters disaster;

    // Variables for tracking time and perSecond.
    private float elapsedTime = 0f;
    public float perSecond = 1f; // Update every 1 second

    private bool isPurchaseMechanicItem = false;


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

    private void CheckRandomEvent()
    {
        randomEventTimer += Time.deltaTime;
        //increases the timer using real-time

        if (isRandomEventHappening) //checks if the bool is true
        {
            float randomEvent = Random.Range(0.0f, 1.0f);
            if (randomEvent < 0.5f)
            {
                totalOutput *= 2;

                if (randomEventTimer >= randomEventDuration)
                {
                    //event has ended, reset the effects.
                    totalOutput /= 2;
                    isRandomEventHappening = false;
                    randomEventTimer = 0f;
                }
            }
            else
            {
                availMoney *= 0.9f;
            }
        }
        else
        {
            if (randomEventTimer >= 100f && Random.Range(1, 101) == 1) //checks if it's time to start a random event (1/100 chance every 100 seconds) you can always change this
            {
                isRandomEventHappening = true;
            }
        }
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
