using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerManagment : MonoBehaviour
{
    public float moveSpeed = 5;
    public Rigidbody2D rb;
    public string direction;

    Vector2 movement;

    public GameObject projectile;
    public Transform Projectile_Start;

    private float timeBtwShots;
    public float startTimeBtwShots;

    public GameObject instantiation;

    // Start is called before the first frame updateW
    void Start()
    {

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
            gameObject.GetComponent<Animator>().SetBool("attacking", true);


        }

        if (!isPlayerAttacking())
        {
            gameObject.GetComponent<Animator>().SetBool("attacking", false);
        }

        if (timeBtwShots <= 0)
        {
            if (isPlayerAttacking())
            {
                instantiation = Instantiate(projectile, Projectile_Start.position, Quaternion.identity); //Instanciamos el proyectil
                timeBtwShots = startTimeBtwShots;
                instantiation.GetComponent<Projectile>().direction = direction;
            }
        }
        else
        {
            timeBtwShots -= Time.deltaTime;
        }
    }



    void FixedUpdate()
    {
        //Movement management goes here
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
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
        if (Input.GetKey("f"))
        {
            return true;
        }

        else
        {
            return false;
        }
    }
}
