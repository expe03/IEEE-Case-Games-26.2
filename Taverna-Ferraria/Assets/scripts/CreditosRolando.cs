using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CreditosRolando : MonoBehaviour
{
    public RectTransform textoCreditos;
    public float velocidade = 80f;
    public string nomeCenaMenu = "Main Menu";
    public bool voltarAoTerminar = true;

    private float alturaTela;
    private float alturaTexto;

    void Start()
    {
        Time.timeScale = 1f;

        Canvas.ForceUpdateCanvases();
        LayoutRebuilder.ForceRebuildLayoutImmediate(textoCreditos);

        RectTransform canvasRect = textoCreditos.GetComponentInParent<Canvas>().GetComponent<RectTransform>();
        alturaTela = canvasRect.rect.height;
        alturaTexto = textoCreditos.rect.height;

        textoCreditos.anchoredPosition = new Vector2(textoCreditos.anchoredPosition.x, 0f);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            VoltarAoMenu();
            return;
        }

        textoCreditos.anchoredPosition += Vector2.up * velocidade * Time.unscaledDeltaTime;

        if (voltarAoTerminar && textoCreditos.anchoredPosition.y > alturaTela + alturaTexto)
        {
            VoltarAoMenu();
        }
    }

    void VoltarAoMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
}