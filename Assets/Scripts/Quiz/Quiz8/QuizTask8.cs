using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class QuizTask8 : MonoBehaviour
{
    public QuizSelectButtons quizSelectButtons;
    public Button buttonAnswer2;
    public Button buttonNext;

    public GameObject speechBubbleTrue;
    public GameObject speechBubbleFalse;

    void Start()
    {
        if (buttonNext != null)
        {
            buttonNext.onClick.AddListener(OnNextClicked);
        }

        if (speechBubbleTrue != null) speechBubbleTrue.SetActive(false);
        if (speechBubbleFalse != null) speechBubbleFalse.SetActive(false);
    }

    void OnNextClicked()
    {
        if (quizSelectButtons == null || buttonAnswer2 == null)
            return;

        Button selected = quizSelectButtons.GetSelectedButton();

        bool isCorrect = (selected != null && selected == buttonAnswer2);

        if (speechBubbleTrue != null) speechBubbleTrue.SetActive(false);
        if (speechBubbleFalse != null) speechBubbleFalse.SetActive(false);

        if (isCorrect)
        {
            if (speechBubbleTrue != null) speechBubbleTrue.SetActive(true);
        }
        else
        {
            if (speechBubbleFalse != null) speechBubbleFalse.SetActive(true);
        }
    }
}