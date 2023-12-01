using UnityEngine;
using System.Collections;

public class RotateToCursor : MonoBehaviour
{
    Vector3 mousePos;
    Camera cam;
    Rigidbody2D rid;

    void Start()
    {
        rid = this.GetComponent<Rigidbody2D>();
        cam = Camera.main;
    }

    void Update()
    {
        RotateToCamera();
    }

    void RotateToCamera()
    {
        mousePos = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z - cam.transform.position.z));
        float angle = Mathf.Atan2((mousePos.y - transform.position.y), (mousePos.x - transform.position.x)) * Mathf.Rad2Deg;
        rid.transform.eulerAngles = new Vector3(0, 0, angle);
    }
}