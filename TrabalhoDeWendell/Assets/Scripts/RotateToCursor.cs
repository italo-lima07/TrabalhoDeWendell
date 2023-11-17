using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class RotateToCursor : MonoBehaviour
{
    private Vector3 mousePos;
    private Vector3 direction;
    private Camera cam;
    private Rigidbody2D rig;
    
    // Start is called before the first frame update
    void Start()
    {
        rig = this.GetComponent<Rigidbody2D>();
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        rotateToCamera();
    }

    void rotateToCamera()
    {
        mousePos = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y,
            Input.mousePosition.z - cam.transform.position.z));
        rig.transform.eulerAngles = new Vector3(0, 0,
            math.atan2((mousePos.y - transform.position.y), (mousePos.x - transform.position.x) * Mathf.Rad2Deg));
    }
}
