using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAIGUN : MonoBehaviour
{
    public GameObject player;
    public bool patrol = true, guard = false, clockwise = false;
    public bool moving = true;
    public bool pursuingPlayer = false, goingToLastLoc = false;
    Vector3 target;
    Rigidbody2D rid;
    public Vector3 playerLastPos;
    RaycastHit2D hit;
    float speed = 2.0f;
    int layerMask = 1 << 8;

    // Variáveis para controle do tiro
    public GameObject bulletPrefab;
    public float fireInterval = 1.5f;
    public int bulletsToFire = 3;
    private float nextFireTime;
    public float desiredDistance = 10f;
    public float bulletSpeed = 5f; // Velocidade da bala

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerLastPos = this.transform.position;
        rid = this.GetComponent<Rigidbody2D>();
        layerMask = ~layerMask;
        nextFireTime = Time.time + fireInterval;
    }

    void Update()
    {
        movement();
        playerDetect();
        Fire();
    }

    void movement()
    {
        if (pursuingPlayer)
        {
            // Calcula a direção do jogador
            Vector3 directionToPlayer = (player.transform.position - transform.position).normalized;

            // Verifica se a distância entre o inimigo e o jogador é maior que a distância desejada
            float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
            if (distanceToPlayer > desiredDistance)
            {
                // Move o inimigo na direção do jogador
                rid.velocity = directionToPlayer * speed;
            }
            else
            {
                // Para o inimigo se a distância desejada for alcançada
                rid.velocity = Vector3.zero;
            }
        }
        else if (patrol)
        {
            // Move o inimigo aleatoriamente pelo mapa
            float randomX = Random.Range(-1f, 1f);
            float randomY = Random.Range(-1f, 1f);
            Vector3 randomDirection = new Vector3(randomX, randomY, 0f).normalized;
            rid.velocity = randomDirection * speed;
        }
        else if (goingToLastLoc)
        {
            Debug.Log("Checking last known player location");
            speed = 3.0f;
            rid.transform.eulerAngles = new Vector3(0, 0, Mathf.Atan2(playerLastPos.y - transform.position.y, playerLastPos.x - transform.position.x) * Mathf.Rad2Deg);

            if (Vector3.Distance(transform.position, playerLastPos) < 1.5f)
            {
                patrol = true;
                goingToLastLoc = false;
            }
        }
    }

    public void playerDetect()
    {
        Vector3 directionToPlayer = player.transform.position - transform.position;
        float distanceToPlayer = directionToPlayer.magnitude;

        if (distanceToPlayer < desiredDistance)
        {
            RaycastHit2D playerHit = Physics2D.Raycast(transform.position, directionToPlayer, distanceToPlayer, layerMask);

            if (playerHit.collider != null && playerHit.collider.gameObject.tag == "Player")
            {
                playerLastPos = player.transform.position;
                patrol = false;
                pursuingPlayer = true;
                goingToLastLoc = false;
            }
            else if (pursuingPlayer)
            {
                goingToLastLoc = true;
                pursuingPlayer = false;
            }
        }
    }
    
    

    void Fire()
    {
        if (pursuingPlayer || goingToLastLoc)
        {
            // Verifica se está na hora de disparar
            if (Time.time > nextFireTime)
            {
                // Verifica se o inimigo está a uma distância específica do jogador
                float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
                if (distanceToPlayer <= desiredDistance)
                {
                    // Calcula a direção do jogador
                    Vector3 directionToPlayer = (player.transform.position - transform.position).normalized;

                    // Dispara as balas
                    for (int i = 0; i < bulletsToFire; i++)
                    {
                        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
                        bullet.GetComponent<Rigidbody2D>().velocity = directionToPlayer * bulletSpeed;
                    }

                    // Atualiza o próximo tempo de disparo
                    nextFireTime = Time.time + fireInterval;
                }
            }
        }
    }
}
