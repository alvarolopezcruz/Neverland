using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ExecutionerManagment : MonoBehaviour
{

    Transform player;
    private Vector2 movement;
    public float movespeed = 3;
    public float maxHealth;
    private float currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Transform>();
        currentHealth = maxHealth;
    }
     
    // Update is called once per frame
    void Update()
    {
        fixSprite();

       
    }

    void FixedUpdate()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.position, movespeed * Time.deltaTime);
    }

    

    void fixSprite()
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
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

}
