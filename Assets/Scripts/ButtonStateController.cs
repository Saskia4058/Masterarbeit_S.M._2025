using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonStateController : MonoBehaviour
{
    [Header("Referenzen")]
    [SerializeField] private GameObject buttonsParent; // "Buttons"
    [SerializeField] private GameObject buttonReplay;  // "ButtonReplay"

    private List<Button> buttons = new List<Button>();

    void Start()
    {
        if (buttonsParent != null)
        {
            buttons.AddRange(buttonsParent.GetComponentsInChildren<Button>());

            foreach (var button in buttons)
            {
                button.interactable = false;
            }
        }
        else
        {
            Debug.LogWarning("Buttons Parent ist nicht zugewiesen!");
        }
    }

    void Update()
    {
        TryActivateButtons();
    }

    private void TryActivateButtons()
    {
        if (buttonReplay != null && buttonReplay.activeSelf)
        {
            foreach (var button in buttons)
            {
                button.interactable = true;
            }
        }
        else
        {
            foreach (var button in buttons)
            {
                button.interactable = false;
            }
        }
    }

    public void ReactivateButtonsIfReplayActive()
    {
        TryActivateButtons();
    }
}