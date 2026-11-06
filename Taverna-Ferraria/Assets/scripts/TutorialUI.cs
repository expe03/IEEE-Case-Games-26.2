using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialUI : MonoBehaviour
{
    public GameObject caixa;
    public TMP_Text textoDialogo;

    void Start()
    {
        if (TutorialManager.Instance == null)
        {
            caixa.SetActive(false);
            return;
        }

        TutorialManager.Instance.OnPassoMudou += Atualizar;
        Atualizar();
    }

    void OnDestroy()
    {
        if (TutorialManager.Instance != null)
        {
            TutorialManager.Instance.OnPassoMudou -= Atualizar;
        }
    }

    void Atualizar()
    {
        TutorialManager tutorial = TutorialManager.Instance;

        bool mostrar = tutorial != null && tutorial.DeveMostrar(SceneManager.GetActiveScene().name);

        caixa.SetActive(mostrar);

        if (mostrar)
        {
            textoDialogo.text = tutorial.TextoAtual;
        }
    }
}