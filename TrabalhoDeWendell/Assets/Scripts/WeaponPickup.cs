using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    public string name;

    public float fireRate;

    private WeaponAttack wa;

    public bool gun;
    // Start is called before the first frame update
    void Start()
    {
        wa = GameObject.FindGameObjectsWithTag("Player").GetComponent<WeaponAttack>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D coll)
    {
        Debug.Log("Collision");
        if (coll.GameObject.tag == "Player" && Input.GetMouseButtonDown(1))
        {
            Debug.Log("Player picked up:  " + name);
            if (wa.GetCur() != null)
            {
                wa.dropWeapon();
            }

            wa.SetWeapon(this.gameObject, name, fireRate, gun);
            this.GameObject.SetActive(false);
        }
    }
}
