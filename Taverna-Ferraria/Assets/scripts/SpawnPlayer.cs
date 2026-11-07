using UnityEngine;
using System.Collections;

public class SpawnPlayer : MonoBehaviour
{
    IEnumerator Start()
    {
        string nomeSpawn = GerenciadorSpawn.spawnDesejado;

        Debug.Log("Spawn solicitado: " + nomeSpawn);

        GameObject ponto = GameObject.Find(nomeSpawn);

        if (ponto != null)
        {
            // posiciona o jogador no Spawn desejado
            transform.position = ponto.transform.position;

            // aguarda a inicialização dos outros componentes
            yield return new WaitForFixedUpdate();

            // reinicia o controle dos passos após posicionar o jogador
            GetComponent<NewMonoBehaviourScript>().ReiniciarPassos();

            Debug.Log("Player posicionado em: " + ponto.name);
            Debug.Log("Posição: " + ponto.transform.position);
        }
        else
        {
            Debug.LogWarning("Spawn não encontrado: " + nomeSpawn);
        }
    }
}
