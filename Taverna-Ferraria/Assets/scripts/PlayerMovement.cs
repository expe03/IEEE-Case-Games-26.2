using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
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

    // controle das animações
    private Animator animator;
    private bool olhandoDireita = true;
    private string animacaoAtual = "";

    public bool OlhandoDireita => olhandoDireita;

    // sprites parados (idle), um para cada lado
    public Sprite idleDireita;
    public Sprite idleEsquerda;
    private SpriteRenderer sr;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();

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

    void Update()
    {
        // Player travado (usando uma estação): não lê as teclas
        if (EstadoJogo.PlayerTravado)
        {
            movementInput = Vector2.zero;
            AtualizarAnimacao();
            return;
        }

        // Pega o valor horizontal (A/D ou Setas Esquerda/Direita) e retorna -1, 0 ou 1
        movementInput.x = Input.GetAxisRaw("Horizontal");

        // Pega o valor vertical (W/S ou Setas Cima/Baixo) e retorna -1, 0 ou 1
        movementInput.y = Input.GetAxisRaw("Vertical");

        // Normaliza o vetor de movimento para que a velocidade seja constante em todas as direções
        movementInput = movementInput.normalized;

        AtualizarAnimacao();
    }

    // Escolhe qual animação tocar: andando ou parado, olhando para a esquerda ou para a direita
    void AtualizarAnimacao()
    {
        if (animator == null || sr == null) return;

        // Só muda o lado quando há movimento horizontal; andando para cima ou para baixo, mantém o último lado
        if (movementInput.x > 0) olhandoDireita = true;
        else if (movementInput.x < 0) olhandoDireita = false;

        bool andando = movementInput != Vector2.zero;

        if (andando)
        {
            string novaAnimacao = olhandoDireita ? "Walk_Right" : "Walk_Left";

            // religa o Animator se ele estava desligado e só troca de animação quando muda
            if (!animator.enabled || novaAnimacao != animacaoAtual)
            {
                animator.enabled = true;
                animator.Play(novaAnimacao);
                animacaoAtual = novaAnimacao;
            }
        }
        else
        {
            // parado: desliga o Animator e mostra o sprite parado do lado certo
            animator.enabled = false;
            sr.sprite = olhandoDireita ? idleDireita : idleEsquerda;
            animacaoAtual = "";
        }
    }

    void FixedUpdate()
    {
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