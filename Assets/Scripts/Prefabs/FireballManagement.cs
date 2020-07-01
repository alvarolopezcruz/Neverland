using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class FireballManagement : MonoBehaviour
{

    public float speed;
    public float lifeTime;
    private UnityEngine.Object explosionRef;
    [HideInInspector]
    public Rigidbody2D rb;
    [HideInInspector]
    public string direction;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestroyProjectile", lifeTime);
        rb = gameObject.GetComponent<Rigidbody2D>();
        calculateDirection(); //Adds inertia to projectiles
        fixProjectileSprite(); //Fixes projectile sprite direction and handles inerta in the opposite direction
        explosionRef = Resources.Load("ProjectileCollisionExplosion");
    }

    // Update is called once per frame
    void Update()
    {


    }

    void FixedUpdate()
    {
        moveProjectile();
    }


    void DestroyProjectile()
    {
        GameObject explosion = (GameObject)Instantiate(explosionRef);
        explosion.transform.position = new Vector2(transform.position.x, transform.position.y);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.CompareTag("Player") && !col.CompareTag("Background")) {
            DestroyProjectile();
        }
    }

    private void calculateDirection()
    {
        if (direction == "left")
        {
            if (Input.GetKey(KeyCode.DownArrow))
            {
                transform.eulerAngles = Vector3.forward * 15;
            }
            else
            if (Input.GetKey(KeyCode.UpArrow))
                transform.eulerAngles = Vector3.forward * -15;
        }
    }

    void fixProjectileSprite()
    {
        if (direction == "right" && !(Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.UpArrow)))
        {
            transform.eulerAngles = Vector3.forward * 180;
        }
        else
        {
            if (direction == "right" && Input.GetKey(KeyCode.DownArrow))
            {
                transform.eulerAngles = Vector3.forward * 165;
            }
            else
            {
                if (direction == "right" && Input.GetKey(KeyCode.UpArrow))
                {
                    transform.eulerAngles = Vector3.forward * 195;
                }
            }
        }
    }

    void moveProjectile()
    {
        if (direction == "left")
        {
            rb.velocity = transform.right * -speed;
        }
        else
        {
            rb.velocity = transform.right * -speed;
        }
    }

}
