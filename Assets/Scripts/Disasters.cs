using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disasters : MonoBehaviour
{
    public ResourceManager resourceManager;
    public WaterWheel WaterWheel;
    public GameObject disasterAlert;
    public float disasterInterval = 300f;

    public int mechanicItemCost = 100;

    private bool disasterAlertActive = false;
    private bool disasterTwoActive;

    private float randomValue; // Declare a class-level variable to store the random value
    private float alertDuration = 10f;
    private float disasterCheckInterval = 10f; // 5 minutes

    // Start is called before the first frame update
    void Start()
    {
        resourceManager = GetComponent<ResourceManager>();
        WaterWheel = GetComponent<WaterWheel>();
        disasterAlert.SetActive(false);
        StartCoroutine(CheckForDisasters());
    }

    // Update is called once per frame
    void Update()
    {

    }

    private IEnumerator CheckForDisasters()
    {
        while (true)
        {
            float randomValue = Random.Range(0.0f, 100.0f);

            if (randomValue <= resourceManager.volatility)
            {
                if (resourceManager.OwnsMechanicItem())
                {
                    //if the state is false, it goes to the [else] statement in BuyandUse.
                    resourceManager.BuyAndUseMechanicItem(false);
                    resourceManager.MechanicButtonVisibile();
                    // Generate a new random value for the next potential disaster
                    randomValue = Random.Range(0.0f, 1.0f);
                    Debug.Log("New Random Value for Next Disaster lol: " + randomValue); // Log the new random value for testing
                    resourceManager.volatility = 0;
                }
                else
                {
                    if (resourceManager.waterWheelOutput > 0.0f)
                    {
                        float disasterChoice = Random.Range(0.0f, 1.0f);
                        if (disasterChoice < 0.333333f)
                        {
                            ActivateDisasterOne();
                            resourceManager.volatility = 0;
                        }
                        else if ((disasterChoice < 0.666666f) && (disasterChoice > 0.333333f))
                        {
                            ActivateDisasterTwo();
                            resourceManager.volatility = 0;
                        }
                        else
                        {
                            ActivateDisasterThree();
                            resourceManager.volatility = 0;
                        }
                    }
                    else
                    {
                        float disasterChoice = Random.Range(0.0f, 1.0f);

                        if (disasterChoice < 0.5f)
                        {
                            ActivateDisasterOne();
                            resourceManager.volatility = 0;
                        }
                        else
                        {
                            ActivateDisasterTwo();
                            resourceManager.volatility = 0;
                        }
                    }
                }
            }

            yield return new WaitForSeconds(disasterCheckInterval);
        }
    }

    public void ActivateDisasterOne()
    {
        Debug.Log("Disaster 1");
        resourceManager.disasterMultiplier += 0.5f;

        disasterAlert.SetActive(true);
        disasterAlertActive = true;

        int reducedIncome = (int)(resourceManager.income / resourceManager.disasterMultiplier);

        StartCoroutine(ReduceIncomeForDuration(5f, reducedIncome)); // Reduce income for 3 minutes (180 seconds)
        StartCoroutine(HideAlertAfterDelay(alertDuration));
    }

    private void ActivateDisasterTwo()
    {
        Debug.Log("Disaster 2");
        bool regressBikeOutput = Random.Range(0, 2) == 0;

        disasterAlert.SetActive(true);
        disasterAlertActive = true;

        StartCoroutine(HideAlertAfterDelay(alertDuration));

        if (regressBikeOutput)
        {
            if (resourceManager.bikeOutput > 0)
            {
                resourceManager.bikeOutput--;
                resourceManager.totalOutput--;
            }
        }
        else
        {
            if (resourceManager.waterWheelOutput > 0)
            {
                resourceManager.waterWheelOutput--;
                resourceManager.totalOutput--;
            }
        }

        disasterTwoActive = false; // No need to set it to true here, as it's never used again in this code.
    }

    private void ActivateDisasterThree()
    {
        Debug.Log("Disaster 3");
        resourceManager.waterWheelOutput = 0;
        WaterWheel.buttonClicked = 0;
    }

    private IEnumerator HideAlertAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        disasterAlert.SetActive(false);
        disasterAlertActive = false;
    }

    private IEnumerator ReduceIncomeForDuration(float durationInSeconds, int reducedIncome)
    {
        int originalIncome = resourceManager.income;
        resourceManager.income = reducedIncome;

        yield return new WaitForSeconds(durationInSeconds);

        resourceManager.income = originalIncome;
    }
}
