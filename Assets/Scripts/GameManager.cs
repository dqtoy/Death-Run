using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    [HideInInspector]
    public static int flyPickupCount = 0;
    [HideInInspector]
    public static float flyFuel = 0;
    public static float pickupToFuelFactor = 2.5f;
    [HideInInspector]
    public static int score = 0;
    [HideInInspector]
    public static bool playerDead = false;
    private GameObject destroyedPlayer;
    public Text scoreText;
    public Text starsText;
    public Slider fuelSlider;
    public static bool gameStarted = false;
    public GameObject inGameCanvas;
    public static bool gamePaused = false;
    public static bool restart = false;
    public static int highScore = 0;
    public static bool hasNewHighScore = false;
    public static float volume = 1f;
    public static bool showAdScreen;
    public static bool playerPaused;
    public static int prevScore = 0;
    public static int rewardUsed = 0;
    public static bool preGameStarted = false;
    public static bool onMoreLifeAd = false;
    public static bool elligibleForRevive = true;
    public static bool playRewardAd = false;
    public static bool playInterstitialAd = false;

    void Start() {
        gameStarted = false;
    }

    void FlyFuelValueFixer()
    {
        if (flyFuel >= 100)
        {
            flyFuel = 100;
        }
        if (flyFuel <= 0)
        {
            flyFuel = 0;
        }
    }
    void CountScore()
    {
        scoreText.text = ((int)score).ToString();
        starsText.text = flyPickupCount.ToString();
        fuelSlider.value = flyFuel;
    }

    void CheckPlayerDead()
    {
        if (playerDead)
        {
            prevScore = 0;
            if (!PlayerPrefs.HasKey("highScore"))
            {
                PlayerPrefs.SetInt("highScore", score);
                hasNewHighScore = true;
            }
            else if(score > PlayerPrefs.GetInt("highScore"))
            {
                PlayerPrefs.SetInt("highScore", score);
                hasNewHighScore = true;
            }
            else
            {
                hasNewHighScore = false;
            }
        }
    }

    public static void Restart(bool re){
        FindObjectOfType<AudioManager>().Play("buttonclick");
        FindObjectOfType<AudioManager>().Play("swoosh");
        restart = re;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        elligibleForRevive = true;
        flyPickupCount = 0;
        flyFuel = 0;

    }

    public static void Replay()
    {
        prevScore = score;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        playerDead = false;
        restart = true;
    }

	void Update () {
        if (gameStarted)
        {
            inGameCanvas.SetActive(true);
        }
        if (restart)
        {
            gameStarted = true;
        }
        CountScore();
        FlyFuelValueFixer();
        CheckPlayerDead();
        AudioListener.volume = volume;
        //Debug.Log(flyFuel + "   " + (int)score);
	}
}
