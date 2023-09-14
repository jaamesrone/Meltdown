using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<MechanicItem> mechanicItems = new List<MechanicItem>();

    public void BuyMechanicItem(MechanicItem item)
    {
        if (!mechanicItems.Contains(item) || item.maxUses <= 1)
        {
            // The item is not in the inventory or has been used up; add it to the inventory
            mechanicItems.Add(item);
        }
    }


    public void UseMechanicItem(MechanicItem item)
    {
        if (mechanicItems.Contains(item) && item.maxUses > 0)
        {
            // Perform mechanic-related actions here
            // For example, prevent disasters or apply a temporary buff

            // Deduct a use from the mechanic item.
            item.maxUses--;

            Debug.Log("Mechanic item used. Remaining uses: " + item.maxUses);

            // If the item has no more uses left, remove it from the inventory.
            if (item.maxUses <= 1)
            {
                mechanicItems.Remove(item);
                Debug.Log("Mechanic item removed from inventory.");
            }
        }
    }



    public bool CanBuyMechanicItem(MechanicItem item)
    {
        return !mechanicItems.Contains(item) || item.maxUses <= 0;
    }
}
