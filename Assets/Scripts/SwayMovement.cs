using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwayMovement : MonoBehaviour
{
    //Sway Amount 
    [SerializeField] private float swayAmount = 5f;
    //Sway Speed
    [SerializeField] private float swaySpeed = 1f;
    private Vector3 initialPosition;

    private void Start()
    {
        initialPosition = transform.localPosition;
    }

    private void Update()
    {

        float horizontalSway = Mathf.Sin(Time.time * swaySpeed) * swayAmount;

        // Apply the sway to the icon's position.
        Vector3 newPosition = initialPosition + new Vector3(horizontalSway, 0f, 0f);
        transform.localPosition = newPosition;
    }
}
