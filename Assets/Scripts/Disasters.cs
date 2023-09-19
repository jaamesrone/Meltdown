using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disasters : MonoBehaviour
{
    private ResourceManager resourceManager;
    public GameObject disasterAlert;

    public int mechanicItemCost = 100;

    private bool disasterAlertActive = false;
    private bool disasterTwoActive;

    private float randomValue; // Declare a class-level variable to store the random value
    private float alertDuration = 10f;
    private float disasterInterval = 5f; // 5 minutes
    private float timeBetweenDisasters = 3f;
    private float disasterTimer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        resourceManager = GetComponent<ResourceManager>();
        disasterAlert.SetActive(false);
        StartCoroutine(StartDisasterTimer());
    }

    // Update is called once per frame
    void Update()
    {
        disasterTimer += Time.deltaTime;

        if (disasterTimer >= disasterInterval)
        {
            float randomFloat = Random.Range(0.0f, 100.0f);

            if (randomFloat >= resourceManager.volatility)
            {
                disasterOccurs();
                disasterTimer = 0f; // Reset the timer
            }
        }
    }

    public void disasterOccurs()
    {
        if (!disasterAlertActive)
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
                // Determine which disaster to activate based on another random value
                float disasterTypeRandom = Random.Range(0.0f, 1.0f);
                if (disasterTypeRandom < 0.5f)
                {
                    ActivateDisasterOne();
                    resourceManager.volatility = 0;
                }
                else
                {
                    ActivateDisasterTwo();
                    resourceManager.volatility = 0;
                }

                // Generate a new random value for the next potential disaster
                randomValue = Random.Range(0.0f, 1.0f);
                Debug.Log("New Random Value for Next Disaster: " + randomValue); // Log the new random value for testing
            }
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

    private IEnumerator StartDisasterTimer()
    {
        yield return new WaitForSeconds(disasterInterval); // Wait for the first disasterInterval before starting the timer

        while (true)
        {
            float randomValue = Random.Range(0.0f, 1.0f);

            if (randomValue < 0.5f)
            {
                ActivateDisasterOne();
            }
            else
            {
                ActivateDisasterTwo();
            }

            disasterTimer = 0f;
            yield return new WaitForSeconds(disasterInterval);
        }
    }
}
