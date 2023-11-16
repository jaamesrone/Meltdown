using UnityEngine;
using TMPro;
using System;
using System.Collections;
using System.IO;
public class GameManager : Singleton<GameManager>
{
    public GameObject RManager;
    public GameObject CanvasObject;
    // Reference to the ResourceManager
    public ResourceManager resourceManager;

    // TextMeshProUGUI objects to display the values (assign these in the Unity editor)
    public TextMeshProUGUI disasterMessageText;
    public TextMeshProUGUI DisasterTexts;
    public TextMeshProUGUI waterWheelText;
    public TextMeshProUGUI powerOutputText;
    public TextMeshProUGUI availMoneyText;
    public TextMeshProUGUI volatilityText;
    public TextMeshProUGUI bikeOutputText;
    public TextMeshProUGUI dutchOutputText;
    public TextMeshProUGUI coalOutputText;
    public TextMeshProUGUI coolingOutputText;
    public TextMeshProUGUI hydroOutputText;
    public TextMeshProUGUI electricalOutputText;
    public TextMeshProUGUI solarOutputText;
    public TextMeshProUGUI nuclearOutputText;
    private float alertDuration = 3;

    public override void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
            DontDestroyOnLoad(CanvasObject);
            DontDestroyOnLoad(RManager);
        }
        else
        {
            Destroy(gameObject);
            Destroy(CanvasObject);
            Destroy(RManager);
        }
    }
    

    // Start is called before the first frame update
    void Start()
    {
        //GameManager.LoadGame(resourceManager);
        // Subscribe to the disaster event
        Disasters disasterScript = RManager.GetComponent<Disasters>();
        disasterScript.OnDisasterActivated += HandleDisasterActivated;


    }

    

    // Update is called once per frame
    void Update()
    {
        // Call the method to update the resource values UI
        UpdateResourceUIText();
    }

        // Method to update the resource values UI based on the ResourceManager's values
        private void UpdateResourceUIText()
    {
        if (resourceManager == null)
        {
            Debug.LogError("ResourceManager is not assigned in the Inspector.");
            return;
        }

        if (powerOutputText != null)
            powerOutputText.text = "Total Output: " + resourceManager.totalOutput.ToString();

        if (availMoneyText != null)
            availMoneyText.text = "Available Money: " + resourceManager.Money.ToString();

        if (volatilityText != null)
            volatilityText.text = "Volatility: " + resourceManager.volatility.ToString() + "%";

        if (waterWheelText != null)
            waterWheelText.text = "WaterOutput: " + resourceManager.waterWheelOutput.ToString();

        if (bikeOutputText != null)
            bikeOutputText.text = "Bike Output: " + resourceManager.bikeOutput.ToString();

        if (dutchOutputText != null)
            dutchOutputText.text = "Dutch Windmill Output: " + resourceManager.dutchOutput.ToString();

        if (coalOutputText != null)
            coalOutputText.text = "Coal Plant Output: " + resourceManager.coalOutput.ToString();

        if (coolingOutputText != null)
            coolingOutputText.text = "Cooling System Output: " + resourceManager.coolingOutput.ToString();

        if (hydroOutputText != null)
            hydroOutputText.text = "Hydroelectric Dam Output: " + resourceManager.hydroOutput.ToString();

        if (electricalOutputText != null)
            electricalOutputText.text = "Electric Windmill Output: " + resourceManager.electricalOutput.ToString();

        if (solarOutputText != null)
            solarOutputText.text = "Solar Farm Output: " + resourceManager.solarOutput.ToString();

        if (nuclearOutputText != null)
            nuclearOutputText.text = "Nuclear Plant Output: " + resourceManager.nuclearOutput.ToString();
    }
    
    // Save game data

    /*public static void SaveGame(ResourceManager resourceManager)
    {
        PlayerPrefs.SetInt("TotalOutput", resourceManager.totalOutput);
        PlayerPrefs.SetInt("WaterWheelOutput", resourceManager.waterWheelOutput);
        PlayerPrefs.SetInt("Income", resourceManager.income);
        PlayerPrefs.SetFloat("AvailMoney", resourceManager.Money);
        PlayerPrefs.SetFloat("Volatility", resourceManager.volatility);
        PlayerPrefs.SetInt("BikeOutput", resourceManager.bikeOutput);
        PlayerPrefs.SetInt("DutchOutput", resourceManager.dutchOutput);
        PlayerPrefs.SetInt("CoalOutput", resourceManager.coalOutput);
        PlayerPrefs.SetInt("CoolingOutput", resourceManager.coolingOutput);
        PlayerPrefs.SetInt("HydroOutput", resourceManager.coolingOutput);
        PlayerPrefs.SetInt("ElectricalOutput", resourceManager.electricalOutput);
        PlayerPrefs.SetInt("SolarOutput", resourceManager.solarOutput);
        PlayerPrefs.SetInt("NuclearOutput", resourceManager.nuclearOutput);
        PlayerPrefs.Save();
    }
    


    // Load game data
    
    public static void LoadGame(ResourceManager resourceManager)
    {
        resourceManager.totalOutput = PlayerPrefs.GetInt("TotalOutput", 1);
        resourceManager.waterWheelOutput = PlayerPrefs.GetInt("WaterWheelOutput", 0);
        resourceManager.income = PlayerPrefs.GetInt("Income", 1);
        resourceManager.Money = PlayerPrefs.GetInt("AvailMoney", 100);
        resourceManager.volatility = PlayerPrefs.GetFloat("Volatility", 0.0f);
        resourceManager.bikeOutput = PlayerPrefs.GetInt("BikeOutput", 0);
        resourceManager.dutchOutput = PlayerPrefs.GetInt("DutchOutput", 0);
        resourceManager.coalOutput = PlayerPrefs.GetInt("CoalOutput", 0);
        resourceManager.coolingOutput = PlayerPrefs.GetInt("CoolingOutput", 0);
        resourceManager.hydroOutput = PlayerPrefs.GetInt("HydroOutput", 0);
        resourceManager.electricalOutput = PlayerPrefs.GetInt("ElectricalOutput", 0);
        resourceManager.solarOutput = PlayerPrefs.GetInt("SolarOutput", 0);
        resourceManager.NuclearOutput = PlayerPrefs.GetInt("NuclearOutput", 0);
    }
    */

    // Delete saved game data
    public static void DeleteSavedGame()
    {
        PlayerPrefs.DeleteAll();
    }

    private void HandleDisasterActivated(string disasterMessage)
    {
        // Display the disaster message using TextMeshPro
        DisasterTexts.text = disasterMessage;

        // Set the disaster message visibility for a duration or until the disaster ends
        StartCoroutine(HideDisasterMessageAfterDelay());
    }

    private IEnumerator HideDisasterMessageAfterDelay()
    {
        // Display the disaster message for a specific duration
        yield return new WaitForSeconds(alertDuration);

        // Hide the disaster message
        DisasterTexts.text = "";

    }
}
