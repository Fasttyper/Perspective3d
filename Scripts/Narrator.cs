using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "newNarrator", menuName = "Narrator")]
public class Narrator : ScriptableObject
{
    public string narratorName;
    public Sprite narratorSprite;
    private bool _isAutomaticTelling = false;

    public int currentSentenceIndex = 0; // Keep track of the current sentence index

    public void SetCurrentSentenceIndexToZero()
    {
        currentSentenceIndex = 0;
    }

    private string Speak(Sentence sentence,
        AudioSource audioSource,
        Image narratorImage,
        float scaleFactor,
        Vector3 initialScale)
    {
        ImageScaler.IncreaseScaleOfImage(narratorImage, scaleFactor);
        audioSource.clip = sentence.voiceClip;
        audioSource.Play();
        ImageScaler.DecreaseScaleOfImage(narratorImage, scaleFactor, initialScale);
        return sentence.text;
    }

    public async Task TellAsync(Sentence[] sentences,
        AudioSource audioSource,
        Text textToDisplay,
        Image narratorImage,
        float scaleFactor,
        Vector3 initialScale)
    {
        string text = "";
        if (_isAutomaticTelling)
        {
            return; // Do nothing if automatic telling is enabled
        }

        // Check if there are more sentences to display
        if (currentSentenceIndex < sentences.Length)
        {
            // Get the next sentence
            Sentence sentence = sentences[currentSentenceIndex];

            // Display the sentence text using the narrator.Speak() method
            text = Speak(sentence, audioSource, narratorImage, scaleFactor, initialScale);
            await TextGenerator.AnimateTextAsync(textToDisplay, text, 0.01f);
            // Increment the current sentence index for the next click
            currentSentenceIndex++;
            Debug.Log(currentSentenceIndex + "CurrentSentenceIndex");
        }
        else
        {
            Debug.Log("No more sentences");
            // If there are no more sentences, you can handle this case (e.g., close a dialogue box)
            // or loop back to the beginning if desired.
            // For example, you can reset currentSentenceIndex to 0 to start over.
            currentSentenceIndex = 0;
        }
    }

    public async Task StartAutomaticTellingAsync(Sentence[] sentences,
        AudioSource audioSource,
        Text textToDisplay,
        Image narratorImage,
        float scaleFactor,
        Vector3 initialScale)
    {
        string text = "";
        if (!_isAutomaticTelling)
        {
            return; // Do nothing if automatic telling is not enabled
        }

        // Check if TextGenerator is still generating text
        while (TextGenerator.isGenerating)
        {
            await Task.Delay(10); // Wait for a short time before checking again
        }

        while (currentSentenceIndex < sentences.Length)
        {
            Sentence sentence = sentences[currentSentenceIndex];
            text = Speak(sentence, audioSource, narratorImage, scaleFactor, initialScale);
            await TextGenerator.AnimateTextAsync(textToDisplay, text, 0.01f);
            currentSentenceIndex++;
            Debug.Log(currentSentenceIndex + "CurrentSentenceIndex");
            await Task.Delay((int)(sentence.timeToShow * 1000)); // Convert seconds to milliseconds

        }

        // If there are no more sentences, you can handle this case (e.g., close a dialogue box)
        // or loop back to the beginning if desired.
        // For example, you can reset currentSentenceIndex to 0 to start over.
        currentSentenceIndex = 0;
        Debug.Log("NO more sentences");
    }

    public void SetAutomaticTelling(bool isAutomaticTelling)
    {
        _isAutomaticTelling = isAutomaticTelling;
    }

    public void Pause(AudioSource audioSource)
    {
        audioSource.Pause();
    }

    public void Continue(AudioSource audioSource)
    {
        audioSource.UnPause();
    }
}
