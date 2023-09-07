using UnityEngine;
using TMPro;

public class ResourceManager : MonoBehaviour
{
    public int totalOutput;
    public int waterWheelOutput;
    public int income;
    public int availMoney;
    public int volatility;
    public int bikeOutput;
    
    public GameObject waterWheelUpgradeButton;

    private Bike bike;

    // Variables for tracking time and perSecond.
    private float elapsedTime = 0f;
    public float perSecond = 1f; // Update every 1 second

    // Start is called before the first frame update
    void Start()
    {
        // Initialize the values
        totalOutput = 1;
        income = 1;
        availMoney = 100;
        volatility = 0;
        waterWheelOutput = 0;
        bikeOutput = 0;
        //if you ever need to just test the code availMoney = 10 so you dont forget.
        //go to PowerGenerator script to look for what to change next.

        bike = GetComponent<Bike>();
    }

    // Update is called once per frame
    void Update()
    {
        incomeStarter();
    }

    // This method should be called when the upgrade button is clicked.
    public void UpgradeButtonClicked()
    {
        bike.UpgradePowerGenerator();
    }

    public void incomeStarter()
    {
        // Update the elapsed time
        elapsedTime += Time.deltaTime;

        // Check if it's time to update the values (once per second)
        if (elapsedTime >= perSecond)
        {
            // For example, you can increase income by income int
            availMoney += income;

            if (availMoney >= 1000 && !waterWheelUpgradeButton.activeSelf) //if the players income is over 1000, button unhides.
            {
                Debug.Log("hi");
                waterWheelUpgradeButton.SetActive(true);
            }

            // Reset the elapsed time
            elapsedTime = 0f;
        }
    }
}
