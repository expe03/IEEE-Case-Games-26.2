using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum CondicaoTutorial
{
    ObterItens,      // o player precisa ter coletado ou criado todos os itens da lista
    EntrarNaCena,    // o player precisa estar na cena indicada
    PedidoEntregue,  // a entrega na mesa do NPC foi feita
    ApenasTempo      // termina sozinho depois de alguns segundos
}

[Serializable]
public class PassoTutorial
{
    [TextArea(2, 4)] public string texto;
    public string cenaOndeAparece;
    public CondicaoTutorial condicao;
    public ItemData[] itens;
    public string nomeCena;
    public float duracao = 5f;
}

public class TutorialManager : MonoBehaviour
{
    public static TutorialManager Instance;

    public PassoTutorial[] passos;

    public event Action OnPassoMudou;

    private int passoAtual = 0;
    private float tempoNoPasso = 0f;

    public bool Ativo => passos != null && passoAtual < passos.Length;
    public string TextoAtual => Ativo ? passos[passoAtual].texto : "";

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        // novo jogo: zera o que o tutorial usa para acompanhar o progresso
        EstadoJogo.PedidoEntregue = false;
        EstadoJogo.ItensObtidos.Clear();

        SceneManager.sceneLoaded += AoCarregarCena;
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= AoCarregarCena;

        if (Instance == this)
        {
            Instance = null;
        }
    }

    // Voltou ao menu principal: o próximo jogo começa o tutorial do zero
    void AoCarregarCena(Scene cena, LoadSceneMode modo)
    {
        if (cena.name == "Main Menu")
        {
            Destroy(gameObject);
        }
    }

    // A interface pergunta se o texto deve aparecer na cena atual
    public bool DeveMostrar(string nomeCenaAtual)
    {
        if (!Ativo) return false;

        string cena = passos[passoAtual].cenaOndeAparece;
        return string.IsNullOrEmpty(cena) || cena == nomeCenaAtual;
    }

    void Update()
    {
        if (Instance != this || !Ativo) return;

        PassoTutorial passo = passos[passoAtual];

        if (passo.condicao == CondicaoTutorial.ApenasTempo)
        {
            tempoNoPasso += Time.unscaledDeltaTime;
        }

        if (CondicaoCumprida(passo))
        {
            passoAtual++;
            tempoNoPasso = 0f;
            OnPassoMudou?.Invoke();
        }
    }

    bool CondicaoCumprida(PassoTutorial passo)
    {
        switch (passo.condicao)
        {
            case CondicaoTutorial.ObterItens:
                if (passo.itens == null) return true;

                foreach (ItemData item in passo.itens)
                {
                    if (!EstadoJogo.ItensObtidos.Contains(item)) return false;
                }

                return true;

            case CondicaoTutorial.EntrarNaCena:
                return SceneManager.GetActiveScene().name == passo.nomeCena;

            case CondicaoTutorial.PedidoEntregue:
                return EstadoJogo.PedidoEntregue;

            case CondicaoTutorial.ApenasTempo:
                return tempoNoPasso >= passo.duracao;
        }

        return false;
    }
}