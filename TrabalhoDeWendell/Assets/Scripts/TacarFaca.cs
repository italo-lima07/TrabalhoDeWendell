using UnityEngine;

public class TacarFaca : MonoBehaviour
{
    public GameObject knifePrefab; // Prefab da faca
    public float throwForce = 500f; // Força do lançamento da faca
    public float knifeLifetime = 3f; // Tempo de vida da faca em segundos
    public float throwInterval = 0.5f; // Intervalo entre lançamentos de faca em segundos
    public int knifeCount = 0; // Quantidade de facas que o jogador tem

    private PlayerAnimate playerAnimate; // Referência ao script PlayerAnimate
    private float timer; // Timer para controlar o intervalo de lançamento

    private void Start()
    {
        playerAnimate = GetComponent<PlayerAnimate>(); // Obtém o componente PlayerAnimate
    }

    private void Update()
    {
        timer += Time.deltaTime; // Incrementa o timer com o tempo decorrido desde o último quadro

        if (Input.GetMouseButton(1) && playerAnimate.holdingKnife && timer >= throwInterval && knifeCount > 0) // Verifica se o botão direito do mouse está pressionado, o jogador está segurando a faca, o tempo de intervalo foi atingido e o jogador tem pelo menos uma faca
        {
            ThrowKnife(); // Chama a função para lançar a faca
            timer = 0f; // Reseta o timer
            knifeCount--; // Decrementa o contador de facas
        }
    }

    private void ThrowKnife()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 throwDirection = (mousePosition - transform.position).normalized;

        // Calcula o ângulo de rotação com base na direção do lançamento
        float angle = Mathf.Atan2(throwDirection.y, throwDirection.x) * Mathf.Rad2Deg;

        // Instancia a faca com a rotação correta
        GameObject knife = Instantiate(knifePrefab, transform.position, Quaternion.Euler(0f, 0f, angle));

        // Obtém o componente Rigidbody2D da faca
        Rigidbody2D knifeRigidbody = knife.GetComponent<Rigidbody2D>();

        // Aplica uma força para lançar a faca em direção ao mouse
        knifeRigidbody.AddForce(throwDirection * throwForce);

        // Define a faca como trigger (sensor)
        knife.GetComponent<Collider2D>().isTrigger = true;

        // Anexa um script à faca para detectar colisões com objetos marcados como "Cenario"
        KnifeCollisionDetection collisionDetection = knife.AddComponent<KnifeCollisionDetection>();
        collisionDetection.knifeOwner = gameObject;

        // Destroi a faca após o tempo de vida especificado
        Destroy(knife, knifeLifetime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("KnifePickup")) // Verifica se o jogador colidiu com uma faca
        {
            knifeCount++; // Incrementa o contador de facas
            Destroy(other.gameObject); // Destroi a faca
        }
    }
}



public class KnifeCollisionDetection : MonoBehaviour
{
    public GameObject knifeOwner;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Cenario")) // Verifica se a faca colidiu com um objeto marcado como "Cenario"
        {
            Destroy(gameObject); // Destroi a faca
        }

        if (collision.gameObject.CompareTag("inimigo"))
        {
            Destroy(gameObject);
        }
    }
}