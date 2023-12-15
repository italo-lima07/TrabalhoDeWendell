using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAttack : MonoBehaviour
{
    private GameObject bullet, curWeapon;
    private bool gun = false;
    private float timer = 0.1f, timerReset = 0.1f;
    private PlayerAnimate pa;
    private SpriteContainer sc;

    private float weaponChange = 0.5f;
    private bool changingWeapon = false;
    
    
    // Start is called before the first frame update
    void Start()
    {
        pa = this.GetComponent<PlayerAnimate>();
        sc = GameObject.FindGameObjectsWithTag("GameController").GetComponent<SpriteContainer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            attack();
        }

        if (Input.GetMouseButtonDown(0))
        {
            pa.resetCounter();
        }

        if (Input.GetMouseButtonUp(0))
        {
            pa.resetCounter();
        }

        if (Input.GetMouseButtonDown(1) && changingWeapon == false)
        {
            dropWeapon();
        }

        if (changingWeapon == true)
        {
            weaponChange -= Time.deltaTime;
            if (weaponChange <= 0)
            {
                changingWeapon = false;
            }
        }
    }


    public void setWeapon(GameObject cur, string name, float fireRate, bool gun)
    {
        changingWeapon = true;
        curWeapon = cur;
        pa.setNewTorso(sc.getWeaponWalk(name), sc.getWeapon(name));
        this.gun = gun;
        timerReset = fireRate;
        timer = timerReset;

    }

    public void attack()
    {
        pa.attack();
    }
}
