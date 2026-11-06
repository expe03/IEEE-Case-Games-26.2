using System.Collections;
using UnityEngine;

public class MesaNPC : MonoBehaviour
{
    [Header("Pedido")]
    public ItemData itemPedido;
    public SpriteRenderer balaoPedido;
    public SpriteRenderer itemNaMesa;

    [Header("Interação")]
    public GameObject promptInteracao;
    public GameObject mensagemConclusao;
    public float tempoMensagem = 4f;

    [Header("Som")]
    public AudioSource audioSource;
    public AudioClip somEntrega;

    private bool jogadorPerto;
    private bool entregue;

    void Start()
    {
        if (promptInteracao != null) promptInteracao.SetActive(false);
        if (mensagemConclusao != null) mensagemConclusao.SetActive(false);

        if (itemNaMesa != null) itemNaMesa.enabled = false;

        if (balaoPedido != null && itemPedido != null)
        {
            balaoPedido.sprite = itemPedido.icone;
            balaoPedido.enabled = true;
        }
    }

    void Update()
    {
        if (promptInteracao != null)
        {
            promptInteracao.SetActive(jogadorPerto && !entregue && !EstadoJogo.Pausado);
        }

        if (EstadoJogo.Pausado) return;
        if (!jogadorPerto || entregue) return;

        if (Input.GetKeyDown(KeyCode.E))
        {
            ItemData naMao = Inventario.Instance.ItemSelecionado;

            if (naMao != null && naMao == itemPedido)
            {
                Inventario.Instance.RemoverItemSelecionado();
                Concluir();
            }
        }
    }

    void Concluir()
    {
        entregue = true;
        EstadoJogo.PedidoEntregue = true;

        if (promptInteracao != null) promptInteracao.SetActive(false);
        if (balaoPedido != null) balaoPedido.enabled = false;

        if (itemNaMesa != null)
        {
            itemNaMesa.sprite = itemPedido.icone;
            itemNaMesa.enabled = true;
        }

        if (audioSource != null && somEntrega != null)
        {
            audioSource.PlayOneShot(somEntrega);
        }

        if (mensagemConclusao != null)
        {
            mensagemConclusao.SetActive(true);

            if (tempoMensagem > 0f)
            {
                StartCoroutine(EsconderMensagem());
            }
        }
    }

    IEnumerator EsconderMensagem()
    {
        yield return new WaitForSeconds(tempoMensagem);
        mensagemConclusao.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !entregue)
        {
            jogadorPerto = true;

            if (promptInteracao != null) promptInteracao.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            jogadorPerto = false;

            if (promptInteracao != null) promptInteracao.SetActive(false);
        }
    }
}