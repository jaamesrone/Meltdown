using UnityEngine;
using TMPro;

public class ResourceManager : MonoBehaviour
{
    public int powerOutput;
    public int income;
    public int availMoney;
    public int volatility;

    // TextMeshProUGUI objects to display the values (assign these in the Unity editor)
    public TextMeshProUGUI powerOutputText;
    public TextMeshProUGUI incomeText;
    public TextMeshProUGUI availMoneyText;
    public TextMeshProUGUI volatilityText;

    // Variables for tracking time and update interval
    private float elapsedTime = 0f;
    public float updateInterval = 1f; // Update every 1 second

    // Start is called before the first frame update
    void Start()
    {
        // Initialize the values
        powerOutput = 1;
        income = 1;
        availMoney = 10;
        volatility = 0;

    }

    // Update is called once per frame
    void Update()
    {
        // Update the elapsed time
        elapsedTime += Time.deltaTime;

        // Check if it's time to update the values (once per second)
        if (elapsedTime >= updateInterval)
        {
            // Calculate the new values per second here
            // For example, you can increase income by 1 per second
            availMoney += income;

            // Reset the elapsed time
            elapsedTime = 0f;

        }
    }
}
