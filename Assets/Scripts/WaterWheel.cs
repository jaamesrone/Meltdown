using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterWheel : MonoBehaviour
{
    public int powerOutput = 2; // Initial power generation per second (twice as much as the original).

    private ResourceManager resourceManager;

    private void Start()
    {
        resourceManager = GetComponent<ResourceManager>();
        resourceManager.powerOutput += powerOutput;
    }
}
