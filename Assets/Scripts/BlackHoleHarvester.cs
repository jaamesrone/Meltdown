using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BlackHoleHarvester : MonoBehaviour
{
    public int holeOutput = 0; // Initial power generation per second (twice as much as the original).
    public int buttonClicked = 0; //how many times you click the button
    public int income = 0;        // Initial income per second.

    public float upgradeCost = 1000;  // Initial upgrade cost.
    public float textSizeIncreaseFactor = 1.5f; // Adjust the factor to control the size increase

    public GameObject holeUI;
    public TextMeshProUGUI holeUpgradeCost;

    private ResourceManager resourceManager;

    public GameObject holeModel;

    private bool popUpOn = false;

    private void Start()
    {
        resourceManager = GetComponent<ResourceManager>();
    }

    void Update()
    {
        if (holeUpgradeCost != null)
            holeUpgradeCost.text = "$" + upgradeCost;
        if (buttonClicked > 0)
        {
            holeModel.SetActive(true);
        }
    }

    public void resetProgress()
    {
        resourceManager.totalOutput -= holeOutput;
        resourceManager.volatility -= 4f * buttonClicked;
        buttonClicked = 0;
        holeOutput = 0;
        resourceManager.holeOutput = holeOutput;
        holeOutput = 0;
        buttonClicked = 0;
        upgradeCost = 1000;
        income = 0;
        holeModel.SetActive(false);
    }

    public void UpgradeholeGenerator()
    {
        if (holeUI != null)
        {
            holeUI.SetActive(true);
            if (buttonClicked >= 10)
            {
                return;
            }
            else
            {
                if (buttonClicked <= 0)
                {
                    holeModel.SetActive(true);
                }
                upgradeOutcomehole();
            }
        }

    }

    public void upgradeOutcomehole()
    {
        income = Mathf.FloorToInt(holeOutput * resourceManager.disasterMultiplier);
        holeOutput += 500;
        resourceManager.holeOutput = holeOutput;
        resourceManager.totalOutput += 500;
        income += 500;
        resourceManager.income += 500;
        buttonClicked++;
        resourceManager.Money -= upgradeCost;
        upgradeCost *= 4f; // upgrade cost for the next level
        if (resourceManager.volatility != 100.0f)
        {
            resourceManager.volatility += 4f; //4f
            while (resourceManager.volatility >= 100.1f)
            {
                resourceManager.volatility -= 0.1f;
            }
        };
        StartCoroutine(AnimateTextSize());
    }

    IEnumerator AnimateTextSize()
    {
        if (!popUpOn)
        {
            popUpOn = true; // Animation begins

            float originalSize = holeUpgradeCost.fontSize;
            float targetSize = originalSize * 2;
            float animationTime = 0.5f; // Total animation time in seconds

            float elapsedTime = 0f;

            while (elapsedTime < animationTime)
            {
                float newSize = Mathf.Lerp(originalSize, targetSize, elapsedTime / animationTime);
                holeUpgradeCost.fontSize = (int)newSize;

                elapsedTime += Time.deltaTime;
                yield return null; // Wait for the next frame
            }

            // Ensure the final size is exactly the target size
            holeUpgradeCost.fontSize = (int)targetSize;

            // Pause for a short duration before reverting back
            yield return new WaitForSeconds(0.5f);

            elapsedTime = 0f;

            while (elapsedTime < animationTime)
            {
                float newSize = Mathf.Lerp(targetSize, originalSize, elapsedTime / animationTime);
                holeUpgradeCost.fontSize = (int)newSize;

                elapsedTime += Time.deltaTime;
                yield return null; // Wait for the next frame
            }

            // Ensure the final size is exactly the original size
            holeUpgradeCost.fontSize = (int)originalSize;

            popUpOn = false; // Animation ends
        }
    }
}
