using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	private Rigidbody2D rgbd;
	public float speed;
    public float speedSec;
    public GameObject rockBullet;

    private bool right, left, up, down;

	// Use this for initialization
	void Start () {
        StartCoroutine(Shoot());
		rgbd = gameObject.GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKey(KeyCode.Space))
        {
            GameObject rock = Instantiate(rockBullet, transform.position, Quaternion.identity) as GameObject;
            rock.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 7);
            Destroy(rock, 5);
        }

        //if (Input.GetAxis("Horizontal") > .3)
        if(Input.GetKey(KeyCode.RightArrow))
        {
            right = true;
        }
        else right = false;
        //if (Input.GetAxis("Horizontal") < -.3)
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            left = true;
        }
        else left = false;
        //if (Input.GetAxis("Vertical") > .3)
        if (Input.GetKey(KeyCode.UpArrow))
        {
            up = true;
        }
        else up = false;
        //if (Input.GetAxis("Vertical") < -.3)
        if (Input.GetKey(KeyCode.DownArrow))
        {
            down = true;
        }
        else down = false;

        if ((right && up) || (right && down) || (left && up) || (left && down))
        {
            speedSec = Mathf.Sqrt(.5f) * speed;
        }
        else speedSec = speed;

        if (right)
            rgbd.velocity = new Vector2(speedSec, rgbd.velocity.y);
        else if (left)
            rgbd.velocity = new Vector2(-speedSec, rgbd.velocity.y);
        else
            rgbd.velocity = new Vector2(0, rgbd.velocity.y);

        if (up)
            rgbd.velocity = new Vector2(rgbd.velocity.x, speedSec);
        else if (down)
            rgbd.velocity = new Vector2(rgbd.velocity.x, -speedSec);
        else
            rgbd.velocity = new Vector2(rgbd.velocity.x, 0);
    }

    IEnumerator Shoot()
    {
        GameObject rock = Instantiate(rockBullet, transform.position, Quaternion.identity) as GameObject;
        rock.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 7);
        Destroy(rock, 5);
        yield return new WaitForSeconds(1);
        StartCoroutine(Shoot());
    }
}
