using UnityEngine;

public class BarraProgresso : MonoBehaviour
{
    public GameObject barra;
    public Transform preenchimento;
    public float larguraMax = 1.2f;
    public float altura = 0.14f;

    private Estacao estacao;

    void Awake()
    {
        estacao = GetComponent<Estacao>();
    }

    void Start()
    {
        barra.SetActive(false);
    }

    void Update()
    {
        bool mostrar = estacao.Processando
                       && estacao.ReceitaAtual != null
                       && estacao.ReceitaAtual.mostraBarra;

        if (barra.activeSelf != mostrar)
        {
            barra.SetActive(mostrar);
        }

        if (mostrar)
        {
            float p = estacao.Progresso;

            // A barra cresce da esquerda para a direita
            preenchimento.localScale = new Vector3(larguraMax * p, altura, 1f);
            preenchimento.localPosition = new Vector3(-larguraMax / 2f + larguraMax * p / 2f, 0f, 0f);
        }
    }
}
