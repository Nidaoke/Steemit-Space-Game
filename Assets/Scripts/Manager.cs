using UnityEngine.UI;
using UnityEngine;
public class Manager : Singleton<Manager> {
	protected Manager () {} // guarantee this will be always a singleton only - can't use the constructor!

	//public string testGlobalVar = "test!";
	public int loopCount = 0, playerLives = 3, playerScore = 0;
    public float bulletSpeed = 5;
	public float scrollSpeed = 1;
    public Text lifeText, scoreText;
    public GameObject player;

    public void Update()
    {
        #if UNITY_STANDALONE
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        #endif
    }

	public void GetHarder(){
		scrollSpeed *= 1.2f;
        bulletSpeed *= 1.1f;
		loopCount++;
	}

    public void LoseLife()
    {
        playerLives--;
        lifeText.text = ("Lives: " + playerLives);

        if(playerLives <= 0)
        {
            GameOver();
        }
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