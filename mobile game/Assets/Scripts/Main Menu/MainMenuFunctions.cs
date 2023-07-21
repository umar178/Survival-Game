using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenuFunctions : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("MainGame");
    }
    
    public void ExitGame()
    {
        Application.Quit();
    }
}
