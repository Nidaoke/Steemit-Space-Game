using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public int health;
    public float runSpeed;
    private float speedOffset;
    private bool shootType, runType, waitType;

    public bool spawnBonus;
    public GameObject foodPickUp, triPickUp, boomerangPickUp;

    public bool alsoRunDown;

    public bool canBeShot = true;
    private int turtleWait = 0;

    private int pterTargetMatch = 0;

    public Rigidbody2D rgbd;

    public float shootChance, shootTime, bulletTurnOffset;
    public GameObject bullet;
    public Transform[] bulletTransform, bulletTargetTransform;
    private float tempShootTime = .5f;

    public Transform[] runTargets;

    //RegesRes
    public int bulletCount = 0; //SET PRIVATE LATER

    public enum DinoType { Turtle, Pterodactyl, Rex, Triceratops, Velociraptor, Stegosaurus}
    public DinoType dinoType;

    public GameObject player;

	// Use this for initialization
	void Start () {
        speedOffset = Manager.Instance.scrollSpeed;
        if (rgbd == null)
            rgbd = GetComponent<Rigidbody2D>();
        tempShootTime = shootTime;
        SetTypeVariables();
        health = Manager.Instance.enemyHealth;
        player = GameObject.FindGameObjectWithTag("Player");
        Destroy(gameObject, 15);
	}
	
	void Update () {
        if (waitType)
            WaitDefense();

        if (alsoRunDown)
            MoveDown();

        if (!runType)
            MoveDown();
        else
            RunToTarget();
        if (shootType)
            WaitShoot();

        if(dinoType == DinoType.Pterodactyl)
        {
            runSpeed = 4 + Manager.Instance.scrollSpeed * (1 / .015f);
        }

        if(dinoType == DinoType.Velociraptor)
        {
            runSpeed = 5 + Manager.Instance.scrollSpeed * (1 / .015f);
        }
    }

    void WaitDefense()
    {
        if (tempShootTime > 0)
        {
            tempShootTime -= Time.deltaTime;
        }
        else
        {
            tempShootTime = shootTime;
            if (!canBeShot)
                canBeShot = true;
            else
            {
                if (turtleWait == 0)
                    turtleWait = 1;
                else
                {
                    turtleWait = 0;
                    canBeShot = false;
                }
            }
        }
    }

    void RunToTarget()
    {
        float step = runSpeed * Time.deltaTime;

        if(dinoType == DinoType.Pterodactyl)
        {
            transform.position = Vector3.MoveTowards(transform.position, runTargets[pterTargetMatch].position, step);
            if(Vector3.Distance(transform.position, runTargets[pterTargetMatch].position) <= .25f)
            {
                if (pterTargetMatch < (runTargets.Length - 1))
                {
                    pterTargetMatch++;
                }
            }
        }

        if(dinoType == DinoType.Velociraptor)
            transform.position = Vector3.MoveTowards(transform.position, runTargets[0].position, step);
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
            //if (Manager.Instance.player != null)
            {
                if(dinoType == DinoType.Rex)                                                                                        //Reges
                {
                    Bullet bul1 = Instantiate(bullet, bulletTransform[bulletCount].position, Quaternion.identity).GetComponent<Bullet>();
                    bul1.MoveToTarget(bulletTargetTransform[bulletCount]);
                    if (bulletCount < 4)
                        bulletCount++;
                    else
                        bulletCount = 0;
                }

                if(dinoType == DinoType.Pterodactyl)
                {
                    if(Mathf.Abs(transform.position.x - runTargets[pterTargetMatch].position.x) > Mathf.Abs(transform.position.y - runTargets[pterTargetMatch].position.y))
                    {
                        Instantiate(bullet, bulletTransform[0].position, Quaternion.identity);
                        GetComponent<Animator>().SetBool("canSpit", true);
                    }
                }

                if(dinoType == DinoType.Stegosaurus)                                                                                //Stegi
                {
                    if (player.transform.position.y <= transform.position.y)
                    {
                        Bullet bul1 = Instantiate(bullet, bulletTransform[0].position, Quaternion.identity).GetComponent<Bullet>();
                        bul1.TurnPlayer(0);
                        Bullet bul2 = Instantiate(bullet, bulletTransform[0].position, Quaternion.identity).GetComponent<Bullet>();
                        Bullet bul3 = Instantiate(bullet, bulletTransform[0].position, Quaternion.identity).GetComponent<Bullet>();
                        bul2.TurnPlayer(bulletTurnOffset);
                        bul3.TurnPlayer(-bulletTurnOffset);
                    }
                }

                if(dinoType == DinoType.Triceratops)
                {
                    //Bullet bul1 = Instantiate(bullet, bulletTransform[0]).GetComponent<Bullet>();
                    Instantiate(bullet, bulletTransform[0]);
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Bullet"){
            Shot();
            if(other.gameObject.GetComponent<Bullet> ().killSelfOnCollision)
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
                shootType = true;
                runType = true;
                break;
            case DinoType.Rex:
                shootType = true;
                //shootMultiple = false;
                break;
            case DinoType.Stegosaurus:
                shootType = true;
                tempShootTime = .8f;
                //shootMultiple = true;
                break;
            case DinoType.Triceratops:
                tempShootTime = 1f;
                shootType = true;
                waitType = false;
                runType = false;
                canBeShot = true;
                break;
            case DinoType.Turtle:
                shootType = false;
                waitType = true;
                runType = false;
                canBeShot = false;
                break;
            case DinoType.Velociraptor:
                shootType = false;
                runType = true;
                break;
        }
    }

    void Shot()
    {
        if (canBeShot) {
            health--;
            if (health <= 0)
            {
                if (spawnBonus)
                {
                    float ran = Random.value;
                    if (ran <= .45f)
                    {
                        Instantiate(boomerangPickUp, transform.position, Quaternion.identity);
                    }
                    else if (ran <= .9f)
                    {
                        Instantiate(triPickUp, transform.position, Quaternion.identity);
                    }
                    else
                    {
                        Instantiate(foodPickUp, transform.position, Quaternion.identity);
                    }
                }

                Die(5);
            }
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
