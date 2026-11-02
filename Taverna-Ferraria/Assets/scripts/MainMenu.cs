using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        GerenciadorSpawn.ResetarSpawn();

        SceneManager.LoadScene("AreaInicial");

        Time.timeScale = 1;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
