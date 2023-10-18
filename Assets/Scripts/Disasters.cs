using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disasters : MonoBehaviour
{
    public event System.Action<string> OnDisasterActivated;
    public ResourceManager resourceManager;
    public BackupGenerator backupGenny;
    public WaterWheel WaterWheel;
    public Bike bike;

    public float disasterInterval = 300f;

    public int mechanicItemCost = 100;

    private bool disasterTwoActive;

    private float disasterCheckInterval = 10f; // 5 minutes

    // Start is called before the first frame update
    void Start()
    {
        resourceManager = GetComponent<ResourceManager>();
        WaterWheel = GetComponent<WaterWheel>();
        bike = GetComponent<Bike>();
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
            float randomValue = Random.Range(0.0f, 5.0f);

            if (randomValue <= resourceManager.volatility)
            {
                if (resourceManager.OwnsMechanicItem())
                {
                    resourceManager.MechanicButtonVisibile();
                    yield return this;
                    Debug.Log("immune to disaster, buy another one");
                }
                else
                {
                    if (resourceManager.waterWheelOutput > 0.0f)
                    {
                        float disasterChoice = Random.Range(0.0f, 1.0f);
                        if (disasterChoice < 0.25f) // Adjust the probability (e.g., 25% chance)
                        {
                            ActivateDisasterOne();
                            resourceManager.volatility = 0;
                        }
                        else if (disasterChoice < 0.5f) // Adjust the probability (e.g., 25% chance)
                        {
                            ActivateDisasterTwo();
                            resourceManager.volatility = 0;
                        }
                        else if (disasterChoice < 0.75f) // Adjust the probability (e.g., 25% chance)
                        {
                            ActivateDisasterThree();
                            resourceManager.volatility = 0;
                        }

                        else if (disasterChoice < 0.95)
                        {
                            ActivateDisasterFour();
                            resourceManager.volatility = 0;
                        }
                        else
                        {
                            ActivateDisasterFive();
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
                        else if (disasterChoice < 0.50f)
                        {
                            ActivateDisasterTwo();
                            resourceManager.volatility = 0;
                        }
                        else if (disasterChoice < 0.75)
                        {
                            ActivateDisasterThree();
                            resourceManager.volatility = 0;
                        }
                        else
                        {
                            ActivateDisasterFour();
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
        string disasterMessage = "Disaster 1: Activated!";
        OnDisasterActivated?.Invoke(disasterMessage);

        GameManager.Instance.DisasterTexts.gameObject.SetActive(true);
        Debug.Log("Disaster 1");
        resourceManager.disasterMultiplier += 0.5f;


        int reducedIncome = (int)(resourceManager.income / resourceManager.disasterMultiplier);

        StartCoroutine(ReduceIncomeForDuration(5f, reducedIncome)); // Reduce income for 3 minutes (180 seconds)
        if (resourceManager.backUpGeneratorBought == true)
        {
            backupGenny.ActivateBackUpGenerator();
        }
        else
        {
            return;
        }
    }

    private void ActivateDisasterTwo()
    {
        string disasterMessage = "Disaster 2: Activated!";
        OnDisasterActivated?.Invoke(disasterMessage);
        GameManager.Instance.DisasterTexts.gameObject.SetActive(true);
        Debug.Log("Disaster 2");
        bool regressBikeOutput = Random.Range(0, 2) == 0;


        if (regressBikeOutput)
        {
            if (resourceManager.bikeOutput > 0)
            {
                resourceManager.bikeOutput--;
                resourceManager.totalOutput--;
                if (resourceManager.backUpGeneratorBought == true)
                {
                    backupGenny.ActivateBackUpGenerator();
                }
                else
                {
                    return;
                }
            }
        }
        else
        {
            if (resourceManager.waterWheelOutput > 0)
            {
                resourceManager.waterWheelOutput--;
                resourceManager.totalOutput--;
                if (resourceManager.backUpGeneratorBought == true)
                {
                    backupGenny.ActivateBackUpGenerator();
                }
                else
                {
                    return;
                }
            }
        }

        disasterTwoActive = false; // No need to set it to true here, as it's never used again in this code.
    }

    private void ActivateDisasterThree()
    {
        string disasterMessage = "Disaster 3: Activated!";
        OnDisasterActivated?.Invoke(disasterMessage);
        GameManager.Instance.DisasterTexts.gameObject.SetActive(true);
        Debug.Log("Disaster 3");
        resourceManager.waterWheelOutput = 0;
        WaterWheel.buttonClicked = 0;
        if (resourceManager.backUpGeneratorBought == true)
        {
            backupGenny.ActivateBackUpGenerator();
        }
        else
        {
            return;
        }

    }

    public void ActivateDisasterFour()
    {
        string disasterMessage = "Disaster 4: Activated!";
        OnDisasterActivated?.Invoke(disasterMessage);
        GameManager.Instance.DisasterTexts.gameObject.SetActive(true);
        Debug.Log("Disaster 4");
        // Reset upgrade progress
        ResetUpgradeProgress();


        if (resourceManager.backUpGeneratorBought == true)
        {
            backupGenny.ActivateBackUpGenerator();
        }
        else
        {
            return;
        }


    }

    public void ActivateDisasterFive()
    {
        string disasterMessage = "Disaster 5: Activated!";
        OnDisasterActivated?.Invoke(disasterMessage);
        GameManager.Instance.DisasterTexts.gameObject.SetActive(true);
        Debug.Log("Disaster 5: Random Power Generator Disabled for 1 Hour");

        // Randomly choose a power generator (0 for Bike, 1 for WaterWheel)
        int generatorChoice = Random.Range(0, 2);

        if (generatorChoice == 0)
        {
            // Disable the Bike power generator for one hour (3600 seconds)
            StartCoroutine(DisableGeneratorForDuration(GetComponent<Bike>(), 3600f));
            if (resourceManager.backUpGeneratorBought == true)
            {
                backupGenny.ActivateBackUpGenerator();
            }
            else
            {
                return;
            }
        }
        else
        {
            // Disable the WaterWheel power generator for one hour (3600 seconds)
            StartCoroutine(DisableGeneratorForDuration(GetComponent<WaterWheel>(), 3600f));
            if (resourceManager.backUpGeneratorBought == true)
            {
                backupGenny.ActivateBackUpGenerator();
            }
            else
            {
                return;
            }
        }
    }

    private IEnumerator DisableGeneratorForDuration(Component generator, float durationInSeconds)
    {
        Bike bike = null;
        WaterWheel waterWheel = null;

        if (generator is Bike)
        {
            bike = generator as Bike;
            bike.bikeOutput = 0;
        }
        else if (generator is WaterWheel)
        {
            waterWheel = generator as WaterWheel;
            waterWheel.waterWheelOutput = 0;
        }

        yield return new WaitForSeconds(durationInSeconds);

        if (bike != null)
        {
            bike.bikeOutput = 1; // Restore the Bike power generator.
        }
        else if (waterWheel != null)
        {
            waterWheel.waterWheelOutput = 2; // Restore the WaterWheel power generator.
        }
    }


    private IEnumerator ReduceIncomeForDuration(float durationInSeconds, int reducedIncome)
    {
        int originalIncome = resourceManager.income;
        resourceManager.income = reducedIncome;

        yield return new WaitForSeconds(durationInSeconds);

        resourceManager.income = originalIncome;
    }

    private void ResetUpgradeProgress()
    {
        resourceManager.Money /= 2;
        resourceManager.bikeOutput = 0;
        resourceManager.waterWheelOutput = 0;
        resourceManager.dutchOutput = 0;
        resourceManager.coalOutput = 0;
        resourceManager.coolingOutput = 0;
    }
}
