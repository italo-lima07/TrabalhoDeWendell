using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeaponController : MonoBehaviour
{
    public GameObject oneHandSpawn, twoHandSpawn, bullet, blood;
    GameObject curWeapon;
    public bool gun = false;
    float timer = 0.1f, timerReset = 0.1f;
    SpriteContainer sc;
    float weaponChange = 0.5f;
    bool changingWeapon = false;
    bool oneHanded = false;
    EnemyAI eai;
    GameObject player;
    SpriteRenderer sr;

    void Start()
    {
        eai = this.GetComponent<EnemyAI>();
        player = GameObject.FindGameObjectWithTag("Player");
        sr = this.GetComponent<SpriteRenderer>();
        sc = GameObject.FindGameObjectWithTag("GameController").GetComponent<SpriteContainer>();
    }

    void Update()
    {
        if (gun == true)
        {
            eai.hasGun = true;
        }
        else
        {
            eai.hasGun = false;
        }

        if (timer > 0)
        {
            this.transform.position += this.transform.TransformDirection(Vector3.up);
            timer -= Time.deltaTime;
        }

        if (eai.hasGun == false && gun == false && eai.pursuingPlayer == true && timer <= 0 && Vector3.Distance(this.transform.position, player.transform.position) <= 1.6f)
        {
            Attack();
        }
        else if (eai.hasGun == true && eai.pursuingPlayer == true && timer <= 0 && Vector3.Distance(this.transform.position, player.transform.position) <= 5.8f)
        {
            Attack();
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
    
    public void setWeapon(GameObject cur, string name, float fireRate, bool gun, bool oneHanded)
    {
        changingWeapon = true;
        curWeapon = cur;
        sr.sprite = sc.getEnemySprite(name);
        this.gun = gun;
        timerReset = fireRate;
        timer = timerReset;
        this.oneHanded = oneHanded;
    }

    public void attack()
    {
        // pa.attack();
        if (gun == true)
        {
            // pa.attack();
            Bullet bl = bullet.GetComponent<Bullet>();
            Vector3 dir = Vector2.right;
            dir.x = Vector2.right.x;
            dir.y = Vector2.right.y;
            dir.z = 0;
            bl.setVals(dir, "Enemy");
            if (oneHanded == true)
            {
                Instantiate(bullet, oneHandSpawn.transform.position, this.transform.rotation);
            }
            else
            {
                Instantiate(bullet, oneHandSpawn.transform.position, this.transform.rotation);
            }

            timer = timerReset;
        }
        else
        {
            // Ataque corpo a corpo
            int layerMask = 1 << 8;
            RaycastHit2D ray = Physics2D.Raycast(new Vector2(this.transform.position.x, this.transform.position.y),
                new Vector2(transform.right.x, transform.right.y), 1.5f, layerMask);
            Debug.DrawRay(new Vector2(this.transform.position.x, this.transform.position.y),
                new Vector2(transform.right.x, transform.right.y), Color.green);
            Debug.Log("Attempting melee attack");

            if (curWeapon == null && ray.collider != null && ray.collider.gameObject.tag == "Player")
            {
                Debug.Log("Punching player");
                //EnemyAttacked ea = ray.collider.gameObject.GetComponent<EnemyAttacked>();
                // ea.knockDownEnemy();
            }
            else if (ray.collider != null && ray.collider.gameObject.tag == "Player")
            {
                Debug.Log("Melee attacking player");
                // EnemyAttacked ea = ray.collider.gameObject.GetComponent<EnemyAttacked>();
                // ea.killMelee();

            }

            Instantiate(blood, ray.collider.gameObject.transform.position, ray.collider.gameObject.transform.rotation);
            timer = timerReset;
        }
    }
    
    public GameObject getCurWeapon()
    {
        return curWeapon;
    }

    public void dropWeapon()
    {
        if (curWeapon != null)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
            curWeapon.AddComponent<ThrowWeapon>();
            Vector3 dir = new Vector3(mousePos.x - this.transform.position.x, mousePos.y - this.transform.position.y, 0);
            curWeapon.GetComponent<Rigidbody2D>().isKinematic = false;
            curWeapon.GetComponent<ThrowWeapon>().setDirection(dir);
            curWeapon.transform.position = oneHandSpawn.transform.position;
            curWeapon.transform.eulerAngles = this.transform.eulerAngles;
            curWeapon.SetActive(true);
            setWeapon(null, "", 0.5f, false, false);
            // pa.resetSprites();
        }
    }
}
    


