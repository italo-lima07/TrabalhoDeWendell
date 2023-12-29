using UnityEngine;

public class CanvaDesativador : MonoBehaviour
{
    private float delay = 10.45f;

    private void Start()
    {
        Invoke("DisableCanvas", delay);
    }

    private void DisableCanvas()
    {
        gameObject.SetActive(false);
    }
}