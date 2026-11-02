using UnityEngine;

public class ItemSegurado : MonoBehaviour
{
    private SpriteRenderer sr;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
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
