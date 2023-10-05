using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class generatorIndicator : MonoBehaviour
{
    [System.Serializable]
    public class Generator
    {
        public string generatorName;
        public bool isBuilt;
        public Sprite generatorImage; // Add an Image field for the generator image.
    }

    public Generator[] generators;
    public Image[] generatorImages; // Use Image components for the generator images.
    public Button[] generatorButtons;

    private void Start()
    {

        UpdateGeneratorIndicator();
    }

    private void UpdateGeneratorIndicator()
    {
        for (int i = 0; i < generators.Length; i++)
        {
            Generator generator = generators[i];
            Image image = generatorImages[i]; // Reference to the Image component.
            Button button = generatorButtons[i];


            image.gameObject.SetActive(false);

            // Check if the generator is built
            if (generator.isBuilt)
            {
                // Generator is built, set the image to the generator's image and enable the button.
                image.gameObject.SetActive(true);
                image.sprite = generator.generatorImage;
                button.interactable = true;
            }
            else
            {
                // Generator is not built, set the image to a placeholder or hide it, and enable the button for buying.
                image.sprite = null; // You can set this to a placeholder sprite.
                button.interactable = true; // Enable the button to make it clickable for buying.
                button.onClick.AddListener(() => OnGeneratorButtonClick(generator, image));
            }
        }
    }

    private void OnGeneratorButtonClick(Generator generator, Image image)
    {
        // Handle the generator button click here (e.g., purchase the generator).
        // After purchasing, set generator.isBuilt = true and call UpdateGeneratorIndicator() to update the UI.
        generator.isBuilt = true;
        UpdateGeneratorIndicator();
    }
}

