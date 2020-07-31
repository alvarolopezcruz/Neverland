using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantManagment : MonoBehaviour
{

    public float radius;
    public float maxHealth;
    private float currentHealth;
    private float expEraseTimer = 1; //Time before erasing the particle system

    private UnityEngine.Object explosionRef;
    private Collider2D area;
    Vector2 spawnPoint;
    GameObject player;
    private Material matWhite;
    private Material matDefault;



    // Start is called before the first frame update
    void Start()
    {
        spawnPoint =new Vector2 (gameObject.GetComponent<Transform>().position.x, gameObject.GetComponent<Transform>().position.y);
        player = GameObject.FindGameObjectWithTag("Player");
        currentHealth = maxHealth;
        matWhite = Resources.Load("WhiteFlash", typeof(Material)) as Material;
        matDefault = gameObject.GetComponent<SpriteRenderer>().material;
        explosionRef = Resources.Load("Plant Death");
    }

    // Update is called once per frame
    void Update()
    {
        checkArea();
    }



    void checkArea()
    {
        area = Physics2D.OverlapCircle(spawnPoint, radius,1,1);
        if (area != null)
        {
            if (area.CompareTag("Player") && radius != 0)
            {
                player.GetComponent<PlantDamageManagment>().takePlantDamage(10);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D col)
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

    void killSelf()
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
