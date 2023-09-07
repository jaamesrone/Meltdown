using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disasters : MonoBehaviour
{
    private ResourceManager resourceManager;

    // Start is called before the first frame update
    void Start()
    {
        resourceManager = GetComponent<ResourceManager>();
    }

    // Update is called once per frame
    void Update()
    {
        // Generates a random float between 0.1 and 100.1 (excluding 100.1)
        float randomValue = Random.Range(0.1f, 100.1f);

        // Checks if the random value is equal to the volatility float variable
        if (Mathf.Approximately(randomValue, resourceManager.volatility))
        {

        }
    }

    public void disasterOccurs()
    {
        print("A disaster has occurred!");
    }
}
