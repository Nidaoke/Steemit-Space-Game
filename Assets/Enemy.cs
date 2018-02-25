using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public int health;
    private float speedOffset;
    private bool shootType;

    public float shootChance, shootTime;
    public GameObject bullet;
    public Transform bulletTransform;
    private float tempShootTime = .5f;

    public enum DinoType { Turtle, Pterodactyl, Rex, Triceratops, Velociraptor, Stegosaurus}
    public DinoType dinoType;

	// Use this for initialization
	void Start () {
        speedOffset = Manager.Instance.scrollSpeed;

        SetTypeVariables();
	}
	
	// Update is called once per frame
	void Update () {
        MoveDown();

        if (shootType)
            WaitShoot();
    }

    void WaitShoot()
    {
        if(tempShootTime > 0)
        {
            tempShootTime -= Time.deltaTime;
        }
        else
        {
            tempShootTime = shootTime;
            Shoot();
        }
    }

    void Shoot()
    {
        if (Random.Range(0f, 1f) <= shootChance)
        {
            if(Manager.Instance.player != null)
                Instantiate(bullet, bulletTransform.position, Quaternion.identity);
        }
        else Debug.Log("Failed to make bullet bad chance");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Bullet"){
            Shot();
            Destroy(other.gameObject);
        }

        if(other.gameObject.tag == "EnemyKiller")
        {
            Die(0);
        }
    }

    void SetTypeVariables()
    {
        switch (dinoType)
        {
            case DinoType.Pterodactyl:
                //
                break;
            case DinoType.Rex:
                //
                break;
            case DinoType.Stegosaurus:
                shootType = true;
                break;
            case DinoType.Triceratops:
                //
                break;
            case DinoType.Turtle:
                //
                break;
            case DinoType.Velociraptor:
                //
                break;
        }
    }

    void Shot()
    {
        health--;
        if(health <= 0)
        {
            Die(5);
        }
    }

    void MoveDown()
    {
        speedOffset = Manager.Instance.scrollSpeed;
        transform.position -= new Vector3(0, speedOffset, 0);
    }

    void Die(int score)
    {
        Manager.Instance.IncreaseScore(score);
        Destroy(gameObject);
    }
}
