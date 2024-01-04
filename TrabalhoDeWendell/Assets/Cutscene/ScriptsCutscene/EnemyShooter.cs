using System;
using System.Collections;
using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
    public float speed = 3f;
    public float detectionRadius = 5f;
    public float detectionAngle = 45f;
    public int vida = 3;
    private bool isdead = false;
    private int dano = 1;
    public float knockbackForce = 10f;
    public GameObject deathSprite;
    
    public GameObject bulletPrefab;
    public float fireRate = 1f;
    public float bulletSpeed = 5f;
    public float shootingDistance = 5f;

    private bool isShooting = false;
    
    private Transform player;
    private bool isChasing = false;
    private Rigidbody2D rig;
    
    public Transform[] patrolPoints;
    private int currentPatrolIndex = 0;
    public float patrolSpeed = 2f;
    
    private Animator animator;

    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
        StartCoroutine(ShootRoutine());
    }

    void Update()
    {
        if (!isdead)
        {
            CheckPlayerDistance();

            if (isChasing)
            {
                ChasePlayer();
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
            if (Vector2.Distance(transform.position, player.position) > shootingDistance)
            {
                rig.velocity = direction * speed;
            }
            else
            {
                rig.velocity = Vector2.zero; // Para de se mover quando estiver na distância de tiro
            }
            transform.right = direction;
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
    
    IEnumerator ShootRoutine()
    {
        while (true)
        {
            if (isChasing)
            {
                Shoot();
            }
            yield return new WaitForSeconds(1f / fireRate);
        }
    }

    void Shoot()
    {
        if (!isShooting)
        {
            isShooting = true;
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            Vector2 direction = (player.position - transform.position).normalized;
            bullet.transform.right = direction; // Aponta a bala na direção correta
            Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
            bulletRb.velocity = direction * bulletSpeed;
            StartCoroutine(DestroyBulletAfterTime(bullet));
        }
    }

    IEnumerator DestroyBulletAfterTime(GameObject bullet)
    {
        yield return new WaitForSeconds(2f); // Altere o tempo conforme necessário
        Destroy(bullet);
        isShooting = false;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}