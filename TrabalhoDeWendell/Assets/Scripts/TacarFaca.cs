using UnityEngine;

public class TacarFaca : MonoBehaviour
{
    public GameObject knifePrefab; // Prefab da faca
    public float throwForce = 500f; // Força do lançamento da faca
    public float knifeLifetime = 3f; // Tempo de vida da faca em segundos

    private PlayerAnimate playerAnimate; // Reference to the PlayerAnimate script

    private void Start()
    {
        playerAnimate = GetComponent<PlayerAnimate>(); // Get the PlayerAnimate component
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1) && playerAnimate.holdingKnife) // Check if the right mouse button is pressed and the player is holding the knife
        {
            ThrowKnife(); // Call the function to throw the knife
        }
    }

    private void ThrowKnife()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 throwDirection = (mousePosition - transform.position).normalized;

        // Calculate the rotation angle based on the throw direction
        float angle = Mathf.Atan2(throwDirection.y, throwDirection.x) * Mathf.Rad2Deg;

        // Instantiate the knife
        GameObject knife = Instantiate(knifePrefab, transform.position, Quaternion.AngleAxis(angle, Vector3.forward));

        // Get the Rigidbody2D component of the knife
        Rigidbody2D knifeRigidbody = knife.GetComponent<Rigidbody2D>();

        // Apply a force to throw the knife towards the mouse
        knifeRigidbody.AddForce(throwDirection * throwForce);

        // Destroy the knife after the specified lifetime or when it hits an enemy or scenery
        Destroy(knife, knifeLifetime);
    }
}