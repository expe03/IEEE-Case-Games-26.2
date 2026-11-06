using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialUI : MonoBehaviour
{
    public GameObject caixa;
    public TMP_Text textoDialogo;

    void Update()
    {
        TutorialManager tutorial = TutorialManager.Instance;

        // A caixa só aparece se o tutorial deve mostrar nesta cena e o jogo não está pausado
        bool mostrar = tutorial != null
                       && tutorial.DeveMostrar(SceneManager.GetActiveScene().name)
                       && Time.timeScale > 0f;

        if (caixa.activeSelf != mostrar)
        {
            caixa.SetActive(mostrar);
        }

        if (mostrar && textoDialogo.text != tutorial.TextoAtual)
        {
            textoDialogo.text = tutorial.TextoAtual;
        }
    }
}