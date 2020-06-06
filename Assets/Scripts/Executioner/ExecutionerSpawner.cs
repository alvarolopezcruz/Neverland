using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    public GameObject[] enemies;
    int randenemy;
    float randX;
    float randY;
    public Transform spawnPoint;
    public float startTimeBtwSpawns;
    private float timeBtwSpawns;

    // Start is called before the first frame update
    void Start()
    {
        timeBtwSpawns = startTimeBtwSpawns;
    }

    // Update is called once per frame
    void Update()
    {
        randenemy = Random.Range(0, enemies.Length);
        if (timeBtwSpawns <= 0)
        {
            Instantiate(enemies[randenemy], spawnPoint.transform.position, Quaternion.identity);
            timeBtwSpawns = startTimeBtwSpawns;
        }
        else
        {
            timeBtwSpawns -= Time.deltaTime;
        }

    }
}
