using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SolarArray : MonoBehaviour
{
    public int solarOutput = 0; // Initial power generation per second (twice as much as the original).
    public int buttonClicked = 0; //how many times you click the button
    public int income = 0;        // Initial income per second.

    public float upgradeCost = 500;  // Initial upgrade cost.
    public float textSizeIncreaseFactor = 1.5f; // Adjust the factor to control the size increase

    public TextMeshProUGUI solarUpgradeCost;
    public GameObject solarUI;

    private ResourceManager resourceManager;

    public GameObject solarModel;
    
    private void Start()
    {
        resourceManager = GetComponent<ResourceManager>();
    }

    void Update()
    {
        if (solarUpgradeCost != null)
            solarUpgradeCost.text = "$" + upgradeCost;
        if (buttonClicked > 0)
        {
            solarModel.SetActive(true);
        }
    }

    public void resetProgress()
    {
        resourceManager.totalOutput -= solarOutput;
        resourceManager.volatility += 2f * buttonClicked;
        buttonClicked = 0;
        solarOutput = 0;
        resourceManager.solarOutput = solarOutput;
        solarOutput = 0;
        buttonClicked = 0;
        upgradeCost = 500;
        income = 0;
        solarModel.SetActive(false);
    }

    public void UpgradeSolarGenerator()
    {
        if (solarUI != null)
        {
            solarUI.SetActive(true);
            if (buttonClicked >= 10)
            {
                return;
            }
            else
            {
                if (buttonClicked <= 0)
                {
                    solarModel.SetActive(true);
                }
                upgradeOutcomeSolar();
            }
        }

    }

    public void upgradeOutcomeSolar()
    {
        income = Mathf.FloorToInt(solarOutput * resourceManager.disasterMultiplier);
        solarOutput += 100;
        resourceManager.solarOutput = solarOutput;
        resourceManager.totalOutput += 100;
        income += 100;
        resourceManager.income += 100;
        buttonClicked++;
        resourceManager.Money -= upgradeCost;
        upgradeCost *= 3f; // upgrade cost for the next level
        resourceManager.volatility -= 2f; //Decreases volatility by 2f
        StartCoroutine(AnimateTextSize());
    }

    private bool popUpOn = false;

    IEnumerator AnimateTextSize()
    {
        if (!popUpOn)
        {
            popUpOn = true; // Animation begins

            float originalSize = solarUpgradeCost.fontSize;
            float targetSize = originalSize * 2;
            float animationTime = 0.5f; // Total animation time in seconds

            float elapsedTime = 0f;

            while (elapsedTime < animationTime)
            {
                float newSize = Mathf.Lerp(originalSize, targetSize, elapsedTime / animationTime);
                solarUpgradeCost.fontSize = (int)newSize;

                elapsedTime += Time.deltaTime;
                yield return null; // Wait for the next frame
            }

            // Ensure the final size is exactly the target size
            solarUpgradeCost.fontSize = (int)targetSize;

            // Pause for a short duration before reverting back
            yield return new WaitForSeconds(0.5f);

            elapsedTime = 0f;

            while (elapsedTime < animationTime)
            {
                float newSize = Mathf.Lerp(targetSize, originalSize, elapsedTime / animationTime);
                solarUpgradeCost.fontSize = (int)newSize;

                elapsedTime += Time.deltaTime;
                yield return null; // Wait for the next frame
            }

            // Ensure the final size is exactly the original size
            solarUpgradeCost.fontSize = (int)originalSize;

            popUpOn = false; // Animation ends
        }
    }
}
