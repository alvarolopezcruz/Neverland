using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ExecutionerManagment : MonoBehaviour
{

    Transform player;
    private Rigidbody2D rb;
    private Vector2 movement;
    public float movespeed = 3;
    public float maxHealth;
    private float currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Transform>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
    }
     
    // Update is called once per frame
    void Update()
    {
        fixSprite(player);

        Vector3 direction = player.position - transform.position;
        direction.Normalize();
        movement = direction;
        Debug.Log(direction);
    }

    void FixedUpdate()
    {
        moveCharacter(movement);
    }

    void moveCharacter(Vector2 direction)
    {
        rb.MovePosition((Vector2)transform.position + (direction * movespeed * Time.deltaTime));
    }

    void fixSprite(Transform character)
    {
        if (gameObject.GetComponent<Transform>().position.x >= player.position.x)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }

        else
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name == "Projectile(Clone)")
        {
            takeDamage(20);
        } 
    }

    void takeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log(currentHealth);
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

}
