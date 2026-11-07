using System;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    // Voltou ao menu principal: o próximo jogo começa com o inventário vazio
    void AoCarregarCena(Scene cena, LoadSceneMode modo)
    {
        if (cena.name == "Main Menu")
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (EstadoJogo.Pausado) return;
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
                EstadoJogo.RegistrarItem(item);
                OnInventarioMudou?.Invoke();
                return true;
            }
        }
        Debug.Log("Inventário cheio!");
        return false;
    }

    public ItemData RemoverItemSelecionado()
    {
        ItemData item = slots[slotSelecionado];
        slots[slotSelecionado] = null;
        OnInventarioMudou?.Invoke();
        return item;
    }

    public bool ReceberNoSlotSelecionado(ItemData item)
    {
        if (slots[slotSelecionado] != null) return false;

        slots[slotSelecionado] = item;
        OnInventarioMudou?.Invoke();
        return true;
    }

    public void MudarSelecao(int direcao)
    {
        slotSelecionado = (slotSelecionado + direcao + TotalSlots) % TotalSlots;
        OnInventarioMudou?.Invoke();

        string nome = ItemSelecionado != null ? ItemSelecionado.nomeItem : "vazio";
        Debug.Log("Slot " + (slotSelecionado + 1) + " selecionado: " + nome);
    }
}