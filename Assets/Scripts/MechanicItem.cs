using UnityEngine;

[CreateAssetMenu(fileName = "MechanicItem", menuName = "ScriptableObjects/MechanicItem")]
public class MechanicItem : ScriptableObject
{
    public bool purchased = false;
    public int uses = 1; // The number of uses the item has.

    public void Buy()
    {
        purchased = true;
    }

    public void Use()
    {
        if (uses > 0)
        {
            uses--;
        }
    }

    public bool CanUse()
    {
        return purchased && uses > 0;
    }
}
