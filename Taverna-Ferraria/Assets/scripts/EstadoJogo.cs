using System.Collections.Generic;

public static class EstadoJogo
{
    public static bool PlayerTravado = false;
    public static bool PedidoEntregue = false;

    // itens que o player já coletou ou criou (usado pelo tutorial)
    public static HashSet<ItemData> ItensObtidos = new HashSet<ItemData>();

    public static void RegistrarItem(ItemData item)
    {
        if (item != null)
        {
            ItensObtidos.Add(item);
        }
    }
}