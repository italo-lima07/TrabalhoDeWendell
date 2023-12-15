using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class legDir : MonoBehaviour
{
    Vector3 rot;

    // Start is called before the first frame update
    void Start()
    {
        rot = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            if (Input.GetKey(KeyCode.A))
            {
                rot = new Vector3(0, 0, 135);
            }
            else if (Input.GetKey(KeyCode.D))
            {
                rot = new Vector3(0, 0, 45);
            }
            else
            {
                rot = new Vector3(0, 0, 90);
            }
            transform.eulerAngles = rot;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            if (Input.GetKey(KeyCode.A))
            {
                rot = new Vector3(0, 0, 225);
            }
            else if (Input.GetKey(KeyCode.D))
            {
                rot = new Vector3(0, 0, 315);
            }
            else
            {
                rot = new Vector3(0, 0, 270);
            }
            transform.eulerAngles = rot;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            rot = new Vector3(0, 0, 180);
            transform.eulerAngles = rot;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            rot = new Vector3(0, 0, 0);
            transform.eulerAngles = rot;
        }
    }
}