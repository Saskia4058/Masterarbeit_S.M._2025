using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class PasserelleTask : MonoBehaviour
{
    public MultiSelectButtons multiSelectButtons;

    public Button buttonFMS;
    public Button buttonFaMa;
    public Button buttonPasserelle;
    public Button buttonUNI;
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

        bool containsFMS = selected.Contains(buttonFMS);
        bool containsFaMa = selected.Contains(buttonFaMa);
        bool containsPasserelle = selected.Contains(buttonPasserelle);
        bool containsUNI = selected.Contains(buttonUNI);

        bool isExactlyTheseFourSelected = 
            selected.Count == 4 &&
            containsFMS && containsFaMa && containsPasserelle && containsUNI;

        if (speechBubbleTrue != null) speechBubbleTrue.SetActive(false);
        if (speechBubbleFalse != null) speechBubbleFalse.SetActive(false);
        if (speechBubbleFalse2 != null) speechBubbleFalse2.SetActive(false);

        if (isExactlyTheseFourSelected)
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