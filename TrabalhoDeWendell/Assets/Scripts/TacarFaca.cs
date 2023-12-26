using UnityEngine;

public class TacarFaca : MonoBehaviour
{
    public GameObject knifePrefab; // Prefab da faca
    public float throwForce = 500f; // Força do lançamento da faca
    public float knifeLifetime = 3f; // Tempo de vida da faca em segundos
    public float throwInterval = 0.5f; // Intervalo entre lançamentos de faca em segundos
    public int knifeCount = 0; // Quantidade de facas que o jogador tem

    private PlayerAnimate playerAnimate; // Reference to the PlayerAnimate script
    private float timer; // Timer para controlar o intervalo de lançamento

    private void Start()
    {
        playerAnimate = GetComponent<PlayerAnimate>(); // Get the PlayerAnimate component
    }

    private void Update()
    {
        timer += Time.deltaTime; // Incrementa o timer com o tempo decorrido desde o último quadro

        if (Input.GetMouseButton(1) && playerAnimate.holdingKnife && timer >= throwInterval && knifeCount > 0) // Check if the right mouse button is held down, the player is holding the knife, the timer has reached the throw interval, and the player has at least one knife
        {
            ThrowKnife(); // Call the function to throw the knife
            timer = 0f; // Reset the timer
            knifeCount--; // Decrement the knife count
        }
    }

    private void ThrowKnife()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 throwDirection = (mousePosition - transform.position).normalized;

        // Calculate the rotation angle based on the throw direction
        float angle = Mathf.Atan2(throwDirection.y, throwDirection.x) * Mathf.Rad2Deg;

        // Instantiate the knife with the correct rotation
        GameObject knife = Instantiate(knifePrefab, transform.position, Quaternion.Euler(0f, 0f, angle));

        // Get the Rigidbody2D component of the knife
        Rigidbody2D knifeRigidbody = knife.GetComponent<Rigidbody2D>();

        // Apply a force to throw the knife towards the mouse
        knifeRigidbody.AddForce(throwDirection * throwForce);

        // Destroy the knife after the specified lifetime or when it hits an enemy or scenery
        Destroy(knife, knifeLifetime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("KnifePickup")) // Check if the player collided with a knife pickup
        {
            knifeCount++; // Increment the knife count
            Destroy(other.gameObject); // Destroy the knife pickup
        }
    }
}