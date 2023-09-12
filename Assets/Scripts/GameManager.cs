using UnityEngine;
using TMPro;

public class GameManager : Singleton<GameManager>
{

    // Reference to the ResourceManager
    public ResourceManager resourceManager;
    public WaterWheel waterWheel;

    // TextMeshProUGUI objects to display the values (assign these in the Unity editor)
    public TextMeshProUGUI waterWheelText;
    public TextMeshProUGUI powerOutputText;
    public TextMeshProUGUI availMoneyText;
    public TextMeshProUGUI volatilityText;
    public TextMeshProUGUI bikeOutputText;

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
        powerOutputText.text = "Total Output: " + resourceManager.totalOutput.ToString();
        availMoneyText.text = "Available Money: " + resourceManager.availMoney.ToString();
        volatilityText.text = "Volatility: " + resourceManager.volatility.ToString();
        waterWheelText.text = "WaterOutput: " + resourceManager.waterWheelOutput.ToString();
        bikeOutputText.text = "Bike Output: " + resourceManager.bikeOutput.ToString();
    }

    // Save game data
    public static void SaveGame(ResourceManager resourceManager)
    {
        PlayerPrefs.SetInt("TotalOutput", resourceManager.totalOutput);
        PlayerPrefs.SetInt("WaterWheelOutput", resourceManager.waterWheelOutput);
        PlayerPrefs.SetInt("Income", resourceManager.income);
        PlayerPrefs.SetInt("AvailMoney", resourceManager.availMoney);
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
        resourceManager.availMoney = PlayerPrefs.GetInt("AvailMoney", 100);
        resourceManager.volatility = PlayerPrefs.GetFloat("Volatility", 0.0f);
        resourceManager.bikeOutput = PlayerPrefs.GetInt("BikeOutput", 0);
    }

    // Delete saved game data
    public static void DeleteSavedGame()
    {
        PlayerPrefs.DeleteAll();
    }
}
