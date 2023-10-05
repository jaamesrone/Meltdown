using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public GameObject tutorialButton;
    public GameObject tutorialScreen;

    private bool isTutorialVisible = false;

    private GameObject[] hiddenObjects;

    // Start is called before the first frame update
    void Start()
    {
        tutorialScreen.SetActive(false);

        hiddenObjects = GameObject.FindObjectsOfType<GameObject>();
        foreach (GameObject obj in hiddenObjects)
        {
            if (!obj.activeSelf)
            {
                obj.SetActive(true);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject == tutorialButton)
                {
                    // The TutorialButton was clicked.
                    ToggleTutorialScreen();
                }
            }
        }
    }

    private void ToggleTutorialScreen()
    {
        isTutorialVisible = !isTutorialVisible;

        if (isTutorialVisible)
        {
            // Hide all objects except for the TutorialButton and TutorialScreen.
            HideAllObjectsExcept(tutorialButton, tutorialScreen);
            tutorialScreen.SetActive(true);
        }
        else
        {
            // Show all originally hidden objects and hide the TutorialScreen.
            foreach (GameObject obj in hiddenObjects)
            {
                if (!obj.activeSelf)
                {
                    obj.SetActive(false);
                }
            }
            tutorialScreen.SetActive(false);
        }
    }

    private void HideAllObjectsExcept(params GameObject[] exceptions)
    {
        GameObject[] allObjects = GameObject.FindObjectsOfType<GameObject>();

        foreach (GameObject obj in allObjects)
        {
            if (obj != tutorialButton && obj != tutorialScreen && !ArrayContains(exceptions, obj))
            {
                obj.SetActive(false);
            }
        }
    }

    private void ShowAllHiddenObjects()
    {
        GameObject[] allObjects = GameObject.FindObjectsOfType<GameObject>();

        foreach (GameObject obj in allObjects)
        {
            if (!obj.activeSelf)
            {
                obj.SetActive(true);
            }
        }
    }

    private bool ArrayContains(GameObject[] array, GameObject obj)
    {
        foreach (GameObject item in array)
        {
            if (item == obj)
            {
                return true;
            }
        }
        return false;
    }
}
