using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemySpawner : MonoBehaviour
{

    public GameObject[] enemies;
    Vector2 area;
    public GameObject endFallPoint;
    private GameObject instantiation;

    int randomEnemie;
    public float startTimeBtwSpawns;
    private float timeBtwSpawns;
    public float fallSpeed;
    public float heigth;


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
        Vector3 endPoint = randomPointInMap();
        Vector3 spawnPoint = endPoint;
        spawnPoint.y = heigth;
        
        randomEnemie = Random.Range(0, enemies.Length);
        if (timeBtwSpawns <= 0)
        {
            instantiation = Instantiate(enemies[0], spawnPoint, Quaternion.identity);
            putEndPoint(endPoint);
            enemyFalling(instantiation, endPoint);
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

    void enemyFalling(GameObject enemy, Vector3 endPoint)
    {
        Rigidbody2D rb = enemy.GetComponent<Rigidbody2D>();
        if (enemy.transform.position.y != endPoint.y)
        {
            rb.gravityScale = 1;
            enemy.GetComponent<ExecutionerManagment>().movespeed = 0;
        }
    }

    void putEndPoint(Vector3 endPoint)
    {
        Instantiate(endFallPoint, endPoint, Quaternion.identity);
    }

}
