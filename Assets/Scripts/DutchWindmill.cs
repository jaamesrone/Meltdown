using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DutchWindmill : MonoBehaviour
{
    public int dutchOutput = 0; // Initial power generation per second (twice as much as the original).
    public int buttonClicked = 0; //how many times you click the button
    public int income = 0;        // Initial income per second.

    public float upgradeCost = 100;  // Initial upgrade cost.
    public float textSizeIncreaseFactor = 1.5f; // Adjust the factor to control the size increase
    
    public GameObject dutchUI;

    public TextMeshProUGUI dutchUpgradeCost;

    private ResourceManager resourceManager;

    public GameObject dutchModel;

    private void Start()
    {
        resourceManager = GetComponent<ResourceManager>();
    }

    private void Update()
    {
        if (dutchUpgradeCost != null)
            dutchUpgradeCost.text = "$" + upgradeCost;
        if (buttonClicked > 0)
        {
            dutchModel.SetActive(true);
        }
    }

    public void resetProgress()
    {
        resourceManager.totalOutput -= dutchOutput;
        resourceManager.volatility -= 1f * buttonClicked;
        buttonClicked = 0;
        dutchOutput = 0;
        resourceManager.dutchOutput = dutchOutput;
        dutchOutput = 0;
        buttonClicked = 0;
        upgradeCost = 100;
        income = 0;
        dutchModel.SetActive(false);
    }

    public void UpgradeDutchGenerator()
    {
        if (dutchUI != null)
        {
            dutchUI.SetActive(true);
            if (buttonClicked >= 10)
            {
                return;
            }
            else
            {
                if (buttonClicked <= 0)
                {
                    dutchModel.SetActive(true);
                }
                upgradeOutcomeDutch();
            }
        }

    }

    public void upgradeOutcomeDutch()
    {
        upgradeProgress();
                          
        if (resourceManager.volatility != 100.0f)
        {
            resourceManager.volatility += 1f; //1f
            while (resourceManager.volatility >= 100.1f)
            {
                resourceManager.volatility -= 0.1f;
            }
        }
    }

    public void upgradeProgress()
    {
        income = Mathf.FloorToInt(dutchOutput * resourceManager.disasterMultiplier);
        dutchOutput += 10;
        resourceManager.dutchOutput = dutchOutput;
        resourceManager.totalOutput += 10;
        income += 10;
        resourceManager.income += 10;
        buttonClicked++;
        resourceManager.Money -= upgradeCost;
        upgradeCost *= 1.5f; // upgrade cost for the next level
        StartCoroutine(AnimateTextSize());
    }

    private bool popUpOn = false;

    IEnumerator AnimateTextSize()
    {
        if (!popUpOn)
        {
            popUpOn = true; // Animation begins

            float originalSize = dutchUpgradeCost.fontSize;
            float targetSize = originalSize * 2;
            float animationTime = 0.5f; // Total animation time in seconds

            float elapsedTime = 0f;

            while (elapsedTime < animationTime)
            {
                float newSize = Mathf.Lerp(originalSize, targetSize, elapsedTime / animationTime);
                dutchUpgradeCost.fontSize = (int)newSize;

                elapsedTime += Time.deltaTime;
                yield return null; // Wait for the next frame
            }

            // Ensure the final size is exactly the target size
            dutchUpgradeCost.fontSize = (int)targetSize;

            // Pause for a short duration before reverting back
            yield return new WaitForSeconds(0.5f);

            elapsedTime = 0f;

            while (elapsedTime < animationTime)
            {
                float newSize = Mathf.Lerp(targetSize, originalSize, elapsedTime / animationTime);
                dutchUpgradeCost.fontSize = (int)newSize;

                elapsedTime += Time.deltaTime;
                yield return null; // Wait for the next frame
            }

            // Ensure the final size is exactly the original size
            dutchUpgradeCost.fontSize = (int)originalSize;

            popUpOn = false; // Animation ends
        }
    }
}
