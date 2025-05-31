using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RestartLevel3 : MonoBehaviour
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
        SceneManager.LoadScene("Level 3");
    }
}
