using UnityEngine;

public class HotbarUI : MonoBehaviour
{
    public SlotUI slotPrefab;
    private SlotUI[] slots;

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
