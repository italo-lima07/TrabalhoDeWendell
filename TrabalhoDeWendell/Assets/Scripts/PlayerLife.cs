using UnityEngine;

public class PlayerLife : MonoBehaviour
{
    public int life = 1; // Vida do jogador
    public GameObject deathSpritePrefab; // Prefab do sprite de morte
    private AudioSource audioSource; // Referência ao componente AudioSource
    private Rigidbody2D rig;

    public GameManagerScript gameManager;
    private bool isDead;

    private void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>(); // Obtém a referência ao componente AudioSource
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            rig.velocity = new Vector2(rig.velocity.x, 0f);
            TakeDamage(1); // O jogador perde 1 de vida ao colidir com um objeto inimigo
        }
    }

    private void Update()
    {
        if (life <= 0 && !isDead) // Verifica se a vida do jogador é zero ou menos
        {
            isDead = true;
            gameManager.gameOver();
            Die(); // Chama a função para matar o jogador
        }
    }

    public void TakeDamage(int damage)
    {
        life -= damage; // Reduz a vida do jogador pelo valor do dano recebido
    }

    private void Die()
    {
        // Gera um ângulo aleatório para a rotação do sprite de morte
        float angle = Random.Range(0f, 360f);

        // Substitui o jogador pelo sprite de morte com a rotação aleatória
        Instantiate(deathSpritePrefab, transform.position, Quaternion.Euler(0f, 0f, angle));

        // Destroi o objeto do jogador
        Destroy(gameObject);
    }
}