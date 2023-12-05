using UnityEngine;
using TMPro;
using System.Collections;

public class Bike : MonoBehaviour
{
    public int buttonClicked = 0;
    public int bikeOutput = 0;   // Initial power generation per second.
    public int income =0;        // Initial income per second.

    public float upgradeCost = 50;  // Initial upgrade cost.
    public float textSizeIncreaseFactor = 1.5f; // Adjust the factor to control the size increase

    private ResourceManager resourceManager;
    public TextMeshProUGUI bikeUpgradeCost;

    public GameObject bikeModel;

    private bool popUpOn = false;

    void Start()
    {
        resourceManager = GetComponent<ResourceManager>();
    }

    void Update()
    {
        if (bikeUpgradeCost != null)
            bikeUpgradeCost.text = "$" + upgradeCost;
        if (buttonClicked > 0)
        {
            bikeModel.SetActive(true);
        }
    }

    public void resetProgress()
    {
        resourceManager.totalOutput -= bikeOutput;
        resourceManager.volatility -= 0.2f * buttonClicked;
        buttonClicked = 0;
        bikeOutput = 0;
        resourceManager.bikeOutput = bikeOutput;
        income = 0;
        upgradeCost = 50;
        bikeModel.SetActive(false);
    }

    public void UpgradePowerGenerator()
    {
        if (buttonClicked >= 10)
        {
            return;
        }
        if (resourceManager.Money >= upgradeCost)
        {
            if (buttonClicked <= 0)
            {
                bikeModel.SetActive(true);
            }
            upgradeOutcomeBike();
        }
    }

    public void upgradeOutcomeBike()
    {
        upgradeProgress();
        if (resourceManager.volatility != 100.0f)
        {
            resourceManager.volatility += 0.2f; //0.2f
            while (resourceManager.volatility >= 100.1f)
            {
                resourceManager.volatility -= 0.1f;
            }
        }
    }

    public void upgradeProgress()
    {
        income = Mathf.FloorToInt(bikeOutput * resourceManager.disasterMultiplier);
        bikeOutput++; // increase the powerOutput.
        resourceManager.totalOutput++;
        resourceManager.bikeOutput = bikeOutput;
        income += 1;     // Double the income.
        resourceManager.income += 1;
        buttonClicked++;
        resourceManager.Money -= upgradeCost;
        upgradeCost *= 1.5f; // upgrade cost for the next level.
        StartCoroutine(AnimateTextSize());
    }

    IEnumerator AnimateTextSize()
    {
        if (!popUpOn)
        {
            popUpOn = true; // Animation begins

            float originalSize = bikeUpgradeCost.fontSize;
            float targetSize = originalSize * 2;
            float animationTime = 0.5f; // Total animation time in seconds

            float elapsedTime = 0f;

            while (elapsedTime < animationTime)
            {
                float newSize = Mathf.Lerp(originalSize, targetSize, elapsedTime / animationTime);
                bikeUpgradeCost.fontSize = (int)newSize;

                elapsedTime += Time.deltaTime;
                yield return null; // Wait for the next frame
            }

            // Ensure the final size is exactly the target size
            bikeUpgradeCost.fontSize = (int)targetSize;

            // Pause for a short duration before reverting back
            yield return new WaitForSeconds(0.5f);

            elapsedTime = 0f;

            while (elapsedTime < animationTime)
            {
                float newSize = Mathf.Lerp(targetSize, originalSize, elapsedTime / animationTime);
                bikeUpgradeCost.fontSize = (int)newSize;

                elapsedTime += Time.deltaTime;
                yield return null; // Wait for the next frame
            }

            // Ensure the final size is exactly the original size
            bikeUpgradeCost.fontSize = (int)originalSize;

            popUpOn = false; // Animation ends
        }
    }

}
