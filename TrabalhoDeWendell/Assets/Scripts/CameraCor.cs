using UnityEngine;

public class CameraCor : MonoBehaviour
{
    public Color startColor;
    public Color endColor;
    public float gradientSpeed = 0.5f;

    private Camera cameraComponent;
    private float t = 0f;

    void Start()
    {
        cameraComponent = GetComponent<Camera>();
    }

    void Update()
    {
        t += gradientSpeed * Time.deltaTime;
        cameraComponent.backgroundColor = Color.Lerp(startColor, endColor, Mathf.PingPong(t, 1f));
    }
}