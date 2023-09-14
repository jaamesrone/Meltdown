using UnityEngine;

public class Mechanic : MonoBehaviour
{
    public int purchaseCount = 0; // Keep track of how many times the item has been purchased
    private int cost = 20000; // Cost of the item
    public int maxPurchaseCount = 3; // Maximum number of times the item can be purchased

    private ResourceManager resourceManager;

    public GameObject mechanicButton;

    private void Start()
    {
        // Find the ResourceManager script in the scene
        resourceManager = FindObjectOfType<ResourceManager>();
    }

    public void UpgradeMechanic()
    {
        if (purchaseCount >= maxPurchaseCount)
        {
            // Maximum purchase count reached, do not allow further upgrades
            return;
        }

        if (resourceManager.availMoney >= cost && resourceManager.volatility > 0)
        {
            // Deduct the cost from AvailMoney
            resourceManager.availMoney -= cost;

            // Call the method to reduce volatility in ResourceManager
            resourceManager.ReduceVolatility();

            // Increment the purchase count
            purchaseCount++;

            // Handle max purchase count logic if needed
            if (purchaseCount >= maxPurchaseCount)
            {
                mechanicButton.SetActive(false);
            }
        }
    }
}
