using UnityEngine;

public class PlayerLife : MonoBehaviour
{
    public int life = 1; // Vida do jogador
    public GameObject deathSpritePrefab; // Prefab do sprite de morte

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            TakeDamage(1); // O jogador perde 1 de vida ao colidir com um objeto inimigo
        }
    }

    private void Update()
    {
        if (life <= 0) // Verifica se a vida do jogador é zero ou menos
        {
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