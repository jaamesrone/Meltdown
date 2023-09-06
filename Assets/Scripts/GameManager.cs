using UnityEngine;
using TMPro;

public class GameManager : Singleton<GameManager>
{

    // Reference to the ResourceManager
    public ResourceManager resourceManager;
    public WaterWheel waterWheel;

    // TextMeshProUGUI objects to display the values (assign these in the Unity editor)
    public TextMeshProUGUI waterWheelUI;
    public TextMeshProUGUI powerOutputText;
    public TextMeshProUGUI availMoneyText;
    public TextMeshProUGUI volatilityText;

    // Start is called before the first frame update
    void Start()
    {

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
        powerOutputText.text = "Power Output: " + resourceManager.powerOutput.ToString();
        availMoneyText.text = "Available Money: " + resourceManager.availMoney.ToString();
        volatilityText.text = "Volatility: " + resourceManager.volatility.ToString();
        waterWheelUI.text = "WaterOutput: " + resourceManager.waterWheelOutput.ToString();
    }
}
