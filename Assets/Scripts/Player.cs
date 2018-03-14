using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	private Rigidbody2D rgbd;
	public float speed;
    public float speedSec;
    public GameObject rockBullet;
    public float bulletShootingSpeed;
    public Transform bulletTransform;
    private float tempBulletSpeed;

    public SpriteRenderer caveMan;

    public bool invincible, canShoot;
    public float iFrames;

    private bool shootThree;
    private float tempTriTime;
    public float triOffset, triTime;

    private bool right, left, up, down;

	// Use this for initialization
	void Start () {
        tempBulletSpeed = bulletShootingSpeed;
        tempTriTime = triTime;
		rgbd = gameObject.GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {

        WaitShoot();

        Inputs();
    }

    void Inputs()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (Manager.Instance.collectedRock)
            {
                shootThree = true;
                Manager.Instance.RockEnd();
            }
        }

        //if (Input.GetAxis("Horizontal") > .3)
        if (Input.GetKey(KeyCode.RightArrow))
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

        Movement();
    }

    void Movement()
    {
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

    void WaitShoot()
    {
        if (tempBulletSpeed < 0)
        {
            Shoot();
            tempBulletSpeed = bulletShootingSpeed;
        }
        else
        {
            tempBulletSpeed -= Time.deltaTime;
        }

        if (shootThree)
        {
            ShootThree();
        }
    }

    void Shoot()
    {
        if (canShoot)
        {
            GameObject rock = Instantiate(rockBullet, bulletTransform.position, Quaternion.identity) as GameObject;
            rock.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 7);
            Destroy(rock, 5);

            if (shootThree)
            {
                GameObject rock1 = Instantiate(rockBullet, bulletTransform.position, Quaternion.identity) as GameObject;
                rock1.GetComponent<Rigidbody2D>().velocity = new Vector2(triOffset, 7);
                Destroy(rock1, 5);

                GameObject rock2 = Instantiate(rockBullet, bulletTransform.position, Quaternion.identity) as GameObject;
                rock2.GetComponent<Rigidbody2D>().velocity = new Vector2(-triOffset, 7);
                Destroy(rock2, 5);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Enemy" || other.gameObject.tag == "EnemyBullet")
        {
            if (!invincible)
            {
                Die();
                Destroy(other.gameObject);
            }
        }

        if(other.gameObject.tag == "RockPickUp")
        {
            Manager.Instance.RockPickUp();
            Destroy(other.gameObject);
        }
    }

    void ShootThree()
    {
        if(tempTriTime > 0)
        {
            tempTriTime -= Time.deltaTime;
        }
        else
        {
            shootThree = false;
            tempTriTime = triTime;
        }
    }

    void Die()
    {
        StartCoroutine(IFrames());
        Manager.Instance.LoseLife();
    }

    IEnumerator IFrames()
    {
        invincible = true;
        canShoot = false;
        caveMan.color = new Color(caveMan.color.r, caveMan.color.g, caveMan.color.b, .5f);
        yield return new WaitForSeconds(iFrames);
        invincible = false;
        canShoot = true;
        caveMan.color = new Color(caveMan.color.r, caveMan.color.g, caveMan.color.b, 1f);
    }
}