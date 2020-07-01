using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    public GameObject[] enemies;
    Vector2 area;


    int randomEnemie;
    public float startTimeBtwSpawns;
    private float timeBtwSpawns;


    // Start is called before the first frame update
    void Start()
    {
        area = gameObject.GetComponent<BoxCollider2D>().size;
        timeBtwSpawns = startTimeBtwSpawns;
    }

    // Update is called once per frame
    void Update()
    {
        Spawner();
    }

    void Spawner()
    {
        randomEnemie = Random.Range(0, enemies.Length);
        if (timeBtwSpawns <= 0)
        {
            Instantiate(enemies[0], randomPointInMap(), Quaternion.identity);
            timeBtwSpawns = startTimeBtwSpawns;
        }
        else
        {
            timeBtwSpawns -= Time.deltaTime;
        }
    }

    Vector3 randomPointInMap()
    {
        return new Vector3(
            Random.Range(-area.x/2, area.x/2),
            Random.Range(-area.y/2, area.y/2),
            0
        );
    }
}
