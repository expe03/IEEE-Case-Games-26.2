using UnityEngine;
using UnityEngine.SceneManagement;
public class Interacao : MonoBehaviour
{
    public GameObject promptInteracao;
    public string nomeCena;
    public string nomeSpawn;
    private bool jogadorPerto;

    void Start()
    {
        promptInteracao.SetActive(false);
    }

    void Update()
    {
        if (jogadorPerto && Input.GetKeyDown(KeyCode.E))
        {
            GerenciadorSpawn.spawnDesejado = nomeSpawn;
            SceneManager.LoadScene(nomeCena);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            jogadorPerto = true;
            promptInteracao.SetActive(true);
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
