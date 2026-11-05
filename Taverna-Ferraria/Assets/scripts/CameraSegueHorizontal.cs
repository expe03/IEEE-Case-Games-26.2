using UnityEngine;

public class CameraSegueHorizontal : MonoBehaviour
{
    public Transform alvo;
    public SpriteRenderer fundo;
    public bool ajustarAlturaAoFundo = false;

    private Camera cam;
    private float yFixo;

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

        if (ajustarAlturaAoFundo && fundo != null)
        {
            cam.orthographicSize = fundo.bounds.size.y / 2f;
            yFixo = fundo.bounds.center.y;
        }
        else
        {
            yFixo = transform.position.y;
        }
    }

    void LateUpdate()
    {
        if (alvo == null) return;

        float x = alvo.position.x;

        if (fundo != null)
        {
            float metadeLargura = cam.orthographicSize * cam.aspect;
            float minX = fundo.bounds.min.x + metadeLargura;
            float maxX = fundo.bounds.max.x - metadeLargura;

            if (minX > maxX)
            {
                // cenário mais estreito que a tela: fica centralizado
                x = fundo.bounds.center.x;
            }
            else
            {
                x = Mathf.Clamp(x, minX, maxX);
            }
        }

        transform.position = new Vector3(x, yFixo, transform.position.z);
    }
}