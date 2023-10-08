using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextHolderManager : MonoBehaviour
{
    public Text textToDisplay;
    public Image textHolderImage;
    private float scaleSpeed = 10f;

    // Call this method to set the size of textHolderImage based on the text size
    public void SetTextHolderSizeAccordingToText()
    {
        Debug.Log("Null text holder trigger");
        if (textToDisplay != null && textHolderImage != null)
        {
            Debug.Log("NOT NULL text holder trigger");
            UnityEngine.TextGenerator textGenerator = new UnityEngine.TextGenerator();
            TextGenerationSettings generationSettings = textToDisplay.GetGenerationSettings(textToDisplay.rectTransform.rect.size);

            // Calculate the preferred width and height of the text
            float preferredWidth = textGenerator.GetPreferredWidth(textToDisplay.text, generationSettings);
            float preferredHeight = textGenerator.GetPreferredHeight(textToDisplay.text, generationSettings);

            // Set the size of the textHolderImage to match the preferred size of the text
            RectTransform textHolderRectTransform = textHolderImage.rectTransform;
            textHolderRectTransform.sizeDelta = new Vector2(preferredWidth, preferredHeight);
        }
    }

    private void Update()
    {
        if (ImageScaler.isScaling)
        {
            // Smoothly increase the scale of the image
            float newScale = Mathf.Lerp(textHolderImage.transform.localScale.x, 1f, Time.deltaTime * scaleSpeed);
            textHolderImage.transform.localScale = new Vector3(newScale, newScale, 1f);

            // Check if the scale is almost 1, and stop scaling
            if (Mathf.Approximately(newScale, 1f))
            {
            }
        }
    }

    private void Start()
    {
        textHolderImage.transform.localScale = Vector3.zero;
    }
}
