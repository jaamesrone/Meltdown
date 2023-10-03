using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

[CreateAssetMenu(fileName = "MechanicItem", menuName = "ScriptableObjects/MechanicItem")]
public class MechanicItem : ScriptableObject
{
    public bool purchased = false;
    private LunchRoom lunchRoom;
    public int uses;

    public void Uses()
    {
        if (lunchRoom.purchased == true)
        {
            uses = 2; // The number of uses the item has.
        }
        else
        {
            uses = 1; // The number of uses the item has.
        }
    }
    
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
