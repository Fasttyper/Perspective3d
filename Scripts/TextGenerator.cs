using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class TextGenerator
{
    public static bool isGenerating = false;
    public static async Task AnimateTextAsync(Text textToDisplay, string textToAnimate, float delayBetweenCharacters)
    {
        isGenerating = true;
        textToDisplay.text = ""; // Clear the text

        // Gradually display the text character by character
        for (int i = 0; i < textToAnimate.Length; i++)
        {
            textToDisplay.text += textToAnimate[i];
            await Task.Delay((int)(delayBetweenCharacters * 1000));
        }
        isGenerating = false;
    }
}
