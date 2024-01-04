using UnityEngine;

public class Teleport : MonoBehaviour
{
    public Transform teleportTarget; // A posição para onde o player será teleportado

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Verifica se o objeto que colidiu é o player
        {
            other.transform.position = teleportTarget.position; // Teleporta o player para a posição especificada
        }
    }
}