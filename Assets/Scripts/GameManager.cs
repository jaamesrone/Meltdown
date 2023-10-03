using UnityEngine;
using TMPro;
using System;

public class GameManager : Singleton<GameManager>
{
    public GameObject RManager;
    public GameObject CanvasObject;
    // Reference to the ResourceManager
    public ResourceManager resourceManager;

    // TextMeshProUGUI objects to display the values (assign these in the Unity editor)
    public TextMeshProUGUI waterWheelText;
    public TextMeshProUGUI powerOutputText;
    public TextMeshProUGUI availMoneyText;
    public TextMeshProUGUI volatilityText;
    public TextMeshProUGUI bikeOutputText;


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
        GameManager.LoadGame(resourceManager);
       

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
            volatilityText.text = "Volatility: " + resourceManager.volatility.ToString();

        if (waterWheelText != null)
            waterWheelText.text = "WaterOutput: " + resourceManager.waterWheelOutput.ToString();

        if (bikeOutputText != null)
            bikeOutputText.text = "Bike Output: " + resourceManager.bikeOutput.ToString();
    }

    // Save game data
    public static void SaveGame(ResourceManager resourceManager)
    {
        PlayerPrefs.SetInt("TotalOutput", resourceManager.totalOutput);
        PlayerPrefs.SetInt("WaterWheelOutput", resourceManager.waterWheelOutput);
        PlayerPrefs.SetInt("Income", resourceManager.income);
        PlayerPrefs.SetFloat("AvailMoney", resourceManager.Money);
        PlayerPrefs.SetFloat("Volatility", resourceManager.volatility);
        PlayerPrefs.SetInt("BikeOutput", resourceManager.bikeOutput);
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
    }

    // Delete saved game data
    public static void DeleteSavedGame()
    {
        PlayerPrefs.DeleteAll();
    }
}
