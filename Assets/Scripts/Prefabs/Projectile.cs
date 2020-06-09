using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
  
    public float speed;
    public float lifeTime;
    [HideInInspector]
    public Rigidbody2D rb;
    [HideInInspector]
    public string direction;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestroyProjectile", lifeTime);
        rb = gameObject.GetComponent<Rigidbody2D>();
        calculateDirection();
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


}
