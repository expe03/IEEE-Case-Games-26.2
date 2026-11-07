using UnityEngine;
using System.Collections.Generic;

public static class EstadoJogo
{
    public static bool PlayerTravado = false;
    public static bool PedidoEntregue = false;

    // itens que o player já coletou ou criou (usado pelo tutorial)
    public static HashSet<ItemData> ItensObtidos = new HashSet<ItemData>();

    // verdadeiro enquanto o jogo está pausado (o menu de pause coloca o tempo em zero)
    public static bool Pausado => Time.timeScale == 0f;

    public static void RegistrarItem(ItemData item)
    {
        if (item != null)
        {
            ItensObtidos.Add(item);
        }
    }
}