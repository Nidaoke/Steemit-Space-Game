﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaptorSpawner : SpawnerGeneric {

    public GameObject raptor;
    public Transform raptorSpawn, raptorTarget;
    public int countRap;
    public float delay;
    public bool flipX;

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
            StartCoroutine(Spawn());
        }
    }

    IEnumerator Spawn()
    {
        for(int i = 0; i < countRap; i++)
        {
            GameObject rap1 = Instantiate(raptor, raptorSpawn.position, Quaternion.identity) as GameObject;
            rap1.GetComponent<Enemy>().runTargets[0] = raptorTarget;
            rap1.transform.rotation = rap1.GetComponent<Enemy>().runTargets[0].rotation;
            if (flipX)
                rap1.transform.localScale = new Vector3(rap1.transform.localScale.x * -1, rap1.transform.localScale.y, rap1.transform.localScale.z);

            if (spawnSpecial)
            {
                rap1.GetComponent<SpriteRenderer>().color = color;
                rap1.GetComponent<Enemy>().spawnBonus = true;
                spawnSpecial = false;
            }

            yield return new WaitForSeconds(delay);
        }
    }
}
