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
        CanFollowPlayer();
    }

    void CanFollowPlayer()
    {
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);
    }

    public void SetFollowPlayer(bool val)
    {
        followPlayer = val;
    }

    

    void lookAhead()
    {
        Vector3 camPos = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y));
        camPos.z = -10;
        Vector3 dir = camPos - this.transform.position;

        if (player.GetComponent<SpriteRenderer>().isVisible == true)
        {
            transform.Translate(dir * 2 * Time.deltaTime);
        }
    }
}