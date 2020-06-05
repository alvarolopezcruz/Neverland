using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
  
    public float speed;
    public float lifeTime;
    
    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestroyProjectile", lifeTime);
    }

    // Update is called once per frame
    void Update()
    {

        
    }

   /* void FixedUpdate()
    {
        if (projectileDirection() == "left")
        {
            rb.MovePosition(-rb.position * speed * Time.fixedDeltaTime);
        }
        else
        {
            rb.MovePosition(rb.position * speed * Time.fixedDeltaTime);
        }
        
    }*/


    void DestroyProjectile()
    {
        Destroy(gameObject);
    }

     /*string projectileDirection()
    {
        if (player.position.x >= gameObject.GetComponent<Transform>().position.x)
        {
            return "left";
        }
        else
        {
            return "right";
        }
    }*/
}
