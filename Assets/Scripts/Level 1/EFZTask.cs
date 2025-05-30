using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class EFZTask : MonoBehaviour
{
    public MultiSelectButtons multiSelectButtons;
    public Button buttonEFZ;
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
        if (multiSelectButtons == null || buttonEFZ == null)
            return;

        List<Button> selected = multiSelectButtons.GetSelectedButtons();

        bool isOnlyEFZSelected = (selected.Count == 1 && selected.Contains(buttonEFZ));

        if (speechBubbleTrue != null) speechBubbleTrue.SetActive(false);
        if (speechBubbleFalse != null) speechBubbleFalse.SetActive(false);

        if (isOnlyEFZSelected)
        {
            if (speechBubbleTrue != null) speechBubbleTrue.SetActive(true);
        }
        else
        {
            if (speechBubbleFalse != null) speechBubbleFalse.SetActive(true);
        }
    }
}