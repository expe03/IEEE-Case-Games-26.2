using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    public float moveSpeed = 5f;
<<<<<<< Updated upstream
    // cria uma gaveta para guardar o componente de física do player
    private Rigidbody2D rb;
    // guarda os valores dos eixos x e y que o jogador está pressionando
    private Vector2 movementInput;
=======
    private Rigidbody2D rb;

    private Vector2 movementInput;

    public AudioSource audioSource;
    public AudioClip somPassos;

    public float distanciaEntrePassos = 1.5f;

    private float distanciaAcumulada = 0f;

    private Vector2 ultimaPosicao;

    private bool aguardandoPosicionamento = false;

>>>>>>> Stashed changes
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
<<<<<<< Updated upstream
=======

        ultimaPosicao = rb.position;
    }

    public void ReiniciarPassos()
    {
        distanciaAcumulada = 0f;
        ultimaPosicao = rb.position;
        aguardandoPosicionamento = true;

        audioSource.Stop();
>>>>>>> Stashed changes
    }

    void Update()
    {
        // Player travado (usando uma estação): não lê as teclas
        if (EstadoJogo.PlayerTravado)
        {
            movementInput = Vector2.zero;
            return;
        }

        // Pega o valor horizontal (A/D ou Setas Esquerda/Direita) e retorna -1, 0 ou 1
        movementInput.x = Input.GetAxisRaw("Horizontal");
        // Pega o valor vertical (W/S ou Setas Cima/Baixo) e retorna -1, 0 ou 1
        movementInput.y = Input.GetAxisRaw("Vertical");
        // Normaliza o vetor de movimento para que a velocidade seja constante em todas as direções
        movementInput = movementInput.normalized;
    }
    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movementInput * moveSpeed * Time.fixedDeltaTime);
<<<<<<< Updated upstream
=======

        if (aguardandoPosicionamento)
        {
            ultimaPosicao = rb.position;
            distanciaAcumulada = 0f;
            aguardandoPosicionamento = false;
            return;
        }

        float distanciaPercorrida = Vector2.Distance(ultimaPosicao, rb.position);

        if (movementInput != Vector2.zero)
        {
            distanciaAcumulada += distanciaPercorrida;
        }

        ultimaPosicao = rb.position;

        // verifica se o player percorreu a distância necessária
        if (distanciaAcumulada >= distanciaEntrePassos && !audioSource.isPlaying)
        {
            // reproduz o som dos passos
            audioSource.clip = somPassos;
            audioSource.Play();

            // desconta a distância utilizada
            distanciaAcumulada -= distanciaEntrePassos;
        }
>>>>>>> Stashed changes
    }
}
