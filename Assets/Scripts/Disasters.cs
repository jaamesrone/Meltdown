using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disasters : MonoBehaviour
{
    private ResourceManager resourceManager;
    public GameObject disasterAlert;
    private bool disasterAlertActive = false;
    private float alertDuration = 10f;

    // Start is called before the first frame update
    void Start()
    {
        resourceManager = GetComponent<ResourceManager>();
        disasterAlert.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // Generates a random float between 0.1 and 100.1 (excluding 100.1)
        float randomValue = Random.Range(0.1f, 100.1f);

        // Checks if the random value is equal to the volatility float variable
        if (Mathf.Approximately(randomValue, resourceManager.volatility))
        {
            disasterOccurs();
        }
    }

    public void disasterOccurs()
    {
        if (!disasterAlertActive)
        {
            // Shows the disasterAlert GameObject.
            disasterAlert.SetActive(true);
            disasterAlertActive = true;

            // Hides the disasterAlert GameObject after ten seconds.
            StartCoroutine(HideAlertAfterDelay(alertDuration));
        }
    }

    private IEnumerator HideAlertAfterDelay(float delay)
    {
        // Wait for the specified delay in seconds.
        yield return new WaitForSeconds(delay);

        // Hide the DisasterAlert GameObject.
        disasterAlert.SetActive(false);
        disasterAlertActive = false;
    }
}
