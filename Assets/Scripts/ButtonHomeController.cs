using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonHomeController : MonoBehaviour
{
    private bool isGamePaused = false;
  
    // Update is called once per frame
    void Update()
    {
        if (isGamePaused)
        {
            Time.timeScale = 0f;
            AudioListener.pause = true;
        }
        if (!isGamePaused)
        {
            Time.timeScale = 1f;
            AudioListener.pause = false;
        }
    }
    public void ToogelGamePaused()
    {
        isGamePaused = !isGamePaused;
    }
    public void QuitToMainMenu()
    {
        SceneManager.LoadScene("Startscreen");
    }
}
