using UnityEngine.UI;
public class Manager : Singleton<Manager> {
	protected Manager () {} // guarantee this will be always a singleton only - can't use the constructor!

	//public string testGlobalVar = "test!";
	public int loopCount = 0, playerLives = 3;
	public float scrollSpeed = 1;
    public Text lifeText;

	public void GetHarder(){
		scrollSpeed *= 1.2f;
		loopCount++;
	}

    public void LoseLife()
    {
        playerLives--;
        lifeText.text = ("Lives: " + playerLives);
    }
}