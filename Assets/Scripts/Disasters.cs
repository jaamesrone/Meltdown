using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disasters : MonoBehaviour
{
    private ResourceManager resourceManager;
    public GameObject disasterAlert;
    private bool disasterAlertActive = false;
    private float alertDuration = 10f;

    private bool disasterTwoActive = false;
    private float disasterTwoCooldown = 60f;


    // Start is called before the first frame update
    void Start()
    {
        resourceManager = GetComponent<ResourceManager>();
        disasterAlert.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        // Generates a random float between 0.1 and 100.1 (excluding 100.1)
        float randomValue = Random.Range(0.1f, 100.1f);
        Debug.Log("randomValue" + randomValue);

        // Checks if the random value is equal to the volatility float variable
        if (randomValue <= resourceManager.volatility)
        {
            disasterOccurs();
        }

        if(disasterTwoActive)
        {
            disasterTwoCooldown -= Time.deltaTime;
            if (disasterTwoCooldown>= 0f)
            {
                ActivateDisasterTwo(); 
            }
        }
    }

    public void disasterOccurs()
    {
        if (!disasterAlertActive)
        {
            float randomValue = Random.Range(0, 100);
            if (randomValue < 0.5)
            {
                ActivateDisasterOne();
                Debug.Log("1");
            }
            else
            {

                ActivateDisasterTwo();
                Debug.Log("2");
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

        StartCoroutine(ReduceIncomeForDuration(180f, reducedIncome)); // Reduce income for 3 minutes (180 seconds)
        StartCoroutine(HideAlertAfterDelay(alertDuration));
    }



    private void ActivateDisasterTwo()//activates disaster two
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

        disasterTwoCooldown = Random.Range(60f, 120f);
        disasterTwoActive = true;
        disasterTwoActive = false;

    }
    

    private IEnumerator HideAlertAfterDelay(float delay)
    {
        // Wait for the specified delay in seconds.
        yield return new WaitForSeconds(delay);

        // Hide the DisasterAlert GameObject.
        disasterAlert.SetActive(false);
        disasterAlertActive = false;
    }

    private IEnumerator ReduceIncomeForDuration(float durationInSeconds, int reducedIncome)
    {
        // Save the original income and apply the reduction
        int originalIncome = resourceManager.income;
        resourceManager.income = reducedIncome;

        // Wait for the specified duration
        yield return new WaitForSeconds(durationInSeconds);

        // Restore the original income after the duration
        resourceManager.income = originalIncome;

        // Make sure to update your UI or perform any other necessary actions
    }


}
