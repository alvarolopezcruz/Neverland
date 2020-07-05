using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemySpawner : MonoBehaviour
{

    private Vector3 plantCirceEdgeTempScale;
    private Vector3 plantCircleInsideTempScale;
    private Vector3 tempScale;
    Vector2 area;
    public GameObject[] enemies;
    public GameObject endFallPoint;
    private GameObject instantiation;

    int randomEnemie;
    public float startTimeBtwSpawns;
    private float timeBtwSpawns;
    public float fallSpeed;
    public float heigth;
    private bool scalingPlant = false;
    public float plantScaleRate;
    private float plantSize;
    public float minPlantSize;
    public float maxPlantSize;


    // Start is called before the first frame update
    void Start()
    {
        area = gameObject.GetComponent<BoxCollider2D>().size;
        timeBtwSpawns = startTimeBtwSpawns;
    }

    // Update is called once per frame
    void Update()
    {
        spawnRandomEnemy();
        if (scalingPlant)
        {
            plantScaling();
        }
    }

    void spawnRandomEnemy()
    {
        Vector3 endPoint = randomPointInMap();
        Vector3 spawnPoint = endPoint;
        spawnPoint.y = heigth;
        
        randomEnemie = Random.Range(0, enemies.Length);
        if (timeBtwSpawns <= 0)
        {
            switch (randomEnemie)
            {
                case 0: //Enemy is an executioner
                    spawnExecutioner(spawnPoint, endPoint);
                    break;
                case 1: //Enemy is a plant
                    spawnPlant(endPoint);
                    setPlantScaleToZero();
                    plantSize = Random.Range(minPlantSize, maxPlantSize);
                    scalingPlant = true;
                    break;
            }
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

    void spawnExecutioner(Vector3 spawnPoint, Vector3 endPoint)
    {
        instantiation = Instantiate(enemies[0], spawnPoint, Quaternion.identity);
        putEndPoint(endPoint);
        enemyFalling(instantiation, endPoint);
    }

    void spawnPlant(Vector3 spawnPoint)
    {
        instantiation = Instantiate(enemies[1], spawnPoint, Quaternion.identity);
        
        scalingPlant = true;
    }

    void plantScaling()
    {
        if (instantiation.transform.localScale.x < plantSize && instantiation.transform.localScale.y < plantSize)
        {
            tempScale = instantiation.transform.localScale;
            plantCirceEdgeTempScale = instantiation.transform.GetChild(0).gameObject.GetComponent<ParticleSystem>().transform.localScale;
            plantCircleInsideTempScale = instantiation.transform.GetChild(1).gameObject.GetComponent<ParticleSystem>().transform.localScale;
            plantCircleInsideTempScale.x += plantScaleRate; // Scale plant and its children
            plantCircleInsideTempScale.y += plantScaleRate;
            plantCirceEdgeTempScale.x += plantScaleRate;
            plantCirceEdgeTempScale.y += plantScaleRate;
            tempScale.x += plantScaleRate;
            tempScale.y += plantScaleRate;
            instantiation.transform.localScale = tempScale;
            instantiation.transform.GetChild(0).gameObject.GetComponent<ParticleSystem>().transform.localScale = plantCirceEdgeTempScale;
            instantiation.transform.GetChild(1).gameObject.GetComponent<ParticleSystem>().transform.localScale = plantCircleInsideTempScale;
            instantiation.GetComponent<PlantManagment>().radius = tempScale.x; //ScaleRadius
        }
        else
        {
            scalingPlant = false;
        }
    }

    void setPlantScaleToZero()
    {
        instantiation.transform.localScale = Vector3.zero;
        instantiation.transform.GetChild(0).gameObject.GetComponent<ParticleSystem>().transform.localScale = Vector3.zero;
        instantiation.transform.GetChild(1).gameObject.GetComponent<ParticleSystem>().transform.localScale = Vector3.zero;
    }
}
