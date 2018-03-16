using UnityEngine.UI;
using UnityEngine;
public class Manager : Singleton<Manager> {
	protected Manager () {} // guarantee this will be always a singleton only - can't use the constructor!

	//public string testGlobalVar = "test!";
	public int loopCount = 0, playerLives = 3, playerScore = 0, enemyHealth;
    public float bulletSpeed = 5;
	public float scrollSpeed = 1;
    public float powerUpChance = .2f;
    public Text lifeText, scoreText;
    public Image rockPileImage, boomerangImage;
    public GameObject player;

    public GameObject scroll1, scroll2;

    public bool startSpawningDrops;

    public bool collectedRock, collectedBoomerang;

    public void Start()
    {
        ShowLives();
        #if UNITY_STANDALONE
            scrollSpeed *= 2;
        #endif
    }

    public void Update()
    {
        #if UNITY_STANDALONE
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Application.Quit();
            }
        #endif
    }

    public void RockPickUp()
    {
        collectedRock = true;
        rockPileImage.enabled = true;
    }

    public void RockEnd()
    {
        collectedRock = false;
        rockPileImage.enabled = false;
    }

    public void BoomerangPickUp()
    {
        collectedBoomerang = true;
        boomerangImage.enabled = true;
    }

    public void BoomerangEnd()
    {
        collectedBoomerang = false;
        boomerangImage.enabled = false;
    }

	public void GetHarder(){
		scrollSpeed *= 1.1f;
        //bulletSpeed *= 1.1f;
		loopCount++;

        if(loopCount == 2)
        {
            startSpawningDrops = true;
        }

        if (startSpawningDrops)
        {
            if(loopCount % 2 == 0)
            {
                enemyHealth++;
            }

            float ran = Random.value;
            if(ran <= powerUpChance)
            {
                Debug.Log("Spawn!");
                if(loopCount % 2 == 0)
                {
                    Debug.Log("Spawn 1");
                    scroll1.GetComponent<VertScrollingCode>().SpawnBonus();
                }else if(loopCount % 2 == 1)
                {
                    Debug.Log("Spawn 2");
                    scroll2.GetComponent<VertScrollingCode>().SpawnBonus();
                }
            }
        }
	}

    void ShowLives()
    {
        lifeText.text = ("Lives: " + playerLives);
    }

    public void LoseLife()
    {
        playerLives--;

        ShowLives();
        
        if(playerLives <= 0)
        {
            GameOver();
        }
    }

    public void GainLife()
    {
        playerLives++;
        ShowLives();
    }

    public void IncreaseScore(int score)
    {
        playerScore += score;
        scoreText.text = ("Score: " + playerScore);
    }

    public void GameOver()
    {
        scrollSpeed = 0;
        Destroy(player);
        Debug.Log("Game Over");
    }
}