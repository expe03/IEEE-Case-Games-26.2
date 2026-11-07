using UnityEngine;
using UnityEngine.UI;

public class SlotUI : MonoBehaviour
{
    public Image icone;
    public GameObject destaque;

    public void Atualizar(ItemData item, bool selecionado)
    {
        if (item != null)
        {
            icone.sprite = item.icone;
            icone.enabled = true;
        }
        else
        {
            icone.sprite = null;
            icone.enabled = false;

        }

        destaque.SetActive(selecionado);
    }
}
