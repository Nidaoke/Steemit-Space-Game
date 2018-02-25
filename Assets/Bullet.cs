using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public bool enemyBullet, followPlayer;
    public float speed;
    public Vector2 trans;

    private GameObject player;
    private Rigidbody2D rgbd;

	// Use this for initialization
	void Start () {
        player = Manager.Instance.player;
        rgbd = GetComponent<Rigidbody2D>();

        if (followPlayer)
        {
            Vector3 dir = player.transform.position - transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
            rgbd.velocity = transform.up * speed;
        }
        
	}
	
	// Update is called once per frame
	void Update () {
    }
}
