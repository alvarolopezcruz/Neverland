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

    private Material matWhite;
    private Material matDefault;
    private UnityEngine.Object explosionRef;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Transform>();
        currentHealth = maxHealth;
        matWhite = Resources.Load("WhiteFlash", typeof(Material)) as Material;
        matDefault = gameObject.GetComponent<SpriteRenderer>().material;
        explosionRef = Resources.Load("EnemyExplosion");
        
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
        if (col.CompareTag("Projectile"))
        {
            takeDamage(20);
        }
    }

    void takeDamage(int damage)
    {
        currentHealth -= damage;
        gameObject.GetComponent<SpriteRenderer>().material = matWhite;
        if (currentHealth <= 0)
        {
            killSelf();
        }
        else
        {
            Invoke("resetMaterial", .1f);
        }
    }

    private void killSelf()
    {
        GameObject explosion = (GameObject)Instantiate(explosionRef);
        explosion.transform.position = new Vector2(transform.position.x, transform.position.y + .3f);
        Destroy(gameObject);
    }

    void resetMaterial()
    {
        gameObject.GetComponent<SpriteRenderer>().material = matDefault;
    }


}
