using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Estacao : MonoBehaviour
{
    [Header("Configuração")]
    public List<ReceitaData> receitas;
    public int capacidade = 1;
    public GameObject promptInteracao;

    [Header("Visual dos itens")]
    public SpriteRenderer[] visuaisDosItens;

    private List<ItemData> itensNaEstacao = new List<ItemData>();
    private bool jogadorPerto;
    private bool processando;
    private bool travandoPlayer;
    private float progresso;
    private ReceitaData receitaAtual;

    // Para a barra de progresso
    public bool Processando => processando;
    public float Progresso => progresso;
    public ReceitaData ReceitaAtual => receitaAtual;

    void Start()
    {
        if (promptInteracao != null)
        {
            promptInteracao.SetActive(false);
        }

        AtualizarVisual();
    }

    void Update()
    {
        // Enquanto processa, a estação não aceita nada
        if (!jogadorPerto || processando) return;

        if (Input.GetKeyDown(KeyCode.E))
        {
            TentarInteragir();
        }

        if (Input.GetMouseButtonDown(0))
        {
            ReceitaData receita = ProcurarReceita();

            if (receita != null)
            {
                StartCoroutine(Processar(receita));
            }
        }
    }

    void TentarInteragir()
    {
        ItemData naMao = Inventario.Instance.ItemSelecionado;

        if (naMao != null)
        {
            if (itensNaEstacao.Count < capacidade)
            {
                itensNaEstacao.Add(Inventario.Instance.RemoverItemSelecionado());
                AtualizarVisual();

                // Receitas automáticas começam assim que os itens certos entram
                ReceitaData receita = ProcurarReceita();

                if (receita != null && receita.iniciaAutomatico)
                {
                    StartCoroutine(Processar(receita));
                }
            }
        }
        else if (itensNaEstacao.Count > 0)
        {
            int ultimo = itensNaEstacao.Count - 1;

            if (Inventario.Instance.ReceberNoSlotSelecionado(itensNaEstacao[ultimo]))
            {
                itensNaEstacao.RemoveAt(ultimo);
                AtualizarVisual();
            }
        }
    }

    ReceitaData ProcurarReceita()
    {
        foreach (ReceitaData receita in receitas)
        {
            if (receita.Combina(itensNaEstacao))
            {
                return receita;
            }
        }

        return null;
    }

    IEnumerator Processar(ReceitaData receita)
    {
        processando = true;
        receitaAtual = receita;
        progresso = 0f;

        if (receita.prendePlayer)
        {
            EstadoJogo.PlayerTravado = true;
            travandoPlayer = true;
        }

        float tempo = 0f;

        while (tempo < receita.duracao)
        {
            tempo += Time.deltaTime;
            progresso = tempo / receita.duracao;
            yield return null;
        }

        itensNaEstacao.Clear();
        itensNaEstacao.Add(receita.resultado);
        AtualizarVisual();

        LiberarPlayer();
        processando = false;
        receitaAtual = null;
        progresso = 0f;
    }

    void LiberarPlayer()
    {
        if (travandoPlayer)
        {
            EstadoJogo.PlayerTravado = false;
            travandoPlayer = false;
        }
    }

    // Se a cena mudar no meio do processo, não deixa o player travado para sempre
    void OnDestroy()
    {
        LiberarPlayer();
    }

    void AtualizarVisual()
    {
        for (int i = 0; i < visuaisDosItens.Length; i++)
        {
            if (i < itensNaEstacao.Count)
            {
                visuaisDosItens[i].sprite = itensNaEstacao[i].icone;
                visuaisDosItens[i].enabled = true;
            }
            else
            {
                visuaisDosItens[i].sprite = null;
                visuaisDosItens[i].enabled = false;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            jogadorPerto = true;

            if (promptInteracao != null)
            {
                promptInteracao.SetActive(true);
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            jogadorPerto = false;

            if (promptInteracao != null)
            {
                promptInteracao.SetActive(false);
            }
        }
    }
}