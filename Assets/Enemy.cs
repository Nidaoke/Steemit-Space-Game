using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public int health;
    private float speedOffset;

	// Use this for initialization
	void Start () {
        speedOffset = Manager.Instance.scrollSpeed;
	}
	
	// Update is called once per frame
	void Update () {
        speedOffset = Manager.Instance.scrollSpeed;

        transform.position -= new Vector3(0, speedOffset, 0);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Bullet"){
            Shot();
            Destroy(other.gameObject);
        }
    }

    void Shot()
    {
        health--;
        if(health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
