using UnityEngine;

public class Door : MonoBehaviour
{
    public float openAngle = 90f; // Ângulo de abertura da porta
    public float rotationSpeed = 100f; // Velocidade de rotação da porta

    private bool isOpen = false; // Estado da porta (aberta ou fechada)
    private Quaternion initialRotation; // Rotação inicial da porta

    private void Start()
    {
        initialRotation = transform.rotation;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isOpen)
        {
            isOpen = true;
            OpenDoor();
        }
    }

    private void OpenDoor()
    {
        Vector3 targetRotation = transform.eulerAngles + new Vector3(0f, openAngle, 0f);
        LeanTween.rotate(gameObject, targetRotation, rotationSpeed / 90f)
            .setEase(LeanTweenType.easeInOutQuad);
    }
}