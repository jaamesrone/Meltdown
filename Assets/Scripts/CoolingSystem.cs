using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoolingSystem : MonoBehaviour
{
    public int coolingOutput = 0; // Initial power generation per second (twice as much as the original).
    public int buttonClicked = 0; //how many times you click the button
    public int income = 0;        // Initial income per second.

    public float upgradeCost = 200;  // Initial upgrade cost.
    public float textSizeIncreaseFactor = 1.5f; // Adjust the factor to control the size increase

    public GameObject coolingUI;
    public TextMeshProUGUI coolingUpgradeCost;

    private ResourceManager resourceManager;

    public GameObject coolingModel;

    private bool popUpOn = false;

    private void Start()
    {
        resourceManager = GetComponent<ResourceManager>();
    }

    void Update()
    {
        if (coolingUpgradeCost != null)
            coolingUpgradeCost.text = "$" + upgradeCost;
        if (buttonClicked > 0)
        {
            coolingModel.SetActive(true);
        }
    }

    public void resetProgress()
    {
        resourceManager.totalOutput -= coolingOutput;
        resourceManager.volatility += 1f * buttonClicked;
        buttonClicked = 0;
        coolingOutput = 0;
        resourceManager.coolingOutput = coolingOutput;
        coolingOutput = 0;
        buttonClicked = 0;
        upgradeCost = 200;
        income = 0;
        coolingModel.SetActive(false);
    }

    public void UpgradeCoolingGenerator()
    {
        if (coolingUI != null)
        {
            coolingUI.SetActive(true);
            if (buttonClicked >= 10)
            {
                return;
            }
            else
            {
                if (buttonClicked <= 0)
                {
                    coolingModel.SetActive(true);
                }
                upgradeOutcomeCooling();
            }
        }

    }

    public void upgradeOutcomeCooling()
    {
        income = Mathf.FloorToInt(coolingOutput * resourceManager.disasterMultiplier);
        coolingOutput -= 10;
        resourceManager.coolingOutput = coolingOutput;
        resourceManager.totalOutput -= 10;
        income -= 10;
        resourceManager.income -= 10;
        buttonClicked++;
        resourceManager.Money -= upgradeCost;
        upgradeCost *= 2f; // upgrade cost for the next level
        resourceManager.volatility -= 1f; //Lowers volatility by 1f
        StartCoroutine(AnimateTextSize());
    }

    IEnumerator AnimateTextSize()
    {
        if (!popUpOn)
        {
            popUpOn = true; // Animation begins

            float originalSize = coolingUpgradeCost.fontSize;
            float targetSize = originalSize * 2;
            float animationTime = 0.5f; // Total animation time in seconds

            float elapsedTime = 0f;

            while (elapsedTime < animationTime)
            {
                float newSize = Mathf.Lerp(originalSize, targetSize, elapsedTime / animationTime);
                coolingUpgradeCost.fontSize = (int)newSize;

                elapsedTime += Time.deltaTime;
                yield return null; // Wait for the next frame
            }

            // Ensure the final size is exactly the target size
            coolingUpgradeCost.fontSize = (int)targetSize;

            // Pause for a short duration before reverting back
            yield return new WaitForSeconds(0.5f);

            elapsedTime = 0f;

            while (elapsedTime < animationTime)
            {
                float newSize = Mathf.Lerp(targetSize, originalSize, elapsedTime / animationTime);
                coolingUpgradeCost.fontSize = (int)newSize;

                elapsedTime += Time.deltaTime;
                yield return null; // Wait for the next frame
            }

            // Ensure the final size is exactly the original size
            coolingUpgradeCost.fontSize = (int)originalSize;

            popUpOn = false; // Animation ends
        }
    }
}
