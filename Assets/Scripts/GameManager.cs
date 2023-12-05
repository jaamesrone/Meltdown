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

    private bool isBlinkActive = false;

    private float alertDuration = 3;
    private float blinkerTimer = 0f;
    private float blinkerInterval = 0.5f;

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
            waterWheelText.text = "Water Wheel\nOutput: " + resourceManager.waterWheelOutput.ToString();

        if (bikeOutputText != null)
            bikeOutputText.text = "Bike\nOutput: " + resourceManager.bikeOutput.ToString();

        if (dutchOutputText != null)
            dutchOutputText.text = "Dutch Windmill\nOutput: " + resourceManager.dutchOutput.ToString();

        if (coalOutputText != null)
            coalOutputText.text = "Coal Plant\nOutput: " + resourceManager.coalOutput.ToString();

        if (coolingOutputText != null)
            coolingOutputText.text = "Cooling System\nOutput: " + resourceManager.coolingOutput.ToString();

        if (hydroOutputText != null)
            hydroOutputText.text = "Hydroelectric Dam\nOutput: " + resourceManager.hydroOutput.ToString();

        if (electricalOutputText != null)
            electricalOutputText.text = "Electric Windmill\nOutput: " + resourceManager.electricalOutput.ToString();

        if (solarOutputText != null)
            solarOutputText.text = "Solar Farm\nOutput: " + resourceManager.solarOutput.ToString();

        if (nuclearOutputText != null)
            nuclearOutputText.text = "Nuclear Plant\nOutput: " + resourceManager.nuclearOutput.ToString();
    }
   

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
        isBlinkActive = true;

        // Display the disaster message for a specific duration
        yield return new WaitForSeconds(alertDuration);

        // Hide the disaster message
        DisasterTexts.text = "";

        isBlinkActive = false;

    }

    public void BlinkerEffect()
    {
        if (isBlinkActive)
        {
            // Update the timer
            blinkerTimer += Time.deltaTime;

            // Toggle the visibility of eventText based on the timer and interval
            if (blinkerTimer >= blinkerInterval)
            {
                DisasterTexts.enabled = !DisasterTexts.enabled;
                blinkerTimer = 0f; // Reset the timer
            }
        }
        else
        {
            // If blinking is not active, keep eventText disabled
            DisasterTexts.enabled = false;
        }
    }
}
