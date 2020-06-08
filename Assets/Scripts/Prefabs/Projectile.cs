using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
  
    public float speed;
    public float lifeTime;
    public string direction;
    public Rigidbody2D rb;
    
    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestroyProjectile", lifeTime);
        rb = gameObject.GetComponent<Rigidbody2D>();
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
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        DestroyProjectile();
    }


}
