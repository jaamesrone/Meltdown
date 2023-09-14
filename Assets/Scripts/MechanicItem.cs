using UnityEngine;

[CreateAssetMenu(fileName = "New Mechanic Item", menuName = "Inventory/Mechanic Item")]
public class MechanicItem : ScriptableObject
{
    public string itemName = "Mechanic Item";
    public int maxUses = 1; // Number of times the item can be used
}
