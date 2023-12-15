using UnityEngine;
using System.Collections;

public class PlayerAnimate : MonoBehaviour
{
    Sprite[] walking, attacking, legsSpr;
    int counter = 0, legCount = 0;
    PlayerMovement pm;
    float timer = 0.051f, legTimer = 0.051f;
    public SpriteRenderer torso, legs;
    SpriteContainer sc;

    void Start()
    {
        pm = this.GetComponent<PlayerMovement>();
        sc = GameObject.FindGameObjectWithTag("GameController").GetComponent<SpriteContainer>();
        walking = sc.getPlayerUnarmedwalk();
        legsSpr = sc.getPlayerLegs();
    }

    void Update()
    {
        animateLegs();
        animateTorso();
    }

    void animateTorso()
    {
        if (pm.moving == true)
        {
            torso.sprite = walking[counter];
            timer += Time.deltaTime;
            if (timer >= 0.1f)
            {
                counter = (counter + 1) % walking.Length;
                timer = 0f;
            }
        }
    }

    void animateLegs()
    {
        if (pm.moving == true)
        {
            legs.sprite = legsSpr[legCount];
            legTimer += Time.deltaTime;
            if (legTimer >= 0.05f)
            {
                legCount = (legCount + 1) % legsSpr.Length;
                legTimer = 0f;
            }
        }
    }
}

