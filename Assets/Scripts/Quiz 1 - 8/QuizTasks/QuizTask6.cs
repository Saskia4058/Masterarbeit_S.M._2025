using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class QuizTask6 : MonoBehaviour, ILevelTask
{
    public QuizSelectButtons quizSelectButtons;
    public Button buttonAnswer3;
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
        if (quizSelectButtons == null || buttonAnswer3 == null)
            return;

        Button selected = quizSelectButtons.GetSelectedButton();

        bool isCorrect = (selected != null && selected == buttonAnswer3);

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
        public List<Button> GetCorrectButtons()
    {
        return new List<Button> { buttonAnswer3 };
    }
}
