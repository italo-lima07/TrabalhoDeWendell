using UnityEngine;
using System.Collections;

public class GenericAnimate : MonoBehaviour
{
    public Sprite[] sprites;
    public bool loop, destroyOnFinish, whileNear, needsActivating;
    bool activated = false;
    SpriteRenderer sr;
    float timer = 0.051f;
    public float distance = 5.0f, timerReset = 0.051f;
    int counter = 0;
    GameObject player;

    void Start()
    {
        sr = this.GetComponent<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (whileNear == true)
        {
            animateWhileNear();
        }
        else if (needsActivating == true && activated == false)
        {
            if (Input.GetKeyDown(KeyCode.E) && Vector3.Distance(player.transform.position, this.transform.position) < 2.0f)
            {
                activated = true;
            }
        }
        else if (needsActivating == true && activated == true)
        {
            normalAnimate();
            if (Input.GetKeyDown(KeyCode.E) && Vector3.Distance(player.transform.position, this.transform.position) < 2.01f)
            {
                activated = false;
            }
        }
        else
        {
            normalAnimate();
        }
    }

    void animateWhileNear()
    {
        sr.sprite = sprites[counter];
        if (Vector3.Distance(player.transform.position, this.transform.position) < distance)
        {
            timer += Time.deltaTime;
            if (timer >= timerReset)
            {
                if (counter < sprites.Length - 1)
                {
                    counter++;
                }
                else
                {
                    if (loop == true)
                    {
                        counter = 0;
                    }
                    else if (destroyOnFinish == true)
                    {
                        Destroy(this.gameObject);
                    }
                }
                timer = timerReset;
            }
        }
    }

    void normalAnimate()
    {
        sr.sprite = sprites[counter];
        if (counter < sprites.Length - 1)
        {
            counter++;
        }
        else
        {
            if (loop == true)
            {
                counter = 0;
            }
            else if (destroyOnFinish == true)
            {
                Destroy(this.gameObject);
            }
        }

        timer = timerReset;
    }
}

