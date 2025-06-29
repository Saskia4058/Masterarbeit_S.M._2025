using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HighscoreManager : MonoBehaviour
{
    [Header("Eingabe nur in Szene 'Rank'")]
    public TMP_InputField nameInputField;
    public Button buttonWeiter;  // 🔹 NEU: Referenz zum Button

    [Header("Anzeige nur in Szene 'Leaderboard'")]
    public List<TextMeshProUGUI> entryTexts;

    private const int maxEntries = 5;
    private const string highscoreKey = "Highscores";

    [System.Serializable]
    public class HighscoreEntry
    {
        public string name;
        public int score;

        public HighscoreEntry(string name, int score)
        {
            this.name = name;
            this.score = score;
        }
    }

    [System.Serializable]
    public class HighscoreList
    {
        public List<HighscoreEntry> entries = new List<HighscoreEntry>();
    }

    private void Start()
    {
        string currentScene = SceneManager.GetActiveScene().name;

        if (currentScene == "Rank")
        {
            // 🔸 Button zu Beginn deaktiviert
            if (buttonWeiter != null)
                buttonWeiter.gameObject.SetActive(false);

            // 🔸 Listener für InputField
            if (nameInputField != null)
                nameInputField.onValueChanged.AddListener(delegate { CheckNameInput(); });
        }
        else if (currentScene == "Leaderboard")
        {
            DisplayHighscores();
        }
    }

    /// <summary>
    /// Überprüft den Text im Eingabefeld und aktiviert den Button, wenn etwas eingegeben wurde.
    /// </summary>
    private void CheckNameInput()
    {
        if (buttonWeiter == null || nameInputField == null)
            return;

        string input = nameInputField.text;
        bool hasInput = !string.IsNullOrWhiteSpace(input);

        buttonWeiter.gameObject.SetActive(hasInput);
    }

    /// <summary>
    /// Wird von ButtonControllerRank aufgerufen – speichert aktuellen Score mit Namen
    /// </summary>
    public void SaveCurrentScoreWithName()
    {
        string playerName = nameInputField != null ? nameInputField.text : "Spieler";
        if (string.IsNullOrWhiteSpace(playerName))
            playerName = "Spieler";

        int score = PlayerPrefs.GetInt("CurrentScore", 0);

        AddHighscore(playerName, score);
    }

    private void AddHighscore(string name, int score)
    {
        HighscoreList highscoreList = LoadHighscores();

        // Eintrag hinzufügen
        highscoreList.entries.Add(new HighscoreEntry(name, score));

        // Sortieren (höchste Punktzahl zuerst)
        highscoreList.entries.Sort((a, b) => b.score.CompareTo(a.score));

        // Nur Top 5 behalten
        if (highscoreList.entries.Count > maxEntries)
        {
            highscoreList.entries.RemoveRange(maxEntries, highscoreList.entries.Count - maxEntries);
        }

        // Speichern
        string json = JsonUtility.ToJson(highscoreList);
        PlayerPrefs.SetString(highscoreKey, json);
        PlayerPrefs.Save();
    }

    private HighscoreList LoadHighscores()
    {
        if (PlayerPrefs.HasKey(highscoreKey))
        {
            string json = PlayerPrefs.GetString(highscoreKey);
            return JsonUtility.FromJson<HighscoreList>(json);
        }
        else
        {
            return new HighscoreList();
        }
    }

    private void DisplayHighscores()
    {
        HighscoreList highscoreList = LoadHighscores();

        for (int i = 0; i < entryTexts.Count; i++)
        {
            if (i < highscoreList.entries.Count)
            {
                var entry = highscoreList.entries[i];
                entryTexts[i].text = $"{i + 1}. {entry.name} - {entry.score} Punkte";
            }
            else
            {
                entryTexts[i].text = $"{i + 1}. ---";
            }
        }
    }
}