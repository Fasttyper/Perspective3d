using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class NarratorDisplay : MonoBehaviour
{
    public Narrator narrator;
    public Sentence[] sentences;
    public Image narratorImage;
    public Image textHolderImage;
    public Image textHolderImagePrefab;
    public Transform textHolderParent;
    public Text text;
    public AudioSource audioSource;
    public bool isAutomaticTelling;
    public float scaleFactor = 1.2f;
    public Vector3 initialScale;
    public float restoreTime = 2.0f;
    public bool isScaling = false;


    async void Start()
    {
        initialScale = narratorImage.transform.localScale;
        narrator.SetCurrentSentenceIndexToZero();
        narratorImage.sprite = narrator.narratorSprite;

        narrator.SetAutomaticTelling(isAutomaticTelling);

        await narrator.TellAsync(sentences, audioSource, text, narratorImage, scaleFactor, initialScale);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Check if the mouse click is not over a UI element
            if (!IsPointerOverUIObject())
            {
                // Handle the click or touch here
                HandleClickOrTouch();
            }
        }
    }

    private async void HandleClickOrTouch()
    {
        //if (isScaling && isAutomaticTelling && !TextGenerator.isGenerating)
        //{
        //    ImageScaler.SetScaleOfImageToZero(textHolderImage);

        //    await narrator.TellAsync(sentences, audioSource, text, narratorImage, scaleFactor, initialScale);

        //    foreach (var sentence in sentences)
        //    {
        //        if (sentence.isQuestion && sentence.HasAnswers())
        //        {
        //            // Create textHolderImages for each answer
        //            foreach (var answer in sentence.possibleAnswers)
        //            {
        //                // Instantiate a new textHolderImage from the prefab
        //                Image newImage = Instantiate(textHolderImagePrefab, textHolderParent);

        //                // Set the text of the new textHolderImage
        //                newImage.GetComponentInChildren<Text>().text = answer;

        //                // Activate the new textHolderImage
        //                newImage.gameObject.SetActive(true);
        //            }
        //        }
        //    }
        //}

        if (!isScaling && !isAutomaticTelling && !TextGenerator.isGenerating)
        {
            ImageScaler.SetScaleOfImageToZero(textHolderImage);

            await narrator.TellAsync(sentences, audioSource, text, narratorImage, scaleFactor, initialScale);

            textHolderImage.gameObject.SetActive(true);
        }
    }

    public async void AutomaticSpellingButtonClick()
    {
        if (isAutomaticTelling)
        {
            isAutomaticTelling = false;
            narrator.SetAutomaticTelling(isAutomaticTelling);
            return;
        }

        isAutomaticTelling = true;
        narrator.SetAutomaticTelling(isAutomaticTelling);

        await narrator.StartAutomaticTellingAsync(sentences, audioSource, text, narratorImage, scaleFactor, initialScale);

        ImageScaler.DeActivateImage(textHolderImage);
    }

    // Check if the mouse click is over a UI element
    private bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

        var results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);

        foreach (var result in results)
        {
            if (result.gameObject.GetComponent<Button>() != null)
            {
                return true; // Clicked on a button
            }
        }
        return false; // Not clicked on a button
    }
}
