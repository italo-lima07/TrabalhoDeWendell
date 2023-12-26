using UnityEngine;

public class PlayerLife : MonoBehaviour
{
    public int life = 1; // Vida do jogador
    public GameObject deathSpritePrefab; // Prefab do sprite de morte

    private void Update()
    {
        if (life <= 0) // Verifica se a vida do jogador é zero ou menos
        {
            Die(); // Chama a função para matar o jogador
        }
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