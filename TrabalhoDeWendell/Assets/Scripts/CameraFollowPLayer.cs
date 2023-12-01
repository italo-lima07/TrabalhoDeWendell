using UnityEngine;

public class CameraFollowPLayer : MonoBehaviour
{
    public GameObject player;
    private PlayerMovement pm;
    public bool followPlayer = true;
    Vector3 mousePos;
    Camera cam;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        pm = player.GetComponent<PlayerMovement>();
        cam = Camera.main;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            followPlayer = false;
            pm.SetMoving(false);
        }
        else
        {
            followPlayer = true;
        }

        if (followPlayer == true)
        {
            CanFollowPlayer();
        }
        else
        {
            LookAhead();
        }
    }

    public void SetFollowPlayer(bool val)
    {
        followPlayer = val;
    }

    void CanFollowPlayer()
    {
        Vector3 newPos = new Vector3(player.transform.position.x, player.transform.position.y, this.transform.position.z);
        this.transform.position = newPos;
    }

    void LookAhead()
    {
        Vector3 camPos = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y));
        camPos.z = -18;
        Vector3 dir = camPos - this.transform.position;

        if (player.GetComponent<SpriteRenderer>().isVisible == true)
        {
            transform.Translate(dir * 2 * Time.deltaTime);
        }
    }
}