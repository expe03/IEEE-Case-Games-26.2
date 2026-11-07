using UnityEngine;

public class ItemSegurado : MonoBehaviour
{
    [Header("Posição da mão (relativa ao Player)")]
    public Vector3 posicaoDireita = new Vector3(0.4f, 0.5f, 0f);
    public Vector3 posicaoEsquerda = new Vector3(-0.4f, 0.5f, 0f);
    public bool espelharSpriteAoVirar = false;

    private SpriteRenderer sr;
    private NewMonoBehaviourScript jogador;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        jogador = GetComponentInParent<NewMonoBehaviourScript>();
    }

    void Start()
    {
        Inventario.Instance.OnInventarioMudou += Atualizar;
        Atualizar();
    }

    void OnDestroy()
    {
        if (Inventario.Instance != null)
        {
            Inventario.Instance.OnInventarioMudou -= Atualizar;
        }
    }

    // Depois que o player decidiu o lado, move o item para a mão certa
    void LateUpdate()
    {
        if (jogador == null) return;

        bool direita = jogador.OlhandoDireita;

        transform.localPosition = direita ? posicaoDireita : posicaoEsquerda;

        if (espelharSpriteAoVirar)
        {
            sr.flipX = !direita;
        }
    }

    void Atualizar()
    {
        ItemData item = Inventario.Instance.ItemSelecionado;

        if (item != null)
        {
            sr.sprite = item.icone;
            sr.enabled = true;
        }
        else
        {
            sr.sprite = null;
            sr.enabled = false;
        }
    }
}