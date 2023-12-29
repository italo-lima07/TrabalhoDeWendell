using UnityEngine;

public class PlayerStatic : MonoBehaviour
{
    private float initialDelay = 10f;
    private float maxDelay = 50f;
    private float currentDelay = 0f;
    private bool isStatic = true;

    private void Start()
    {
        currentDelay = Random.Range(initialDelay, maxDelay);
        Invoke("EnableMovement", currentDelay);
    }

    private void Update()
    {
        if (isStatic)
        {
            // Execute qualquer lógica adicional enquanto o jogador estiver estático, se necessário
        }
        else
        {
            // Lógica de movimento normal do jogador aqui
        }
    }

    private void EnableMovement()
    {
        isStatic = false;
    }
}