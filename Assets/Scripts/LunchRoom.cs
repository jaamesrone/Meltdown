using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "LunchRoom", menuName = "ScriptableObjects/LunchRoom")]
public class LunchRoom : ScriptableObject
{
    public bool purchased = false;
    public int uses = 1; // The number of uses the item has.

    public void Buy()
    {
        purchased = true;  
    }
}
