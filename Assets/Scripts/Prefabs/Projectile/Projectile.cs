using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
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
        fixProjectileSprite(); //Fixes projectile sprite direction
        explosionRef = Resources.Load("ProjectileCollisionExplosion");
    }

    // Update is called once per frame
    void Update()
    {

        
    }

    void FixedUpdate()
    {
        if (direction == "left")
        {
            rb.velocity = transform.right * -speed;
        }
        else
        {
            rb.velocity = transform.right * speed;
        }
    }


     void DestroyProjectile()
    {
        GameObject explosion = (GameObject)Instantiate(explosionRef);
        explosion.transform.position = new Vector2(transform.position.x, transform.position.y);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        DestroyProjectile();
    }

    private void calculateDirection()
    {
        if (Input.GetKey(KeyCode.DownArrow))
        {
            if (direction == "left")
            {
                transform.eulerAngles = Vector3.forward * 15;
            }
            else
            {
                transform.eulerAngles = Vector3.forward * -15;
            }
        }
        else
            if (Input.GetKey(KeyCode.UpArrow))
            {
               if (direction == "left")
               {
                   transform.eulerAngles = Vector3.forward * -15;
               }
               else
               {
                transform.eulerAngles = Vector3.forward * 15;
            }
            }
    }

    void fixProjectileSprite()
    {
        if (direction == "left")
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }
        else
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
    }

}
