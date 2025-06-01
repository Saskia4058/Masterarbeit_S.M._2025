using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class BM1Task : MonoBehaviour
{
    public MultiSelectButtons multiSelectButtons;
    public Button buttonEFZ;
    public Button buttonBM1;
    public Button buttonFH;
    public Button buttonNext;

    public GameObject speechBubbleTrue;
    public GameObject speechBubbleFalse;
    public GameObject speechBubbleFalse2;

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
        if (multiSelectButtons == null)
            return;

        List<Button> selected = multiSelectButtons.GetSelectedButtons();

        bool containsEFZ = selected.Contains(buttonEFZ);
        bool containsBM1 = selected.Contains(buttonBM1);
        bool containsFH = selected.Contains(buttonFH);

        bool isExactlyTheseThreeSelected = 
            selected.Count == 3 &&
            containsEFZ && containsBM1 && containsFH;

        if (speechBubbleTrue != null) speechBubbleTrue.SetActive(false);
        if (speechBubbleFalse != null) speechBubbleFalse.SetActive(false);
        if (speechBubbleFalse2 != null) speechBubbleFalse2.SetActive(false);

        if (isExactlyTheseThreeSelected)
        {
            if (speechBubbleTrue != null) speechBubbleTrue.SetActive(true);
        }
        else
        {
            string currentLevel = SceneManager.GetActiveScene().name;

            bool restarted = LevelRestartTracker.WasLevelRestarted(currentLevel);

            if (restarted)
            {
                if (speechBubbleFalse2 != null) speechBubbleFalse2.SetActive(true);
            }
            else
            {
                if (speechBubbleFalse != null) speechBubbleFalse.SetActive(true);
            }
        }
    }
}
