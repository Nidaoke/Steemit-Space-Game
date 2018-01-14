﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VertScrollingCode : MonoBehaviour {

	public float speed;
	public bool starting;
	public VertScrollingCode otherOne;

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
		transform.position += new Vector3 (0, -speed, 0);

		if (transform.position.y <= lessFloat) {
			transform.position = new Vector3 (0, setFloat, 0); 
		}
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
