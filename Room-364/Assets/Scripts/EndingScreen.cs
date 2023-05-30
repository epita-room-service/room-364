
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingScreen : MonoBehaviour
{
    public string RestartLvl;

    public void RestartButton()
    {
        SceneManager.LoadScene(RestartLvl);
    }
    
    public void QuitGameButton()
    {
        Application.Quit();
    }
}
