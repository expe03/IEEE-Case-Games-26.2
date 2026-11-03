using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    // define a velocidade que o player vai ter
    public float moveSpeed = 5f;

    // cria uma gaveta para guardar o componente de física do player
    private Rigidbody2D rb;

    // guarda os valores dos eixos x e y que o jogador está pressionando
    private Vector2 movementInput;

    // guarda o componente de áudio e o arquivo de som dos passos
    public AudioSource audioSource;
    public AudioClip somPassos;

    // define a distância que o player precisa percorrer para tocar os passos novamente
    public float distanciaEntrePassos = 1.5f;

    // guarda a distância percorrida desde o último som
    private float distanciaAcumulada = 0f;

    // guarda a última posição do player
    private Vector2 ultimaPosicao;

    // impede que os passos sejam contabilizados durante a troca de área
    private bool aguardandoPosicionamento = false;

    void Start()
    {
        // o GetComponent busca o componente de física do player e guarda na variável rb
        rb = GetComponent<Rigidbody2D>();

        // registra a posição inicial do player
        ultimaPosicao = rb.position;
    }

    // Reinicia o controle dos passos quando o player troca de área
    public void ReiniciarPassos()
    {
        distanciaAcumulada = 0f;
        ultimaPosicao = rb.position;
        aguardandoPosicionamento = true;

        // Interrompe qualquer som de passo anterior
        audioSource.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        // Pega o valor horizontal (A/D ou Setas Esquerda/Direita) e retorna -1, 0 ou 1
        movementInput.x = Input.GetAxisRaw("Horizontal");

        // Pega o valor vertical (W/S ou Setas Cima/Baixo) e retorna -1, 0 ou 1
        movementInput.y = Input.GetAxisRaw("Vertical");

        // Normaliza o vetor de movimento para que a velocidade seja constante em todas as direções
        movementInput = movementInput.normalized;
    }

    void FixedUpdate()
    {
        // Move a posição atual do Rigidbody somando a direção multiplicada pela velocidade e pelo tempo entre frames
        rb.MovePosition(rb.position + movementInput * moveSpeed * Time.fixedDeltaTime);

        // Ignora a movimentação inicial depois de trocar de área
        if (aguardandoPosicionamento)
        {
            ultimaPosicao = rb.position;
            distanciaAcumulada = 0f;
            aguardandoPosicionamento = false;
            return;
        }

        // calcula a distância real percorrida desde a última verificação
        float distanciaPercorrida = Vector2.Distance(ultimaPosicao, rb.position);

        // só soma a distância quando o jogador estiver se movimentando
        if (movementInput != Vector2.zero)
        {
            distanciaAcumulada += distanciaPercorrida;
        }

        // atualiza a última posição do player
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
    }
}
