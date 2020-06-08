using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManagment : MonoBehaviour
{
    public float moveSpeed = 5;
    public string direction;
    public float currentHealth;
    public float maxHealth = 100;
    

    Vector2 movement;

    public HealthBar healthBar;
    public GameObject projectile;
    public Transform Projectile_Start;
    public Rigidbody2D rb;
    public GameObject instantiation;

    public float interval = 0.65f;
    private float nextShot = 0.0f;


    // Start is called before the first frame updateW
    void Start()
    {
        projectile.GetComponent<SpriteRenderer>().flipX = false; //Para evitar problemas al momento de la instanciación
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        //Input management goes here
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
 

        if (isPlayerMoving())
        {
            gameObject.GetComponent<Animator>().SetBool("moving", true);
        }

        if (!isPlayerMoving())
        {
            gameObject.GetComponent<Animator>().SetBool("moving", false);
        }

        if (Input.GetKey("left"))
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = true;

            Projectile_Start.transform.position = new Vector3(gameObject.GetComponent<Transform>().position.x - 1, gameObject.GetComponent<Transform>().position.y, gameObject.GetComponent<Transform>().position.z);
            projectile.GetComponent<SpriteRenderer>().flipX = true;
            direction = "left";
        }

        if (Input.GetKey("right"))
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = false;

            Projectile_Start.transform.position = new Vector3(gameObject.GetComponent<Transform>().position.x + 1, gameObject.GetComponent<Transform>().position.y, gameObject.GetComponent<Transform>().position.z);
            projectile.GetComponent<SpriteRenderer>().flipX = false;
            direction = "right";
        }

        if (isPlayerAttacking())
        {
            if (Time.time >= nextShot)
            {
                gameObject.GetComponent<Animator>().SetTrigger("attack");

                nextShot = Time.time + interval;

                instantiation = Instantiate(projectile, Projectile_Start.position, Quaternion.identity); //Instanciamos el proyectil
                instantiation.GetComponent<Projectile>().direction = direction;
            }
        }
    }



    void FixedUpdate()
    {
        //Movement management goes here
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        takeDamage(20);
        
    }

    void takeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.setHealth(currentHealth);
        if(currentHealth <= 0)
        {
            //Matar al player
        }
    }

    bool isPlayerMoving()
    {
        if (Input.GetKey("left") || Input.GetKey("right") || Input.GetKey("up") || Input.GetKey("down"))
        {
            return true;
        }

        else
        {
            return false;
        }
    }

    bool isPlayerAttacking()
    {
        if (Input.GetKeyDown("f"))
        {
            return true;
        }

        else
        {
            return false;
        }
    }


}
