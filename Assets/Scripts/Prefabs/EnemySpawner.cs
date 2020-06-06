using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExecutionerSpawner : MonoBehaviour
{

    public GameObject[] enemies;
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
        if (timeBtwSpawns <= 0)
        {
            Instantiate(enemies[0], spawnPoint.transform.position, Quaternion.identity);
        }
        

    }
}
