using UnityEngine;
using System.Collections;

public class PlayerAnimate : MonoBehaviour
{
    Sprite[] walking, attacking, legsSpr, cleaverWalk, cleaverAttack, throwingKnives;
    int counter = 0, legCount = 0;
    PlayerMovement pm;
    float timer = 0.051f, legTimer = 0.051f;
    public SpriteRenderer torso, legs;
    SpriteContainer sc;
    private bool isAttacking = false;
    public AudioSource attackSound;

    public bool holdingKnife = false; 

    void Start()
    {
        pm = this.GetComponent<PlayerMovement>();
        sc = GameObject.FindGameObjectWithTag("GameController").GetComponent<SpriteContainer>();
        walking = sc.getPlayerUnarmedwalk();
        legsSpr = sc.getPlayerLegs();
        cleaverWalk = sc.getPlayerCleaverWalk();
        cleaverAttack = sc.getPlayerCleaverAttack();
        throwingKnives = sc.getPlayerThrowingKnivesAtk();
        attackSound = GetComponent<AudioSource>();
    }

    void Update()
    {
        animateLegs();
        animateTorso();
        checkInput();
    }

    void checkInput()
    {
        if (Input.GetKeyDown(KeyCode.Z) && !isAttacking) 
        {
            if (holdingKnife)
            {
                walking = sc.getPlayerUnarmedwalk();
                counter = 0;
                timer = 0.051f;
                torso.sprite = walking[counter];
                holdingKnife = false; 
            }
            else
            {
                walking = cleaverWalk;
                counter = 0;
                timer = 0.051f;
                torso.sprite = walking[counter];
                holdingKnife = true; 
            }
        }

        if (Input.GetMouseButton(0) && holdingKnife && !isAttacking)
        {
            StartCoroutine(PlayCleaverAttackAnimation(0.4f));
            PlayAttackSound();
        }
        
        else if (Input.GetMouseButton(1) && holdingKnife && !isAttacking)
        {
            StartCoroutine(Faquinha());
        }
    }
    
    IEnumerator Faquinha()
    {
        isAttacking = true;

        Sprite[] originalWalking = walking;

        walking = sc.getPlayerThrowingKnivesAtk(); 
        counter = 0;
        timer = 0.051f;

        float delay = 0.05f;

        for (int i = 0; i < walking.Length; i++)
        {
            torso.sprite = walking[counter];
            yield return new WaitForSeconds(delay);
            counter = (counter + 1) % walking.Length;
        }

        walking = originalWalking;
        counter = 0;
        timer = 0.051f;
        torso.sprite = walking[counter];

        isAttacking = false;
    }

    IEnumerator PlayCleaverAttackAnimation(float colliderActiveTime)
    {
        isAttacking = true; 

        Sprite[] originalWalking = walking; 

        walking = cleaverAttack; 
        counter = 0;
        timer = 0.051f;

        float delay = 0.05f; 

        
        GameObject[] colisorATKObjects = GameObject.FindGameObjectsWithTag("ColisorATK");

        
        foreach (GameObject obj in colisorATKObjects)
        {
            BoxCollider2D collider = obj.GetComponent<BoxCollider2D>();
            if (collider != null)
            {
                collider.enabled = true;
                yield return new WaitForSeconds(0f); 
                collider.enabled = false; 
            }
        }

        for (int i = 0; i < cleaverAttack.Length; i++)
        {
            torso.sprite = walking[counter];
            yield return new WaitForSeconds(delay);
            counter = (counter + 1) % walking.Length;
        }

        walking = originalWalking; 
        counter = 0;
        timer = 0.051f;
        torso.sprite = walking[counter];

        
        foreach (GameObject obj in colisorATKObjects)
        {
            BoxCollider2D collider = obj.GetComponent<BoxCollider2D>();
            if (collider != null)
            {
                collider.enabled = false;
            }
        }

        isAttacking = false; 
    }
    
    void PlayAttackSound()
    {
        if (attackSound != null) 
        {
            attackSound.Play(); 
        }
    }
    
    void animateTorso()
    {
        if (pm.moving == true && walking != cleaverAttack) 
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