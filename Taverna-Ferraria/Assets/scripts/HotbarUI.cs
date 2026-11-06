using UnityEngine;

public class HotbarUI : MonoBehaviour
{
    public SlotUI slotPrefab;
    private SlotUI[] slots;
    private CanvasGroup grupo;

    void Awake()
    {
        grupo = GetComponent<CanvasGroup>();

        if (grupo == null)
        {
            grupo = gameObject.AddComponent<CanvasGroup>();
        }
    }

    void Start()
    {
        slots = new SlotUI[Inventario.TotalSlots];

        for (int i = 0; i < slots.Length; i++)
        {
            slots[i] = Instantiate(slotPrefab, transform);
        }

        Inventario.Instance.OnInventarioMudou += Atualizar;
        Atualizar();
    }

    // A hotbar fica invisível enquanto o jogo está pausado
    void Update()
    {
        grupo.alpha = EstadoJogo.Pausado ? 0f : 1f;
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
        for (int i = 0; i < slots.Length; i++)
        {
            ItemData item = Inventario.Instance.GetItem(i);
            bool selecionado = (i == Inventario.Instance.SlotSelecionado);
            slots[i].Atualizar(item, selecionado);
        }
    }
}