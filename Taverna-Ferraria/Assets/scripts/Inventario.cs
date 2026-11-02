using System;
using UnityEngine;

public class Inventario : MonoBehaviour
{
    public static Inventario Instance;

    public const int TotalSlots = 7;

    private ItemData[] slots = new ItemData[TotalSlots];
    private int slotSelecionado = 0;

    public event Action OnInventarioMudou;

    public int SlotSelecionado => slotSelecionado;
    public ItemData ItemSelecionado => slots[slotSelecionado];

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        float scroll = Input.mouseScrollDelta.y;

        if (scroll > 0) MudarSelecao(-1);
        else if (scroll < 0) MudarSelecao(1);
    }

    public ItemData GetItem(int indice)
    {
        return slots[indice];
    }

    public bool AdicionarItem(ItemData item)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i] == null)
            {
                slots[i] = item;
                OnInventarioMudou?.Invoke();
                return true;
            }
        }
        Debug.Log("Inventário cheio!");
        return false;
    }

    public void MudarSelecao(int direcao)
    {
        slotSelecionado = (slotSelecionado + direcao + TotalSlots) % TotalSlots;
        OnInventarioMudou?.Invoke();

        string nome = ItemSelecionado != null ? ItemSelecionado.nomeItem : "vazio";
        Debug.Log("Slot " + (slotSelecionado + 1) + " selecionado: " + nome);
    }
}