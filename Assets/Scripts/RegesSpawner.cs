using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegesSpawner : SpawnerGeneric {

    public GameObject rex;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Respawn")
        {
            if (spawnSpecial)
            {
                GameObject spec = Instantiate(rex, transform.position, Quaternion.identity) as GameObject;
                spec.GetComponent<SpriteRenderer>().color = color;
                spec.GetComponent<Enemy>().spawnBonus = true;
                spawnSpecial = false;
            }
            else
                Instantiate(rex, transform.position, Quaternion.identity);
        }
    }
}
