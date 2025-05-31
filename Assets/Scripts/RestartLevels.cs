using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RestartLevels : MonoBehaviour
{
    public Button buttonWeiter;

    void Start()
    {
        if (buttonWeiter != null)
        {
            buttonWeiter.onClick.AddListener(ReloadLevel);
        }
        else
        {
            Debug.LogError("ButtonWeiter is not assigned in the inspector.");
        }
    }

    void ReloadLevel()
    {
        string currentLevel = SceneManager.GetActiveScene().name;

        // Markiere dieses Level als "bereits neu gestartet"
        LevelRestartTracker.MarkLevelRestarted(currentLevel);

        // Lade das aktuelle Level neu
        SceneManager.LoadScene(currentLevel);
    }
}
