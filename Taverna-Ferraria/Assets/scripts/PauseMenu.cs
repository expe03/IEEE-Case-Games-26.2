using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject PausePainel;
    public GameObject ConfigsPainel;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Se as configurações estiverem abertas, o ESC volta para o menu de pause
            if (ConfigsPainel != null && ConfigsPainel.activeSelf)
            {
                VoltarDasConfigs();
            }
            else
            {
                PausePainel.SetActive(true);
                Time.timeScale = 0;
            }
        }
    }

    public void ResumeButton()
    {
        PausePainel.SetActive(false);
        Time.timeScale = 1;
    }

    public void MainMenuButton()
    {
        Time.timeScale = 1;
        UnityEngine.SceneManagement.SceneManager.LoadScene("Main Menu");
    }

    public void AbrirConfigs()
    {
        PausePainel.SetActive(false);
        ConfigsPainel.SetActive(true);
    }

    public void VoltarDasConfigs()
    {
        ConfigsPainel.SetActive(false);
        PausePainel.SetActive(true);
    }
}