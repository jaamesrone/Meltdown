using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DivineHamsterWheel : MonoBehaviour
{
    public int hamsterOutput = 0; // Initial power generation per second (twice as much as the original).
    public int buttonClicked = 0; //how many times you click the button
    public int income = 0;        // Initial income per second.

    public float upgradeCost = 2000;  // Initial upgrade cost.
    public float textSizeIncreaseFactor = 1.5f; // Adjust the factor to control the size increase

    public GameObject hamsterUI;
    public TextMeshProUGUI hamsterUpgradeCost;

    private ResourceManager resourceManager;

    public GameObject hamsterModel;

    private bool popUpOn = false;

    private void Start()
    {
        resourceManager = GetComponent<ResourceManager>();
    }

    void Update()
    {
        if (hamsterUpgradeCost != null)
            hamsterUpgradeCost.text = "$" + upgradeCost;
        if (buttonClicked > 0)
        {
            hamsterModel.SetActive(true);
        }
    }

    public void resetProgress()
    {
        resourceManager.totalOutput -= hamsterOutput;
        buttonClicked = 0;
        hamsterOutput = 0;
        resourceManager.hamsterOutput = hamsterOutput;
        hamsterOutput = 0;
        buttonClicked = 0;
        upgradeCost = 2000;
        income = 0;
        hamsterModel.SetActive(false);
    }

    public void UpgradehamsterGenerator()
    {
        if (hamsterUI != null)
        {
            hamsterUI.SetActive(true);
            if (buttonClicked >= 10)
            {
                return;
            }
            else
            {
                if (buttonClicked <= 0)
                {
                    hamsterModel.SetActive(true);
                }
                upgradeOutcomehamster();
            }
        }

    }

    public void upgradeOutcomehamster()
    {
        income = Mathf.FloorToInt(hamsterOutput * resourceManager.disasterMultiplier);
        hamsterOutput += 1000;
        resourceManager.hamsterOutput = hamsterOutput;
        resourceManager.totalOutput += 1000;
        income += 1000;
        resourceManager.income += 1000;
        buttonClicked++;
        resourceManager.Money -= upgradeCost;
        upgradeCost *= 4f; // upgrade cost for the next level
        StartCoroutine(AnimateTextSize());
    }

    IEnumerator AnimateTextSize()
    {
        if (!popUpOn)
        {
            popUpOn = true; // Animation begins

            float originalSize = hamsterUpgradeCost.fontSize;
            float targetSize = originalSize * 2;
            float animationTime = 0.5f; // Total animation time in seconds

            float elapsedTime = 0f;

            while (elapsedTime < animationTime)
            {
                float newSize = Mathf.Lerp(originalSize, targetSize, elapsedTime / animationTime);
                hamsterUpgradeCost.fontSize = (int)newSize;

                elapsedTime += Time.deltaTime;
                yield return null; // Wait for the next frame
            }

            // Ensure the final size is exactly the target size
            hamsterUpgradeCost.fontSize = (int)targetSize;

            // Pause for a short duration before reverting back
            yield return new WaitForSeconds(0.5f);

            elapsedTime = 0f;

            while (elapsedTime < animationTime)
            {
                float newSize = Mathf.Lerp(targetSize, originalSize, elapsedTime / animationTime);
                hamsterUpgradeCost.fontSize = (int)newSize;

                elapsedTime += Time.deltaTime;
                yield return null; // Wait for the next frame
            }

            // Ensure the final size is exactly the original size
            hamsterUpgradeCost.fontSize = (int)originalSize;

            popUpOn = false; // Animation ends
        }
    }
}
