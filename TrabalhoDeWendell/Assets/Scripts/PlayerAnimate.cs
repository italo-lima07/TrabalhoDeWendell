using UnityEngine;
using System.Collections;

public class PlayerAnimate : MonoBehaviour
{
    Sprite[] walking, attacking, legsSpr, cleaverWalk, cleaverAttack;
    int counter = 0, legCount = 0;
    PlayerMovement pm;
    float timer = 0.051f, legTimer = 0.051f;
    public SpriteRenderer torso, legs;
    SpriteContainer sc;
    private bool isAttacking = false;

    bool holdingKnife = false; // Add a variable to track if the player is holding the knife

    void Start()
    {
        pm = this.GetComponent<PlayerMovement>();
        sc = GameObject.FindGameObjectWithTag("GameController").GetComponent<SpriteContainer>();
        walking = sc.getPlayerUnarmedwalk();
        legsSpr = sc.getPlayerLegs();
        cleaverWalk = sc.getPlayerCleaverWalk();
        cleaverAttack = sc.getPlayerCleaverAttack(); // Add this line to get the Cleaver Attack sprites
    }

    void Update()
    {
        animateLegs();
        animateTorso();
        checkInput();
    }

    void checkInput()
    {
        if (Input.GetKeyDown(KeyCode.Z) && !isAttacking) // Check if "Z" is pressed and not currently attacking
        {
            if (holdingKnife)
            {
                walking = sc.getPlayerUnarmedwalk();
                counter = 0;
                timer = 0.051f;
                torso.sprite = walking[counter];
                holdingKnife = false; // Set holdingKnife to false since the player is no longer holding the knife
            }
            else
            {
                walking = cleaverWalk;
                counter = 0;
                timer = 0.051f;
                torso.sprite = walking[counter];
                holdingKnife = true; // Set holdingKnife to true since the player is now holding the knife
            }
        }

        if (Input.GetMouseButton(0) && holdingKnife && !isAttacking)
        {
            StartCoroutine(PlayCleaverAttackAnimation());
        }
    }

    IEnumerator PlayCleaverAttackAnimation()
    {
        isAttacking = true; // Set the flag to indicate that the attack animation is in progress

        Sprite[] originalWalking = walking; // Store the original walking sprites

        walking = cleaverAttack; // Set the walking sprites to cleaverAttack
        counter = 0;
        timer = 0.051f;

        float delay = 0.05f; // Adjust the delay between each sprite (lower value for faster animation)

        // Find and store the references to objects with the tag "ColisorATK"
        GameObject[] colisorATKObjects = GameObject.FindGameObjectsWithTag("ColisorATK");

        // Activate the objects
        foreach (GameObject obj in colisorATKObjects)
        {
            obj.SetActive(true);
        }

        for (int i = 0; i < cleaverAttack.Length; i++)
        {
            torso.sprite = walking[counter];
            yield return new WaitForSeconds(delay);
            counter = (counter + 1) % walking.Length;
        }

        // Deactivate the objects
        foreach (GameObject obj in colisorATKObjects)
        {
            obj.SetActive(false);
        }

        walking = originalWalking; // Restore the original walking sprites
        counter = 0;
        timer = 0.051f;
        torso.sprite = walking[counter];

        isAttacking = false; // Reset the flag to indicate that the attack animation is complete
    }
    void animateTorso()
    {
        if (pm.moving == true && walking != cleaverAttack) // Only animate torso if not in Cleaver Attack animation
        {
            torso.sprite = walking[counter];
            timer += Time.deltaTime;
            if (timer >= 0.1f)
            {
                counter = (counter + 1) % walking.Length;
                timer = 0f;
            }
        }
    }

    void animateLegs()
    {
        if (pm.moving == true)
        {
            legs.sprite = legsSpr[legCount];
            legTimer += Time.deltaTime;
            if (legTimer >= 0.05f)
            {
                legCount = (legCount + 1) % legsSpr.Length;
                legTimer = 0f;
            }
        }
    }
}