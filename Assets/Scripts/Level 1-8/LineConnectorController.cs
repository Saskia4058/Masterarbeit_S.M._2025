using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.UI.Extensions;

public class LineConnectorController : MonoBehaviour
{
    public MultiSelectButtons multiSelectButtons;

    [System.Serializable]
    public class LineConnector
    {
        public UILineRenderer lineRenderer;
        public Button buttonA;
        public Button buttonB;
    }

    public List<LineConnector> connectors = new List<LineConnector>();

    public Color normalColor = new Color32(0xE5, 0x9A, 0x4A, 0xFF); 
    public Color highlightedColor = new Color32(0xFF, 0xB5, 0x66, 0xFF); 

    private void Start()
    {
        if (multiSelectButtons != null)
        {
            multiSelectButtons.OnButtonSelectionChanged += UpdateLines;
        }

        UpdateLines();
    }

    private void UpdateLines()
    {
        foreach (var connector in connectors)
        {
            if (connector.buttonA == null || connector.buttonB == null || connector.lineRenderer == null)
                continue;

            bool bothSelected = multiSelectButtons.IsButtonSelected(connector.buttonA)
                              && multiSelectButtons.IsButtonSelected(connector.buttonB);

            connector.lineRenderer.color = bothSelected ? highlightedColor : normalColor;
        }
    }

    private void OnDestroy()
    {
        if (multiSelectButtons != null)
        {
            multiSelectButtons.OnButtonSelectionChanged -= UpdateLines;
        }
    }
}