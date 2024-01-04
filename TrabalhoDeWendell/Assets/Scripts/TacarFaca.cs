using UnityEngine;

public class TacarFaca : MonoBehaviour
{
    public GameObject knifePrefab; 
    public float throwForce = 500f; 
    public float knifeLifetime = 3f; 
    public float throwInterval = 0.5f; 
    public int knifeCount = 0;

    private PlayerAnimate playerAnimate; 
    private float timer; 

    private void Start()
    {
        playerAnimate = GetComponent<PlayerAnimate>(); 
    }

    private void Update()
    {
        timer += Time.deltaTime; 

        if (Input.GetMouseButton(1) && playerAnimate.holdingKnife && timer >= throwInterval && knifeCount > 0) 
        {
            ThrowKnife(); 
            timer = 0f; 
            knifeCount--; 
        }
    }

    private void ThrowKnife()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 throwDirection = (mousePosition - transform.position).normalized;

        
        float angle = Mathf.Atan2(throwDirection.y, throwDirection.x) * Mathf.Rad2Deg;

        
        GameObject knife = Instantiate(knifePrefab, transform.position, Quaternion.Euler(0f, 0f, angle));

        
        Rigidbody2D knifeRigidbody = knife.GetComponent<Rigidbody2D>();

        
        knifeRigidbody.AddForce(throwDirection * throwForce);

        
        knife.GetComponent<Collider2D>().isTrigger = true;

        
        KnifeCollisionDetection collisionDetection = knife.AddComponent<KnifeCollisionDetection>();
        collisionDetection.knifeOwner = gameObject;

        
        Destroy(knife, knifeLifetime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("KnifePickup")) 
        {
            knifeCount++; 
            Destroy(other.gameObject); 
        }
    }
}



public class KnifeCollisionDetection : MonoBehaviour
{
    public GameObject knifeOwner;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Cenario")) // Verifica se a faca colidiu com um objeto marcado como "Cenario"
        {
            Destroy(gameObject); // Destroi a faca
        }

        if (collision.gameObject.CompareTag("inimigo"))
        {
            Destroy(gameObject);
        }
    }
}