using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantManagment : MonoBehaviour
{

    private Collider2D area;
    public float radius;

    Vector2 spawnPoint;
    GameObject player;
 


    // Start is called before the first frame update
    void Start()
    {
        spawnPoint =new Vector2 (gameObject.GetComponent<Transform>().position.x, gameObject.GetComponent<Transform>().position.y);
        player = GameObject.FindGameObjectWithTag("Player");
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
            if (area.CompareTag("Player"))
            {
                player.GetComponent<PlantDamageManagment>().takePlantDamage(10);
            }
        }
    }
}
