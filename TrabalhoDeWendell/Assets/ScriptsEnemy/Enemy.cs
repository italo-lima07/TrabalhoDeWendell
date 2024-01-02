using System;
using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 3f;
    public float detectionRadius = 5f;
    public float detectionAngle = 45f; // Adicione esta linha
    public int vida = 3;
    private bool isdead = false;
    private int dano = 1;
    public float knockbackForce = 10f;

    private Transform player;
    private bool isChasing = false;
    private Rigidbody2D rig;
    
    public Transform[] patrolPoints;
    private int currentPatrolIndex = 0;
    public float patrolSpeed = 2f;
    
    public float meleeRange = 0.08f;
    private float meleeCooldown = 1f;
    private bool canAttack = true;
    
    private Animator animator;

    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        die();
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
            Destroy(gameObject, 1f);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == 8)
        {
            vida--;
            Vector2 adjustPositionDirection = (transform.position - other.transform.position).normalized;
            transform.position = new Vector2(transform.position.x + adjustPositionDirection.x * 1f, transform.position.y + adjustPositionDirection.y * 1f);
            if (rig != null)
            {
                rig.velocity = Vector2.zero;
            }
        }
        if (other.gameObject.tag == "Flecha")
        {
            vida--;
            Vector2 adjustPositionDirection = (transform.position - other.transform.position).normalized;
            transform.position = new Vector2(transform.position.x + adjustPositionDirection.x * 1f, transform.position.y + adjustPositionDirection.y * 1f);
            if (rig != null)
            {
                rig.velocity = Vector2.zero;
            }
        }
    }
    IEnumerator MeleeAttack()
    {
        speed = 0f;
        canAttack = false;
        animator.SetInteger("transition" ,1); // Aciona a animação de ataque
        yield return new WaitForSeconds(meleeCooldown);
        canAttack = true;
        speed = 2f;
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, meleeRange);
    }
}