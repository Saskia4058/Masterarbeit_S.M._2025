using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class ProgressTracker : MonoBehaviour
{
    public Image progressBarFill; // Das "ProgressBarFill"-Image
    public TextMeshProUGUI levelText; // Das "Levelanzeige"-Textfeld

    private static ProgressTracker instance;

    private string[] sceneOrder = new string[]
    {
        "Level 1", "Level 2", "Level 3", "Level 4",
        "Quiz 1", "Quiz 2", "Quiz 3", "Quiz 4",
        "Level 5", "Level 6", "Level 7", "Level 8",
        "Quiz 5", "Quiz 6", "Quiz 7", "Quiz 8"
    };

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Suche in der neu geladenen Szene nach den UI-Referenzen
        progressBarFill = GameObject.Find("ProgressBarFill")?.GetComponent<Image>();
        levelText = GameObject.Find("Levelanzeige")?.GetComponent<TextMeshProUGUI>();

        UpdateProgress(scene.name);
    }

    void UpdateProgress(string currentSceneName)
    {
        int index = System.Array.IndexOf(sceneOrder, currentSceneName);
        if (index >= 0 && progressBarFill != null && levelText != null)
        {
            float fillAmount = (index + 1) / (float)sceneOrder.Length;
            progressBarFill.fillAmount = fillAmount;

            levelText.text = "Level " + (index + 1).ToString();
        }
    }
}
