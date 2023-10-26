using UnityEngine;

public class BackupGenerator : MonoBehaviour
{
    public int powerOutput = 5; // Power output per activation.
    public ResourceManager resourceManager;
    public GameObject backupGeneratorItem; // Reference to the Backup Generator object
    public int backupGeneratorCost = 100;

    private bool isActivated = false;
    private float activationTime;

    public void ActivateBackUpGenerator()
    {
        if (!isActivated)
        {
            isActivated = true;
            resourceManager.totalOutput += powerOutput;
            resourceManager.income += powerOutput;

            // Set the activation time for 3 minutes from now
            activationTime = Time.time + 180.0f; // 3 minutes = 180 seconds

            // Start the timer
            Invoke("DeactivateBackUpGenerator", 180.0f);
        }
    }

    public void DeactivateBackUpGenerator()
    {
        isActivated = false;
        resourceManager.totalOutput -= powerOutput;
        resourceManager.income -= powerOutput;
        resourceManager.backUpGeneratorBought = false;

        // Add any other deactivation logic here if needed.

        // Cancel the timer
        CancelInvoke("DeactivateBackUpGenerator");
    }

    public bool IsActivated()
    {
        return isActivated;
    }

    public void BuyBackupGenerator()
    {
        if (resourceManager.Money >= backupGeneratorCost)
        {

            resourceManager.backUpGeneratorBought = true;
            // Deduct the cost from the player's money
            resourceManager.Money -= backupGeneratorCost;

            // Set the Backup Generator as active in the ResourceManager
            resourceManager.backupGenerator = this;

            // Deactivate the Backup Generator item in the shop
            backupGeneratorItem.SetActive(false);
        }
    }
}
