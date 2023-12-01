using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public bool moving = true;
    public float speed = 5.0f;

    void Start()
    {
        // Inicialização do jogador
    }

    void Update()
    {
        if (moving)
        {
            Movement();
        }

        MovementCheck(); // Verifica o movimento (adicionado em EP 2, falar sobre as mudanças)
    }

    public void SetMoving(bool val)
    {
        moving = val;
    }

    void Movement()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.up * speed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.down * speed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime, Space.World);
        }
    }

    void MovementCheck()
    {
        if (Input.GetKey(KeyCode.D) != true && Input.GetKey(KeyCode.A) != true && Input.GetKey(KeyCode.S) != true && Input.GetKey(KeyCode.W) != true)
        {
            moving = false;
        }
        else
        {
            moving = true;
        }
    }
}