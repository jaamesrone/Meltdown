using UnityEngine;
using TMPro;

public class GameManager : Singleton<GameManager>
{

    // Reference to the ResourceManager
    public ResourceManager resourceManager;

    // TextMeshProUGUI objects to display the values (assign these in the Unity editor)
    public TextMeshProUGUI powerOutputText;
    public TextMeshProUGUI incomeText;
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
        incomeText.text = "Income: " + resourceManager.income.ToString();
        availMoneyText.text = "Available Money: " + resourceManager.availMoney.ToString();
        volatilityText.text = "Volatility: " + resourceManager.volatility.ToString();
    }
}
