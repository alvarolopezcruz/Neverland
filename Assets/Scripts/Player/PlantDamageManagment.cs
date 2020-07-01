using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantDamageManagment : MonoBehaviour
{

    GameObject player;
    public float immuneTime;
    private bool immuneToPlant;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void takePlantDamage(int damage)
    {
        if (!immuneToPlant)
        {
            player.GetComponent<PlayerManagment>().takeDamage(10);
            StartCoroutine(JustDamaged());
        }
    }

    private IEnumerator JustDamaged()
    {
        immuneToPlant = true;
        yield return new WaitForSeconds(immuneTime);
        immuneToPlant = false;
    }
}
