using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class taxation : MonoBehaviour
{
    public TextMeshProUGUI popupText;
    public float stayTime = 10f;
    public float fadeoutTime = 1.0f;

    private float currentTime;
    private bool isPopUpActive = false;

    private void Start()
    {
        currentTime = stayTime + fadeoutTime;
    }

    private void Update()
    {
        currentTime -= Time.deltaTime;

        if (currentTime < fadeoutTime && currentTime > 0)
        {
            float alpha = currentTime / fadeoutTime;
            foreach (CanvasRenderer r in GetComponentsInChildren<CanvasRenderer>())
            {
                r.SetAlpha(alpha);
            }
        }
        if (isPopUpActive)
        {
            currentTime += Time.deltaTime;
            if(currentTime>= stayTime)
            {
                Close();
            }
        }

        if (currentTime <= 0)
        {
            gameObject.SetActive(false);
        }
    }
    public void Close()
    {
        isPopUpActive = false;
        currentTime = 0;
        gameObject.SetActive(false);
    }

    public void ShowPopup(string message)
    {
        popupText.text = message;
        gameObject.SetActive(true);
    }
}
