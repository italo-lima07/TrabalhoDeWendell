using UnityEngine;

public class Door : MonoBehaviour
{
    public Rigidbody2D doorRigidbody;
    public Vector2 pivotPoint = Vector2.zero;
    public float openAngle = 90f;

    private bool isOpen = false;
    private float closedRotation;

    private void Start()
    {
        closedRotation = doorRigidbody.rotation;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            OpenDoor();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            CloseDoor();
        }
    }

    private void OpenDoor()
    {
        isOpen = true;
        doorRigidbody.MoveRotation(Quaternion.Euler(0f, 0f, closedRotation + openAngle));
    }

    private void CloseDoor()
    {
        isOpen = false;
        doorRigidbody.MoveRotation(Quaternion.Euler(0f, 0f, closedRotation));
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position + (Vector3)pivotPoint, 0.1f);
    }
}