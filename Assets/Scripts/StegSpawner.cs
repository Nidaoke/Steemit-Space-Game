using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StegSpawner : SpawnerGeneric {

    public GameObject steg;
    public bool flipX;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Respawn")
        {
            GameObject pter1 = Instantiate(steg, transform.position, Quaternion.identity) as GameObject;

            if (flipX)
                pter1.transform.localScale = new Vector3(pter1.transform.localScale.x * -1, pter1.transform.localScale.y, pter1.transform.localScale.z);

            if (spawnSpecial)
            {
                pter1.GetComponent<SpriteRenderer>().color = color;
                pter1.GetComponent<Enemy>().spawnBonus = true;
                spawnSpecial = false;
            }

        }
    }
}
