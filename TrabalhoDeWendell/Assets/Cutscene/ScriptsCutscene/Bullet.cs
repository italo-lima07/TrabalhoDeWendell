using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed = 5f;

    void Start()
    {
        StartCoroutine(DestroyAfterTime());
    }

    void Update()
    {
        transform.Translate(Vector2.right * bulletSpeed * Time.deltaTime);
    }

    IEnumerator DestroyAfterTime()
    {
        yield return new WaitForSeconds(2f); // Altere o tempo conforme necessário
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Cenario") || other.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}