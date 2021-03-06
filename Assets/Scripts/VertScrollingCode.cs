﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VertScrollingCode : MonoBehaviour {

	public bool starting;
	public VertScrollingCode otherOne;

    public GameObject[] spawners;

    //public Transform[] stegSpawns;


	private float lessFloat;
	private float setFloat;

	// Use this for initialization
	void Start () {
		if (starting) {
			setFloat = otherOne.GetStartingY ();
			lessFloat = (transform.position.y - (otherOne.GetStartingY () - transform.position.y));
			otherOne.SetLessFloat (lessFloat);
			otherOne.SetSetFloat (setFloat);
		}
	}
	
	// Update is called once per frame
	void Update () {
		//transform.position += new Vector3 (0, -Manager.Instance.scrollSpeed, 0);

		if (transform.position.y <= lessFloat) {
			transform.position = new Vector3 (otherOne.GetStartingX(), setFloat, 0); 

			Manager.Instance.GetHarder ();
			Debug.Log ("Looped " + Manager.Instance.loopCount + " times");
		}

        transform.position += new Vector3(0, -Manager.Instance.scrollSpeed, 0);
    }

    public void SpawnBonus()
    {
        Debug.Log("Spawn Bonus Called!");
        int index = Random.Range(0, spawners.Length);
        spawners[index].GetComponent<SpawnerGeneric>().spawnSpecial = true;
        Debug.Log("Spawned at " + index);
    }

    public float GetStartingX()
    {
        return transform.position.x;
    }
    public float GetStartingY(){
		return transform.position.y;
	}
	public void SetLessFloat(float newFloat){
		lessFloat = newFloat;
	}
	public void SetSetFloat(float newFloat){
		setFloat = newFloat;
	}
}
