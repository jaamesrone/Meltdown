using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disasters : MonoBehaviour
{
    public event System.Action<string> OnDisasterActivated;
    public ResourceManager resourceManager;
    public BackupGenerator backupGenny;
    public WaterWheel water;
    public Bike bike;
    public HydroDam hydro;
    public DutchWindmill dutch;
    public CoalPowerPlant coal;
    public CoolingSystem coolSystem;

    public AudioSource alarm;

    public int mechanicItemCost = 100;

    private bool disasterTwoActive;

    private float disasterCheckInterval = 10f; // 5 minutes


    // Start is called before the first frame update
    void Start()
    {
        coolSystem = GetComponent<CoolingSystem>();
        coal = GetComponent<CoalPowerPlant>();
        dutch = GetComponent<DutchWindmill>();
        resourceManager = GetComponent<ResourceManager>();
        water = GetComponent<WaterWheel>();
        hydro = GetComponent<HydroDam>();
        bike = GetComponent<Bike>();
        StartCoroutine(CheckForDisasters(300f));
    }

    // Update is called once per frame
    void Update()
    {
        GameManager.Instance.BlinkerEffect();
    }

    private IEnumerator CheckForDisasters(float interval)
    {
        while (true)
        {
            yield return new WaitForSeconds(interval);

            float randomValue = Random.value;

            if (randomValue <= resourceManager.volatility)
            {
                if (resourceManager.OwnsMechanicItem())
                {
                    resourceManager.MechanicButtonVisibile();
                    yield return null;
                    Debug.Log("immune to disaster, buy another one");
                }
                else
                {
                    if (resourceManager.waterWheelOutput > 0.0f)
                    {
                        float disasterChoice = Random.value;

                        if (disasterChoice < 0.10f && !hydro.isPurchasedHydroElectricDam || disasterChoice < 0.25f && hydro.isPurchasedHydroElectricDam)
                        {
                            ActivateDisasterOne();
                            alarm.Play();
                        }
                        else if (disasterChoice < 0.25f && !hydro.isPurchasedHydroElectricDam || disasterChoice < 0.50f && hydro.isPurchasedHydroElectricDam)
                        {
                            ActivateDisasterTwo();
                            alarm.Play();
                        }
                        else if (disasterChoice < 0.35f && !hydro.isPurchasedHydroElectricDam || disasterChoice < 0.50f && hydro.isPurchasedHydroElectricDam)
                        {
                            ActivateDisasterEight();
                            alarm.Play();
                        }
                        else if (disasterChoice < 0.50f && !hydro.isPurchasedHydroElectricDam || disasterChoice < 0.75f && hydro.isPurchasedHydroElectricDam)
                        {
                            ActivateDisasterThree();
                            alarm.Play();
                        }
                        else if (disasterChoice < 0.75 && !hydro.isPurchasedHydroElectricDam || disasterChoice < 0.50f && hydro.isPurchasedHydroElectricDam)
                        {
                            ActivateDisasterFour();
                            alarm.Play();
                        }
                        else if (hydro.isPurchasedHydroElectricDam || disasterChoice < 0.50)
                        {
                            ActivateDisasterSix();
                            alarm.Play();
                        }
                        else if (disasterChoice < 0.90 && !hydro.isPurchasedHydroElectricDam || disasterChoice < 0.90f && hydro.isPurchasedHydroElectricDam)
                        {
                            ActivateDisasterSeven();
                            alarm.Play();
                        }
                        else
                        {
                            ActivateDisasterFive();
                            alarm.Play();
                        }

                        resourceManager.volatility = 0;
                    }
                    else
                    {
                        float disasterChoice = Random.value;

                        if (disasterChoice < 0.25f)
                        {
                            ActivateDisasterOne();
                        }
                        else if (disasterChoice < 0.50f)
                        {
                            ActivateDisasterTwo();
                        }
                        else if (disasterChoice < 0.75)
                        {
                            ActivateDisasterThree();
                        }
                        else
                        {
                            ActivateDisasterFour();
                        }

                        resourceManager.volatility = 0;
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
        water.buttonClicked = 0;
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

    public void ActivateDisasterSix()
    {
        string disasterMessage = "Disaster 6: Activated!";
        OnDisasterActivated?.Invoke(disasterMessage);
        GameManager.Instance.DisasterTexts.gameObject.SetActive(true);
        Debug.Log("Disaster 6: Random Power Generator ");

        // Randomly choose a power generator (0 for Bike, 1 for WaterWheel, 2 for Dutch, 3 for coal, etc)
        int generatorChoice = Random.Range(0, 5);

        if (generatorChoice == 0)
        {
            bike.resetProgress();
            if (resourceManager.backUpGeneratorBought == true)
            {
                backupGenny.ActivateBackUpGenerator();
            }
            else
            {
                return;
            }
        }
        else if (generatorChoice == 1)
        {
            water.resetProgress();
            if (resourceManager.backUpGeneratorBought == true)
            {
                backupGenny.ActivateBackUpGenerator();
            }
            else
            {
                return;
            }
        }
        else if (generatorChoice == 2)
        {
            dutch.resetProgress();
            if (resourceManager.backUpGeneratorBought == true)
            {
                backupGenny.ActivateBackUpGenerator();
            }
            else
            {
                return;
            }
        }
        else if (generatorChoice == 3)
        {
            coal.resetProgress();
            if (resourceManager.backUpGeneratorBought == true)
            {
                backupGenny.ActivateBackUpGenerator();
            }
            else
            {
                return;
            }
        }
        else if (generatorChoice == 4)
        {
            coolSystem.resetProgress();
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
            hydro.resetProgress();
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

    public void ActivateDisasterSeven()
    {
        string disasterMessage = "Disaster 7: Activated!";
        OnDisasterActivated?.Invoke(disasterMessage);
        GameManager.Instance.DisasterTexts.gameObject.SetActive(true);
        Debug.Log("Disaster 7: Random Power Generator can not be upgraded");
        if (resourceManager.backUpGeneratorBought == true)
        {
            backupGenny.ActivateBackUpGenerator();
        }
        else
        {
            return;
        }

        StartCoroutine(DisableUpgradeButton(500));


    }

    public void ActivateDisasterEight()
    {
        string disasterMessage = "Disaster 8: Activated!";
        OnDisasterActivated?.Invoke(disasterMessage);
        GameManager.Instance.DisasterTexts.gameObject.SetActive(true);
        Debug.Log("Disaster 8: Coal Plant Inactive");

        if (resourceManager.backUpGeneratorBought == true)
        {
            backupGenny.ActivateBackUpGenerator();
        }
        else
        {
            return;
        }

        StartCoroutine(DisableCoalPlant(500f));
    }

    private IEnumerator DisableCoalPlant(float duration)
    {
        resourceManager.UpgradeCoalButton.SetActive(false);

        yield return new WaitForSeconds(duration);
        resourceManager.UpgradeCoalButton.SetActive(true);
    }


    private IEnumerator DisableUpgradeButton(float duration)
    {
        int generatorChoice = Random.Range(0, 7);

        // Disable the selected game object
        GameObject selectedButton = null;
        switch (generatorChoice)
        {
            case 0:
                selectedButton = resourceManager.UpgradeBikeButton;
                if (resourceManager.backUpGeneratorBought == true)
                {
                    backupGenny.ActivateBackUpGenerator();
                }
                else
                {
                    yield return this;
                }
                break;
            case 1:
                selectedButton = resourceManager.UpgradeWaterButton;
                if (resourceManager.backUpGeneratorBought == true)
                {
                    backupGenny.ActivateBackUpGenerator();
                }
                else
                {
                    yield return this;
                }
                break;
            case 2:
                selectedButton = resourceManager.dutchUpgradeButton;
                if (resourceManager.backUpGeneratorBought == true)
                {
                    backupGenny.ActivateBackUpGenerator();
                }
                else
                {
                    yield return this;
                }
                break;
            case 3:
                selectedButton = resourceManager.coalUpgradeButton;
                if (resourceManager.backUpGeneratorBought == true)
                {
                    backupGenny.ActivateBackUpGenerator();
                }
                else
                {
                    yield return this;
                }
                break;
            case 4:
                selectedButton = resourceManager.coolingUpgradeButton;
                if (resourceManager.backUpGeneratorBought == true)
                {
                    backupGenny.ActivateBackUpGenerator();
                }
                else
                {
                    yield return this;
                }
                break;
            case 5:
                selectedButton = resourceManager.hydroUpgradeButton;
                if (resourceManager.backUpGeneratorBought == true)
                {
                    backupGenny.ActivateBackUpGenerator();
                }
                else
                {
                    yield return this;
                }
                break;
            case 6:
                selectedButton = resourceManager.electricalUpgradeButton;
                if (resourceManager.backUpGeneratorBought == true)
                {
                    backupGenny.ActivateBackUpGenerator();
                }
                else
                {
                    yield return this;
                }
                break;
            default:
                selectedButton = resourceManager.solarUpgradeButton;
                if (resourceManager.backUpGeneratorBought == true)
                {
                    backupGenny.ActivateBackUpGenerator();
                }
                else
                {
                    yield return this;
                }
                break;
        }

        if (selectedButton != null)
        {
            selectedButton.SetActive(false);
            yield return new WaitForSeconds(duration);
            selectedButton.SetActive(true); // Re-enable the game object after the duration
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
            waterWheel.waterWheelOutput = 0; // Restore the WaterWheel power generator.
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
