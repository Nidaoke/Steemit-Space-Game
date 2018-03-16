using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PterSpawner : SpawnerGeneric {

    public GameObject pter;
    public Transform pterSpawn;
    public Transform[] pterTargets;
    public int countPter;
    public float delay;
    public bool flipX;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Respawn")
        {
            StartCoroutine(Spawn());
        }
    }

    IEnumerator Spawn()
    {
        for (int i = 0; i < countPter; i++)
        {
            GameObject pter1 = Instantiate(pter, pterSpawn.position, Quaternion.identity) as GameObject;
            pter1.GetComponent<Enemy>().runTargets = pterTargets;
            pter1.transform.rotation = pter1.GetComponent<Enemy>().runTargets[0].rotation;
            if (flipX)
                pter1.transform.localScale = new Vector3(pter1.transform.localScale.x * -1, pter1.transform.localScale.y, pter1.transform.localScale.z);

            if (spawnSpecial)
            {
                pter1.GetComponent<SpriteRenderer>().color = color;
                pter1.GetComponent<Enemy>().spawnBonus = true;
                spawnSpecial = false;
            }

            yield return new WaitForSeconds(delay);
        }
    }
}
