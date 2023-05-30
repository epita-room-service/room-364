
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string StartLvl;

    public string SettingsScene;
      
    public void StartButton()
    {
        SceneManager.LoadScene(StartLvl);
    }

    public void SettingsButton()
    {
        SceneManager.LoadScene(SettingsScene);
    }

    public void ExitButton()
    {
        Application.Quit();
    }
}
