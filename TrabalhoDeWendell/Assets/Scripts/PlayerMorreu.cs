using UnityEngine;

public class PlayerMorreu : MonoBehaviour
{
    private Animator animator; // Reference to the Animator component

    private bool isDead = false; // Flag to track if the player is dead

    private void Start()
    {
        animator = GetComponent<Animator>(); // Get the Animator component
    }

    private void Update()
    {
        // Check if the player is dead
        if (isDead)
        {
            // Play the death animation
            animator.SetTrigger("Death");
        }
    }

    public void Die()
    {
        // Set the isDead flag to true
        isDead = true;

        // Disable other components or perform other actions related to death
        // ...
    }
}