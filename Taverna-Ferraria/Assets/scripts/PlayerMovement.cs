using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    // define a velocidade que o player vai ter
    public float moveSpeed = 5f;
    // cria uma gaveta para guardar o componente de física do player
    private Rigidbody2D rb;
    // guarda os valores dos eixos x e y que o jogador está pressionando
    private Vector2 movementInput;
    void Start()
    {
        // o GetComponent busca o componente de física do player e guarda na variável rb
        rb = GetComponent<Rigidbody2D>();
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
    }
}
