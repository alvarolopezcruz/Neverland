using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantDisabledManagment : MonoBehaviour
{

    private float scaleRate;
    public float auxRadius;
    private bool shrinkingPlantRadius = false;
    private bool scalingRadius = false;
    public float radiusShrinkRate;
    public float plantSteppedTime;

    private GameObject spawner;
    private Vector3 circleInside;
    private Vector3 circleEdge;

    // Start is called before the first frame update
    void Start()
    {
        spawner = GameObject.Find("EnemySpawner");
        scaleRate = spawner.GetComponent<EnemySpawner>().plantScaleRate;
        auxRadius = spawner.GetComponent<EnemySpawner>().plantSize;
    }

    // Update is called once per frame
    void Update()
    {
        if(shrinkingPlantRadius == true && scalingRadius == true)
        {
            shrinkingPlantRadius = false;
        }
        if(shrinkingPlantRadius == true)
        {
            shrinkPlantRadius();
        }
        if(scalingRadius == true)
        {
            scalePlantRadius();
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player") && !scalingRadius && !spawner.GetComponent<EnemySpawner>().scalingPlant)
        {
            gameObject.GetComponent<Animator>().SetBool("Stepped", true);
            StartCoroutine(plantStepped());
        }
    }

    private IEnumerator plantStepped()
    {
        shrinkingPlantRadius = true;
        yield return new WaitForSeconds(plantSteppedTime);
        gameObject.GetComponent<Animator>().SetBool("Stepped", false);
        scalingRadius = true;
    }

    void scalePlantRadius()
    {
        gameObject.GetComponent<Animator>().SetBool("Stepped", false);
        if (gameObject.GetComponent<PlantManagment>().radius < auxRadius)
        {
            circleEdge = gameObject.transform.GetChild(0).gameObject.GetComponent<ParticleSystem>().transform.localScale;
            circleInside = gameObject.transform.GetChild(1).gameObject.GetComponent<ParticleSystem>().transform.localScale;
            circleEdge.x += scaleRate;
            circleEdge.y += scaleRate;
            circleInside.x += scaleRate;
            circleInside.y += scaleRate;
            gameObject.transform.GetChild(0).gameObject.GetComponent<ParticleSystem>().transform.localScale = circleEdge;
            gameObject.transform.GetChild(1).gameObject.GetComponent<ParticleSystem>().transform.localScale = circleInside;
            gameObject.GetComponent<PlantManagment>().radius = circleEdge.x;
        }
        else
        {
            scalingRadius = false;
        }
    }

    void shrinkPlantRadius()
    {
        if (gameObject.GetComponent<PlantManagment>().radius > 0)
        {
            circleEdge = gameObject.transform.GetChild(0).gameObject.GetComponent<ParticleSystem>().transform.localScale;
            circleInside = gameObject.transform.GetChild(1).gameObject.GetComponent<ParticleSystem>().transform.localScale;
            circleEdge.x -= radiusShrinkRate;
            circleEdge.y -= radiusShrinkRate;
            circleInside.x -= radiusShrinkRate;
            circleInside.y -= radiusShrinkRate;
            gameObject.transform.GetChild(0).gameObject.GetComponent<ParticleSystem>().transform.localScale = circleEdge;
            gameObject.transform.GetChild(1).gameObject.GetComponent<ParticleSystem>().transform.localScale = circleInside;
            gameObject.GetComponent<PlantManagment>().radius = circleEdge.x;
        }
        else
        {
            shrinkingPlantRadius = false;
            circleEdge.x = 0;
            circleEdge.y = 0;
            circleInside.x = 0;
            circleInside.y = 0;
            gameObject.transform.GetChild(0).gameObject.GetComponent<ParticleSystem>().transform.localScale = circleEdge;
            gameObject.transform.GetChild(1).gameObject.GetComponent<ParticleSystem>().transform.localScale = circleInside;
            gameObject.GetComponent<PlantManagment>().radius = circleEdge.x;
        }
    }
}
