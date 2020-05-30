using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5;
    public Rigidbody2D rb;

    Vector2 movement;
    // Start is called before the first frame update
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
        }

        if (Input.GetKey("right"))
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }
    }

    void FixedUpdate()
    {
        //Movement management goes here
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    bool isPlayerMoving()
    {
        if(Input.GetKey("left") || Input.GetKey("right") || Input.GetKey("up") || Input.GetKey("down"))
        {
            return true;
        }

        else
        {
            return false;
        }
    }

}
