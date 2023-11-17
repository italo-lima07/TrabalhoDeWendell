using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPLayer : MonoBehaviour
{
    private GameObject player;
    private bool followPlayer = true;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (followPlayer == true)
        {
            camFollowPlayer();
        }
    }

    public void setFollowPlayer(bool val)
    {
        followPlayer = val;
    }

    void camFollowPlayer()
    {
        Vector3 newPos = new Vector3(player.transform.position.x, player.transform.position.y,
            -10);
        this.transform.position = newPos;
    }
}
