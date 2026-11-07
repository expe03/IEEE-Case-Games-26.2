using UnityEngine;

public class CameraSegue : MonoBehaviour
{
    public Transform alvo;
    public SpriteRenderer fundo;

    public bool seguirHorizontal = true;
    public bool seguirVertical = true;

    [Header("Usados só se o campo Fundo estiver vazio")]
    public Vector2 limiteMin = new Vector2(-10f, -5f);
    public Vector2 limiteMax = new Vector2(10f, 5f);

    private Camera cam;

    void Awake()
    {
        cam = GetComponent<Camera>();
    }

    void Start()
    {
        if (alvo == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");

            if (player != null)
            {
                alvo = player.transform;
            }
        }
    }

    Bounds PegarArea()
    {
        if (fundo != null) return fundo.bounds;

        Vector3 centro = (limiteMin + limiteMax) / 2f;
        Vector3 tamanho = limiteMax - limiteMin;
        return new Bounds(centro, tamanho);
    }

    void LateUpdate()
    {
        if (alvo == null) return;

        Bounds area = PegarArea();

        float metadeAltura = cam.orthographicSize;
        float metadeLargura = metadeAltura * cam.aspect;

        Vector3 pos = transform.position;

        if (seguirHorizontal)
        {
            pos.x = Limitar(alvo.position.x, area.min.x + metadeLargura, area.max.x - metadeLargura, area.center.x);
        }

        if (seguirVertical)
        {
            pos.y = Limitar(alvo.position.y, area.min.y + metadeAltura, area.max.y - metadeAltura, area.center.y);
        }

        transform.position = pos;
    }

    // Segura o valor entre o mínimo e o máximo; se o cenário for menor que a tela, centraliza
    float Limitar(float valor, float min, float max, float centro)
    {
        if (min > max) return centro;
        return Mathf.Clamp(valor, min, max);
    }

    // Mostra no Scene view o retângulo dos limites manuais, quando a câmera está selecionada
    void OnDrawGizmosSelected()
    {
        if (fundo != null) return;

        Gizmos.color = Color.yellow;
        Vector3 centro = (limiteMin + limiteMax) / 2f;
        Vector3 tamanho = limiteMax - limiteMin;
        Gizmos.DrawWireCube(centro, tamanho);
    }
}