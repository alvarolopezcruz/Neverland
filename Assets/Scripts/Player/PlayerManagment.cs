using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManagment : MonoBehaviour
{
    public float moveSpeed = 5;
    private float moveX;
    private float moveY;
    public string direction;
    public float currentHealth;
    public float maxHealth = 100;

    private Vector2 moveDirection;
    public HealthBar healthBar;
    public GameObject projectile;
    public Transform Projectile_Start;
    public Rigidbody2D rb;
    public GameObject instantiation;
    private Material matWhite;
    private Material matDefault;
    private UnityEngine.Object DustEffect;
    public ParticleSystem dustEffect;

    public float interval;
    private float nextShot;


    // Start is called before the first frame updateW
    void Start()
    {
        //projectile.GetComponent<SpriteRenderer>().flipX = false; //Para evitar problemas al momento de la instanciación
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        matWhite = Resources.Load("WhiteFlash", typeof(Material)) as Material;
        matDefault = gameObject.GetComponent<SpriteRenderer>().material;
        nextShot = 0.0f;
        DustEffect = Resources.Load("DustEffect");
    }

    // Update is called once per frame
    void Update()
    {
        processInputs();

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
                instantiation.GetComponent<FireballManagement>().direction = direction;
            }
        }

        if (!(gameObject.GetComponent<Rigidbody2D>().velocity.x != 0  || gameObject.GetComponent<Rigidbody2D>().velocity.y != 0))
        {
            createDust();
        }

    }



    void FixedUpdate()
    {
        //Movement management goes here
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Enemy_Executioner") && col.gameObject.GetComponent<ExecutionerManagment>().landed == true)
        {
            takeDamage(20);
        }
        if (col.CompareTag("Enemy_Plant"))
        {
            //Make the plant squash and disable it for a small period of time
        }

    }

    public void takeDamage(int damage)
    {
        currentHealth -= damage;
        gameObject.GetComponent<SpriteRenderer>().material = matWhite;
        healthBar.setHealth(currentHealth);
        if (currentHealth <= 0)
        {
            //Matar al player
        }
        else
        {
            Invoke("resetMaterial", .2f);
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
        if (Input.GetKey("q"))
        {
            setProjectileStart("left");
            direction = "left";
            fixPlayerSprite("left");
            return true;
        }

        else if (Input.GetKey("e"))
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

    void resetMaterial()
    {
        gameObject.GetComponent<SpriteRenderer>().material = matDefault;
    }

    void createDust()
    {
        dustEffect.Play();
    }

    void processInputs()
    {
        moveX = Input.GetAxisRaw("Horizontal");
        moveY = Input.GetAxisRaw("Vertical");
        moveDirection = new Vector2(moveX, moveY).normalized;
    }

}