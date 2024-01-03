using UnityEngine;

public class VerificadorAndar : MonoBehaviour
{
    public string enemyTag = "Enemy";
    public GameObject replacementPrefab;

    void Update()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);

        if (enemies.Length == 0)
        {
            Instantiate(replacementPrefab, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}