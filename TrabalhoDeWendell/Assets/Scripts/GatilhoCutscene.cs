using UnityEngine;

public class GatilhoCutscene : MonoBehaviour
{
    public GameObject canvasCutscene;
    private bool cutsceneAtiva = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            cutsceneAtiva = true;
            canvasCutscene.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            cutsceneAtiva = false;
            canvasCutscene.SetActive(false);
            Time.timeScale = 1f;
        }
    }
}