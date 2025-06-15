using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class SolutionRevealController : MonoBehaviour
{
    public GameObject speechBubbleExplain;
    public MultiSelectButtons multiSelectButtons;
    public LineConnectorController lineConnectorController;
    public GameObject taskManager; // Neu: GameObject mit dem Task-Script (im Inspector zuweisen)

    private bool solutionShown = false;
    private static readonly Color solutionColor = new Color(253f / 255f, 255f / 255f, 0f); // Gelb

    void Update()
    {
        if (!solutionShown && speechBubbleExplain != null && speechBubbleExplain.activeSelf)
        {
            ShowSolution();
            solutionShown = true;
        }
    }

    void ShowSolution()
    {
        if (multiSelectButtons == null)
        {
            Debug.LogWarning("MultiSelectButtons ist nicht gesetzt.");
            return;
        }

        if (taskManager == null)
        {
            Debug.LogWarning("TaskManager GameObject ist nicht gesetzt.");
            return;
        }

        // 1. Buttons zurücksetzen & deaktivieren
        foreach (var buttonData in multiSelectButtons.buttons)
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
            var bData = multiSelectButtons.buttons.Find(b => b.button == button);
            if (bData != null)
            {
                ApplySolutionColor(button);
            }
        }

        // 3. Verbindungen hervorheben
        if (lineConnectorController != null)
        {
            foreach (var connector in lineConnectorController.connectors)
            {
                if (connector.lineRenderer == null || connector.buttonA == null || connector.buttonB == null)
                    continue;

                if (correctButtons.Contains(connector.buttonA) && correctButtons.Contains(connector.buttonB))
                {
                    connector.lineRenderer.color = solutionColor;
                }
                else
                {
                    // Optional: Auf normale Farbe zurücksetzen
                    connector.lineRenderer.color = lineConnectorController.normalColor;
                }
            }
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
