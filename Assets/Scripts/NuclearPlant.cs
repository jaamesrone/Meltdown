using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NuclearPlant : MonoBehaviour
{
    public int nuclearOutput = 0; // Initial power generation per second (twice as much as the original).
    public int buttonClicked = 0; //how many times you click the button
    public int income = 0;        // Initial income per second.

    public float upgradeCost = 600;  // Initial upgrade cost.
    public float textSizeIncreaseFactor = 1.5f; // Adjust the factor to control the size increase

    public GameObject nuclearUI;
    public TextMeshProUGUI nuclearUpgradeCost;

    private ResourceManager resourceManager;

    public GameObject nuclearModel;

    private void Start()
    {
        resourceManager = GetComponent<ResourceManager>();
    }

    void Update()
    {
        if (nuclearUpgradeCost != null)
            nuclearUpgradeCost.text = "$" + upgradeCost;
        if (buttonClicked > 0)
        {
            nuclearModel.SetActive(true);
        }
    }

    public void resetProgress()
    {
        resourceManager.totalOutput -= nuclearOutput;
        resourceManager.volatility -= 3f * buttonClicked;
        buttonClicked = 0;
        nuclearOutput = 0;
        resourceManager.nuclearOutput = nuclearOutput;
        nuclearOutput = 0;
        buttonClicked = 0;
        upgradeCost = 600;
        income = 0;
        nuclearModel.SetActive(false);
    }

    public void UpgradeNuclearGenerator()
    {
        if (nuclearUI != null)
        {
            nuclearUI.SetActive(true);
            if (buttonClicked >= 10)
            {
                return;
            }
            else
            {
                if (buttonClicked <= 0)
                {
                    nuclearModel.SetActive(true);
                }
                upgradeOutcomeNuclear();
            }
        }

    }

    public void upgradeOutcomeNuclear()
    {
        income = Mathf.FloorToInt(nuclearOutput * resourceManager.disasterMultiplier);
        nuclearOutput += 200;
        resourceManager.nuclearOutput = nuclearOutput;
        resourceManager.totalOutput += 200;
        income += 200;
        resourceManager.income += 200;
        buttonClicked++;
        resourceManager.Money -= upgradeCost;
        upgradeCost *= 3f; // upgrade cost for the next level
        if (resourceManager.volatility != 100.0f)
        {
            resourceManager.volatility += 3f; //3f
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

            float originalSize = nuclearUpgradeCost.fontSize;
            float targetSize = originalSize * 2;
            float animationTime = 0.5f; // Total animation time in seconds

            float elapsedTime = 0f;

            while (elapsedTime < animationTime)
            {
                float newSize = Mathf.Lerp(originalSize, targetSize, elapsedTime / animationTime);
                nuclearUpgradeCost.fontSize = (int)newSize;

                elapsedTime += Time.deltaTime;
                yield return null; // Wait for the next frame
            }

            // Ensure the final size is exactly the target size
            nuclearUpgradeCost.fontSize = (int)targetSize;

            // Pause for a short duration before reverting back
            yield return new WaitForSeconds(0.5f);

            elapsedTime = 0f;

            while (elapsedTime < animationTime)
            {
                float newSize = Mathf.Lerp(targetSize, originalSize, elapsedTime / animationTime);
                nuclearUpgradeCost.fontSize = (int)newSize;

                elapsedTime += Time.deltaTime;
                yield return null; // Wait for the next frame
            }

            // Ensure the final size is exactly the original size
            nuclearUpgradeCost.fontSize = (int)originalSize;

            popUpOn = false; // Animation ends
        }
    }
}
