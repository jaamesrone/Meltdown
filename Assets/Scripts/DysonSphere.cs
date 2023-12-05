using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DysonSphere : MonoBehaviour
{
    public int dysonOutput = 0; // Initial power generation per second (twice as much as the original).
    public int buttonClicked = 0; //how many times you click the button
    public int income = 0;        // Initial income per second.

    public float upgradeCost = 800;  // Initial upgrade cost.
    public float textSizeIncreaseFactor = 1.5f; // Adjust the factor to control the size increase

    public GameObject dysonUI;
    public TextMeshProUGUI dysonUpgradeCost;

    private ResourceManager resourceManager;

    public GameObject dysonModel;

    private void Start()
    {
        resourceManager = GetComponent<ResourceManager>();
    }

    void Update()
    {
        if (dysonUpgradeCost != null)
            dysonUpgradeCost.text = "$" + upgradeCost;
        if (buttonClicked > 0)
        {
            dysonModel.SetActive(true);
        }
    }

    public void resetProgress()
    {
        resourceManager.totalOutput -= dysonOutput;
        resourceManager.volatility -= 3.5f * buttonClicked;
        buttonClicked = 0;
        dysonOutput = 0;
        resourceManager.dysonOutput = dysonOutput;
        dysonOutput = 0;
        buttonClicked = 0;
        upgradeCost = 800;
        income = 0;
        dysonModel.SetActive(false);
    }

    public void UpgradedysonGenerator()
    {
        if (dysonUI != null)
        {
            dysonUI.SetActive(true);
            if (buttonClicked >= 10)
            {
                return;
            }
            else
            {
                if (buttonClicked <= 0)
                {
                    dysonModel.SetActive(true);
                }
                upgradeOutcomedyson();
            }
        }

    }

    public void upgradeOutcomedyson()
    {
        income = Mathf.FloorToInt(dysonOutput * resourceManager.disasterMultiplier);
        dysonOutput += 300;
        resourceManager.dysonOutput = dysonOutput;
        resourceManager.totalOutput += 300;
        income += 300;
        resourceManager.income += 300;
        buttonClicked++;
        resourceManager.Money -= upgradeCost;
        upgradeCost *= 4f; // upgrade cost for the next level
        if (resourceManager.volatility != 100.0f)
        {
            resourceManager.volatility += 3.5f; //3.5f
            while (resourceManager.volatility >= 100.1f)
            {
                resourceManager.volatility -= 0.1f;
            }
        };
        StartCoroutine(AnimateTextSize());
    }

    private bool popUpOn = false;

    IEnumerator AnimateTextSize()
    {
        if (!popUpOn)
        {
            popUpOn = true; // Animation begins

            float originalSize = dysonUpgradeCost.fontSize;
            float targetSize = originalSize * 2;
            float animationTime = 0.5f; // Total animation time in seconds

            float elapsedTime = 0f;

            while (elapsedTime < animationTime)
            {
                float newSize = Mathf.Lerp(originalSize, targetSize, elapsedTime / animationTime);
                dysonUpgradeCost.fontSize = (int)newSize;

                elapsedTime += Time.deltaTime;
                yield return null; // Wait for the next frame
            }

            // Ensure the final size is exactly the target size
            dysonUpgradeCost.fontSize = (int)targetSize;

            // Pause for a short duration before reverting back
            yield return new WaitForSeconds(0.5f);

            elapsedTime = 0f;

            while (elapsedTime < animationTime)
            {
                float newSize = Mathf.Lerp(targetSize, originalSize, elapsedTime / animationTime);
                dysonUpgradeCost.fontSize = (int)newSize;

                elapsedTime += Time.deltaTime;
                yield return null; // Wait for the next frame
            }

            // Ensure the final size is exactly the original size
            dysonUpgradeCost.fontSize = (int)originalSize;

            popUpOn = false; // Animation ends
        }
    }
}
