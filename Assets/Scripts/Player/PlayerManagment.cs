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

        if (isPlayerAttacking())
        {
            if (Time.time >= nextShot)
            {
                gameObject.GetComponent<Animator>().SetTrigger("attack");

                nextShot = Time.time + interval;

                instantiation = Instantiate(projectile, Projectile_Start.position, Quaternion.identity); 
                instantiation.GetComponent<Projectile>().direction = direction;
            }
        }
        else
        {
            if (gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Move")) { //Waits for the attack animation to finish in case its on

                if (Input.GetKey("left"))
                {
                    setProjectileStart("left");
                    projectile.GetComponent<SpriteRenderer>().flipX = true;
                    direction = "left";
                }

                if (Input.GetKey("right"))
                {
                    setProjectileStart("right");
                    projectile.GetComponent<SpriteRenderer>().flipX = false;
                    direction = "right";
                }
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
        if (currentHealth <= 0)
        {
            //Matar al player
        }
    }

    bool isPlayerMoving() //Checks if player is moving
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

    bool isPlayerAttacking() //Checks if player is attacking and fixes projectile direction
    {
        if (Input.GetKeyDown("q"))
        {
            setProjectileStart("left");
            direction = "left";
            fixPlayerSprite("left");
            return true;
        }

        else if (Input.GetKeyDown("e"))
        {
            setProjectileStart("right");
            direction = "right";
            fixPlayerSprite("right");
            return true;
        }

        return false;
       
    }

    void setProjectileStart(string direction) //Sets projectile start point depending of the direction
    {
        if (direction == "left")
        {
            Projectile_Start.transform.position = new Vector3(gameObject.GetComponent<Transform>().position.x - 0.553f, gameObject.GetComponent<Transform>().position.y, gameObject.GetComponent<Transform>().position.z);
        }
        else
            Projectile_Start.transform.position = new Vector3(gameObject.GetComponent<Transform>().position.x + 0.553f, gameObject.GetComponent<Transform>().position.y, gameObject.GetComponent<Transform>().position.z);
    }


    void fixPlayerSprite(string direction) //Fixes player sprite depending on the direction
    {
        if(direction == "left")
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }
        else
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
    }

}