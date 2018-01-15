using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	private Rigidbody2D rgbd;
	public float speed;

	// Use this for initialization
	void Start () {
		rgbd = gameObject.GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (Input.GetAxis ("Horizontal") > .3) {
			rgbd.AddForce (new Vector2(speed, 0));
		}
		if (Input.GetAxis ("Horizontal") < -.3) {
			rgbd.AddForce (new Vector2(-speed, 0));
		}
		if (Input.GetAxis ("Vertical") > .3) {
			rgbd.AddForce (new Vector2(0, speed));
		}
		if (Input.GetAxis ("Vertical") < -.3) {
			rgbd.AddForce (new Vector2(0, -speed));
		}
	}
}
