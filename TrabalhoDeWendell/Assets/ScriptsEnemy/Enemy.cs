using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 3f;
    public float detectionRadius = 5f;
    public float detectionAngle = 45f;
    public int vida = 3;
    private bool isdead = false;
    private int dano = 1;
    public float knockbackForce = 10f;
    public GameObject deathSprite;
    
    public AudioClip attackSound; 
    private AudioSource audioSource;
    
    
    private Transform player;
    private bool isChasing = false;
    private Rigidbody2D rig;
    
    public Transform[] patrolPoints;
    private int currentPatrolIndex = 0;
    public float patrolSpeed = 2f;
    
    public float meleeRange = 0.08f;
    private float meleeCooldown = 0f;
    private bool canAttack = true;
    
    private Animator animator;

    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    void Update()
    {
        if (!isdead)
        {
            CheckPlayerDistance();

            if (isChasing)
            {
                ChasePlayer();
                if (canAttack && Vector2.Distance(transform.position, player.position) < meleeRange)
                {
                    StartCoroutine(MeleeAttack());
                }
            }
            else
            {
                Patrol();
            }
        }
    }

    void CheckPlayerDistance()
    {
        Vector2 directionToPlayer = player.position - transform.position;
        float angle = Vector2.Angle(transform.right, directionToPlayer);

        isChasing = directionToPlayer.magnitude < detectionRadius && angle < detectionAngle / 2;
    }
    
    void Patrol()
    {
        if (patrolPoints.Length > 0)
        {
            Vector2 target = patrolPoints[currentPatrolIndex].position;
            Vector2 moveDirection = (target - (Vector2)transform.position).normalized;
            rig.velocity = moveDirection * patrolSpeed;

            // Rotacionar na direção do ponto de patrulha
            transform.right = moveDirection;

            if (Vector2.Distance(transform.position, target) < 0.1f)
            {
                currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
            }
        }
    }
    
    void ChasePlayer()
    {
        if (rig != null)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            rig.velocity = direction * speed;

            // Verifica se o jogador está no campo de visão do inimigo
            if (Vector2.Distance(transform.position, player.position) < detectionRadius)
            {
                // Rotaciona na direção do jogador
                transform.right = direction;
            }
        }
    }

    void die()
    {
        if (vida <= 0)
        {
            isdead = true;
            float angle = UnityEngine.Random.Range(0f, 360f);
            Instantiate(deathSprite, transform.position, Quaternion.Euler(0f, 0f, angle));
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == 8 || other.gameObject.tag == "Knive")
        {
            vida--;
            Vector2 adjustPositionDirection = (transform.position - other.transform.position).normalized;
            transform.position = new Vector2(transform.position.x + adjustPositionDirection.x * 1f, transform.position.y + adjustPositionDirection.y * 1f);
            if (rig != null)
            {
                rig.velocity = Vector2.zero;
            }

            die(); // Call the die method when the enemy is hit
        }

        if (other.gameObject.layer == 8 || other.gameObject.tag == "ColisorATK")
        {
            vida = 0;
        }
    }

    IEnumerator MeleeAttack()
    {
        speed = 0f;
        canAttack = false;
        animator.SetInteger("transition", 1);
        yield return new WaitForSeconds(0.2f);
        if (Vector2.Distance(transform.position, player.position) > meleeRange)
        {
            StopCoroutine(MeleeAttack());
        }
        
        else
        {
            player.GetComponent<PlayerLife>().TakeDamage(dano);
        }
        
        yield return new WaitForSeconds(meleeCooldown);
        canAttack = true;
        speed = 2f;
        animator.SetInteger("transition", 0);
        audioSource.PlayOneShot(attackSound);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, meleeRange);
    }
}