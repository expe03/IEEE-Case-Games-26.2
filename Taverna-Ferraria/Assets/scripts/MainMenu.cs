using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
   public void StartGame()
   {
        SceneManager.LoadScene("Scene 1");
        Time.timeScale = 1;
   }
   public void QuitGame()
   {
        Application.Quit();
   }
}