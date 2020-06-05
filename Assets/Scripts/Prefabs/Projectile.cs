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
            rb.AddForce(new Vector2(-1f, 0f), ForceMode2D.Impulse);
        }
        else
        {
            rb.AddForce(new Vector2(1f, 0f), ForceMode2D.Impulse);
        }
        
    }


    void DestroyProjectile()
    {
        Destroy(gameObject);
    }

     
}
