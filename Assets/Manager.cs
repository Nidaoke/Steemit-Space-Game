public class Manager : Singleton<Manager> {
	protected Manager () {} // guarantee this will be always a singleton only - can't use the constructor!

	//public string testGlobalVar = "test!";
	public int loopCount = 0;
	public float scrollSpeed = 1;

	public void GetHarder(){
		scrollSpeed += .05f;
		loopCount++;
	}
}