using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "NovaReceita", menuName = "Jogo/Receita")]
public class ReceitaData : ScriptableObject
{
    public List<ItemData> ingredientes;
    public ItemData resultado;
    public float duracao = 3f;
    public bool prendePlayer = true;
    public bool mostraBarra = true;
    public bool iniciaAutomatico = false;

    // Confere se os itens colocados formam exatamente esta receita, em qualquer ordem
    public bool Combina(List<ItemData> itens)
    {
        if (itens.Count != ingredientes.Count) return false;

        List<ItemData> copia = new List<ItemData>(itens);

        foreach (ItemData ingrediente in ingredientes)
        {
            if (!copia.Remove(ingrediente)) return false;
        }

        return true;
    }
}
