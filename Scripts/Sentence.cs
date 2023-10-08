using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "newSentence", menuName = "Sentence")]
public class Sentence : ScriptableObject
{
    public string sentenceName;
    public string text;
    public AudioClip voiceClip;
    public float timeToShow;
    public bool isQuestion;

    public List<string> possibleAnswers = new List<string>(); // List of possible answers
    public int correctAnswerIndex = -1; // Index of the correct answer (-1 if none)

    public bool HasAnswers()
    {
        return isQuestion && possibleAnswers.Count > 0;
    }

    public bool IsCorrectAnswer(int answerIndex)
    {
        return answerIndex == correctAnswerIndex;
    }
}
