using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public bool enemyBullet, followPlayer, justOffset;
    //public float speed;
    //public Vector2 trans;

    public bool moveDown;
    public float moveDownSpeed;

    public bool killSelfOnCollision;

    public bool typeOffsetNeg;

    private GameObject player;
    public Rigidbody2D rgbd;

	// Use this for initialization
	void Start () {
        if (player == null)
            player = Manager.Instance.player;
        if (rgbd == null)
            rgbd = GetComponent<Rigidbody2D>();

        Destroy(gameObject, 5);
    }

    public void TurnPlayer(float offset) //STEG SHOOTING
    {
        if (followPlayer)
        {
            if (player == null)
                player = Manager.Instance.player;
            if (rgbd == null)
                rgbd = GetComponent<Rigidbody2D>();

            Vector3 dir = player.transform.position - transform.position;
            //Debug.Log(dir.y);
            if (dir.y > -1f)
                Destroy(gameObject);
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

            if (typeOffsetNeg == false)
            {
                transform.rotation = Quaternion.AngleAxis(angle - 90 + offset, Vector3.forward);
                rgbd.velocity = transform.up * Manager.Instance.bulletSpeed;
            }
            else
            {
                transform.rotation = Quaternion.AngleAxis(angle - 270 + offset, Vector3.forward);
                rgbd.velocity = transform.up * -Manager.Instance.bulletSpeed;
            }
        }
    }

    public void MoveToTarget(Transform target) //TREX SHOOTING
    {
        Vector3 dir = target.transform.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (typeOffsetNeg == false)
        {
            transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
            rgbd.velocity = transform.up * Manager.Instance.bulletSpeed;
        }
        else
        {
            transform.rotation = Quaternion.AngleAxis(angle - 270, Vector3.forward);
            rgbd.velocity = transform.up * -Manager.Instance.bulletSpeed;
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (moveDown)
        {
            transform.position += new Vector3(0, -moveDownSpeed * Manager.Instance.scrollSpeed, 0);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "EnemyKiller")
        {
            Destroy(gameObject);
        }
    }
}
