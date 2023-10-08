using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public static class ImageScaler
{
    // TODO: make text generating smooth
    // make choices

    public static bool isScaling;
    public static void IncreaseScaleOfImage(Image image, float scaleFactor)
    {
        // Increase the scale of the narratorImage by a factor of scaleFactor
        Debug.Log("Increasing");
        Vector3 newScale = image.transform.localScale * scaleFactor;
        image.transform.localScale = newScale;
    }

    public static void DecreaseScaleOfImage(Image image, float restoreTime, Vector3 initialScale)
    {
        Debug.Log("Decreasing");
        StartRestoringScale(image, restoreTime, initialScale);
    }

    private static async void StartRestoringScale(Image image, float restoreTime, Vector3 initialScale)
    {
        if (isScaling)
        {
            return; // Don't start a new scaling process if one is already in progress.
        }

        isScaling = true;

        float elapsedTime = 0f;
        Vector3 startScale = image.transform.localScale;

        while (elapsedTime < restoreTime)
        {
            // Calculate the new scale using Lerp.
            image.transform.localScale = Vector3.Lerp(startScale, initialScale, elapsedTime / restoreTime);

            // Increment the elapsed time.
            elapsedTime += Time.deltaTime;

            await Task.Yield(); // Yield control back to the main thread momentarily.

            // You can use Task.Delay if you want to introduce a delay here.
        }

        //Ensure the final scale is exactly the initial scale.
        image.transform.localScale = initialScale;

        isScaling = false;
    }

    public static void DeActivateImage(Image image)
    {
        image.gameObject.SetActive(false);
    }

    public static void SetScaleOfImageToZero(Image image)
    {
        image.transform.localScale = Vector3.zero;
    }

    public static void SetImagesForAnswers(Sentence sentence)
    {
        if (sentence.isQuestion)
        {

        }
    }
}
