using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "PowerBreaker", menuName = "ScriptableObjects/Powerbreaker")]
public class PowerBreaker : ScriptableObject
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
