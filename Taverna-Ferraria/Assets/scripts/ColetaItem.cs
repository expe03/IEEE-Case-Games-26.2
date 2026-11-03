using UnityEngine;

public class ColetaItem : MonoBehaviour
{
    public ItemData item;
    public GameObject promptInteracao;
    public bool coletaUnica = true;
    public AudioClip somColeta;
    private AudioSource audioSource;

    private bool jogadorPerto;
    private bool jaColetado;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (promptInteracao != null)
        {
            promptInteracao.SetActive(false);
        }
    }

    void Update()
    {
        if (jogadorPerto && !jaColetado && Input.GetKeyDown(KeyCode.E))
        {
            bool coletou = Inventario.Instance.AdicionarItem(item);

            if (coletou && somColeta != null)
            {
                AudioSource.PlayClipAtPoint(somColeta, transform.position, 1f);
            }

            if (coletou && coletaUnica)
            {
                jaColetado = true;

                if (promptInteracao != null)
                {
                    promptInteracao.SetActive(false);
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !jaColetado)
        {
            jogadorPerto = true;

            if (promptInteracao != null)
            {
                promptInteracao.SetActive(true);
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            jogadorPerto = false;

            if (promptInteracao != null)
            {
                promptInteracao.SetActive(false);
            }
        }
    }
}
