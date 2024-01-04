using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckEnemy : MonoBehaviour
{
    void Update()
    {
        GameObject[] enemies1 = GameObject.FindGameObjectsWithTag("inimigo");
        GameObject[] enemies2 = GameObject.FindGameObjectsWithTag("inimigo2");

        if (enemies1.Length == 0 && enemies2.Length == 0)
        {
            Invoke("LoadNewScene", 5f); 
        }
    }

    void LoadNewScene()
    {
        SceneManager.LoadScene("CutsceneFInal");
    }
}