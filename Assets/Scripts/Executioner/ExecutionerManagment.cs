using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ExecutionerManagment : MonoBehaviour
{

    Transform player;

    [HideInInspector]
    public float movespeed;
    public float movespeedValue;
    public float maxHealth;
    private float currentHealth;
    private float expEraseTimer = 1; //Time before erasing the particle system
    public bool landed = false;

    private Material matWhite;
    private Material matDefault;
    private UnityEngine.Object explosionRef;
    private SpriteRenderer shadowRenderer;
    private Transform shadowPosition;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        currentHealth = maxHealth;
        matWhite = Resources.Load("WhiteFlash", typeof(Material)) as Material;
        matDefault = gameObject.GetComponent<SpriteRenderer>().material;
        explosionRef = Resources.Load("EnemyExplosion");
        shadowRenderer = transform.Find("ExecutionerShadow").GetComponent<SpriteRenderer>();
        shadowRenderer.enabled = false;
        shadowPosition = transform.Find("ExecutionerShadow");
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
            shadowPosition.position = new Vector3(gameObject.transform.position.x + 0.062f, gameObject.transform.position.y - 0.378f, gameObject.transform.position.z + 1f);
        }

        else
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
            shadowPosition.position = new Vector3(gameObject.transform.position.x - 0.062f, gameObject.transform.position.y - 0.378f, gameObject.transform.position.z + 1f);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Projectile") && landed) //Hit by a projectile
        {
            takeDamage(20);
        }
        if (col.CompareTag("endFallPoint")) //Managment when executioner reaches the ground
        {
            if (!landed)
            {
                shadowRenderer.enabled = true;
                movespeed = movespeedValue;
                gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
                gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                Destroy(col.gameObject);
                landed = true;
            }
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
        Destroy(explosion, expEraseTimer);
    }

    void resetMaterial()
    {
        gameObject.GetComponent<SpriteRenderer>().material = matDefault;
    }
}
