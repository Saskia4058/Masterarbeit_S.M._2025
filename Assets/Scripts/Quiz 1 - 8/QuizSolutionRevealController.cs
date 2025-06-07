using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class QuizSolutionRevealController : MonoBehaviour
{
    public GameObject speechBubbleTrue;
    public GameObject speechBubbleFalse;
    public QuizSelectButtons quizSelectButtons;
    public GameObject taskManager; // Neu: GameObject mit dem Task-Script (im Inspector zuweisen)

    private bool solutionShown = false;
    private static readonly Color solutionColor = new Color(253f / 255f, 255f / 255f, 0f); // Gelb

    void Update()
    {
        if (!solutionShown && speechBubbleTrue != null && speechBubbleTrue.activeSelf)
        {
            ShowSolution();
            solutionShown = true;
        }
        if (!solutionShown && speechBubbleFalse != null && speechBubbleFalse.activeSelf)
        {
            ShowSolution();
            solutionShown = true;
        }    
    }

    void ShowSolution()
    {
        if (quizSelectButtons == null)
        {
            Debug.LogWarning("QuizSelectButtons ist nicht gesetzt.");
            return;
        }

        if (taskManager == null)
        {
            Debug.LogWarning("TaskManager GameObject ist nicht gesetzt.");
            return;
        }

        // 1. Buttons zurücksetzen & deaktivieren
        foreach (var buttonData in quizSelectButtons.buttons)
        {
            buttonData.button.interactable = false;
            // Setze Farben auf normal (optional, damit nicht vorherige Farben bleiben)
            ColorBlock cb = buttonData.button.colors;
            cb.normalColor = buttonData.normalColor;
            cb.highlightedColor = buttonData.highlightedColor;
            cb.pressedColor = buttonData.normalColor;
            cb.selectedColor = buttonData.normalColor;
            cb.disabledColor = new Color(buttonData.normalColor.r * 0.5f, buttonData.normalColor.g * 0.5f, buttonData.normalColor.b * 0.5f, 0.5f);
            buttonData.button.colors = cb;
        }

        // 2. Korrekte Buttons markieren
        ILevelTask task = taskManager.GetComponent<ILevelTask>();
        if (task == null)
        {
            Debug.LogWarning("Kein Task-Script gefunden, das ILevelTask implementiert.");
            return;
        }

        List<Button> correctButtons = task.GetCorrectButtons();
        foreach (var button in correctButtons)
        {
            var bData = quizSelectButtons.buttons.Find(b => b.button == button);
            if (bData != null)
            {
                ApplySolutionColor(button);
            }
        }

        void ApplySolutionColor(Button button)
        {
            ColorBlock cb = button.colors;
            cb.normalColor = solutionColor;
            cb.highlightedColor = solutionColor;
            cb.pressedColor = solutionColor;
            cb.selectedColor = solutionColor;
            cb.disabledColor = solutionColor;
            button.colors = cb;
        }
    }
}
