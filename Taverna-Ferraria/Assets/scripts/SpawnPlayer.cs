using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{
    void Start()
    {
        string nomeSpawn = GerenciadorSpawn.spawnDesejado;

        Debug.Log("Spawn solicitado: " + nomeSpawn);

        GameObject ponto = GameObject.Find(nomeSpawn);

        if (ponto != null)
        {
            transform.position = ponto.transform.position;

            Debug.Log("Player posicionado em: " + ponto.name);
            Debug.Log("Posição: " + ponto.transform.position);
        }
        else
        {
            Debug.LogWarning("Spawn não encontrado: " + nomeSpawn);
        }
    }
}
