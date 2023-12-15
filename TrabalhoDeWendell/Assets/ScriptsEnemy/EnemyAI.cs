using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    GameObject player;
    public bool patrol = true, guard = false, clockwise = false;
    public bool moving = true;
    public bool pursuingPlayer = false, goingToLastLoc = false;
    Vector3 target;
    Rigidbody2D rid;
    public Vector3 playerLastPos; // Corrigido para public Vector3 playerLastPos;
    RaycastHit2D hit;
    float speed = 2.0f;
    int layerMask = 1 << 8;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerLastPos = this.transform.position;
        rid = this.GetComponent<Rigidbody2D>();
        layerMask = ~layerMask;
    }

    void Update()
    {
        movement();
        playerDetect();
    }

    void movement()
    {
        float dist = Vector3.Distance(player.transform.position, this.transform.position);
        Vector3 dir = player.transform.position - transform.position;
        hit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), new Vector2(dir.x, dir.y), dist, layerMask);
        Debug.DrawRay(transform.position, dir, Color.red);
        Vector3 fwt = transform.TransformDirection(Vector3.right);
        RaycastHit2D hit2 = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), new Vector2(fwt.x, fwt.y), 1.0f, layerMask);
        Debug.DrawRay(new Vector2(transform.position.x, transform.position.y), new Vector2(fwt.x, fwt.y), Color.cyan);

        if (moving)
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }

        if (patrol)
        {
            Debug.Log("Patrolling normally");
            speed = 2.0f;

            if (hit2.collider != null)
            {
                if (hit2.collider.gameObject.tag == "Wall")
                {
                    if (!clockwise)
                    {
                        transform.Rotate(0, 0, 90);
                    }
                    else
                    {
                        transform.Rotate(0, 0, -90);
                    }
                }
            }
        }

        if (pursuingPlayer)
        {
            Debug.Log("Pursuing Player");
            speed = 3.51f;
            rid.transform.eulerAngles = new Vector3(0, 0, Mathf.Atan2(playerLastPos.y - transform.position.y, playerLastPos.x - transform.position.x) * Mathf.Rad2Deg);

            if (hit.collider != null && hit.collider.gameObject.tag == "Player")
            {
                playerLastPos = player.transform.position;
            }
        }

        if (goingToLastLoc)
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

        if (distanceToPlayer < 9f)
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
}


