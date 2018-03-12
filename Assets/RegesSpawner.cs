using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegesSpawner : MonoBehaviour {

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
            Instantiate(rex, transform.position, Quaternion.identity);
        }
    }
}
