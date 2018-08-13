using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public GameObject ingameCanvas;
    public GameObject menugo;
    public GameObject highscorego;
    public GameObject aboutgo;
    public GameObject settingsgo;
    public GameObject exitgo;
    public GameObject deadgo;
    public GameObject tutgo;
    public GameObject rewardedAdgo;


    private Animator menuAnim;
    private Animator highscoreAnim;
    private Animator aboutAnim;
    private Animator settingsAnim;
    private Animator exitAnim;
    private Animator deadAnim;
    private Animator tutAnim;
    private Animator rewardedAdAnim;


    public Button playButton;
    public Button pauseButton;
    public Button highScoreButton;
    public Button aboutButton;
    public Button settingsButton;
    public Button backButtonFromHighScore;
    public Button backButtonFromAbout;
    public Button backButtonFromSettings;
    public Button noButtonFromExit;
    public Button yesButtonFromExit;
    public Button exitButton;
    public Button restartButton;
    public Button menuButton;
    public Button tutorialButton;
    public Text gameOverText;
    public Text viewHighScoreText;
    public Slider volumeSlider;
    public Button tutorialDone;
    public Button showAd;
    public Button dontShowAd;


    public void StartGame()
    {
        GameManager.gameStarted = true;
        menuAnim.Play("Popup Out");
        FindObjectOfType<AudioManager>().Play("buttonclick");
        FindObjectOfType<AudioManager>().Play("swoosh");

    }

    public void PauseGame()
    {
        menuAnim.Play("Popup In");
        GameManager.gamePaused = true;
    }

    public void HighScoreOpen()
    {
        FindObjectOfType<AudioManager>().Play("buttonclick");
        FindObjectOfType<AudioManager>().Play("swoosh");
        highscorego.SetActive(true);
        highscoreAnim.Play("Popup In");
        viewHighScoreText.text = "High Score: " + PlayerPrefs.GetInt("highScore");
    }

    public void HighScoreClose()
    {
        FindObjectOfType<AudioManager>().Play("swoosh");
        FindObjectOfType<AudioManager>().Play("buttonclick");
        highscoreAnim.Play("Popup Out");
    }

    public void AboutOpen()
    {
        FindObjectOfType<AudioManager>().Play("buttonclick");
        FindObjectOfType<AudioManager>().Play("swoosh");
        aboutgo.SetActive(true);
        aboutAnim.Play("Popup In");
    }

    public void AboutClose()
    {
        FindObjectOfType<AudioManager>().Play("buttonclick");
        FindObjectOfType<AudioManager>().Play("swoosh");
        aboutAnim.Play("Popup Out");
    }

    public void SettingsOpen()
    {
        FindObjectOfType<AudioManager>().Play("buttonclick");
        FindObjectOfType<AudioManager>().Play("swoosh");
        settingsgo.SetActive(true);
        settingsAnim.Play("Popup In");
    }

    public void SettingsClose()
    {
       
        FindObjectOfType<AudioManager>().Play("buttonclick");
        FindObjectOfType<AudioManager>().Play("swoosh");
        settingsAnim.Play("Popup Out");
    }

    public void ExitOpen()
    {
        FindObjectOfType<AudioManager>().Play("buttonclick");
        FindObjectOfType<AudioManager>().Play("swoosh");
        exitgo.SetActive(true);
        exitAnim.Play("Popup In");
    }

    public void ExitNo()
    {
        FindObjectOfType<AudioManager>().Play("buttonclick");
        FindObjectOfType<AudioManager>().Play("swoosh");
        exitAnim.Play("Popup Out");
    }

    public void ExitYes()
    {
        FindObjectOfType<AudioManager>().Play("buttonclick");
        Application.Quit();
    }

    public void Restart()
    {
        FindObjectOfType<AudioManager>().Play("buttonclick");
        FindObjectOfType<AudioManager>().Play("swoosh");
        //
        deadAnim.Play("Popup Out");
        GameManager.playerDead = false;
        GameManager.Restart(true);
    }

    public void Menu()
    {
        GameManager.playInterstitialAd = true;
        FindObjectOfType<AudioManager>().Play("buttonclick");
        FindObjectOfType<AudioManager>().Play("swoosh");
        //
        deadAnim.Play("Popup Out");
        menugo.SetActive(true);
        menuAnim.Play("Popup In");
        ingameCanvas.SetActive(false);
        GameManager.playerDead = false;
        GameManager.Restart(false);

    }

    public void TutDone()
    {
        FindObjectOfType<AudioManager>().Play("buttonclick");
        FindObjectOfType<AudioManager>().Play("swoosh");
        tutAnim.Play("Popup Out");
    }

    public void TutOpen()
    {
        tutAnim.Play("Popup In");
        tutgo.SetActive(true);
        FindObjectOfType<AudioManager>().Play("buttonclick");
        FindObjectOfType<AudioManager>().Play("swoosh");
    }

    
    void Start () {
        //PlayerPrefs.DeleteAll();
        menuAnim = menugo.gameObject.GetComponent<Animator>();
        highscoreAnim = highscorego.gameObject.GetComponent<Animator>();
        aboutAnim = aboutgo.gameObject.GetComponent<Animator>();
        settingsAnim = settingsgo.gameObject.GetComponent<Animator>();
        exitAnim = exitgo.gameObject.GetComponent<Animator>();
        deadAnim = deadgo.gameObject.GetComponent<Animator>();
        tutAnim = tutgo.gameObject.GetComponent<Animator>();
        rewardedAdAnim = rewardedAdgo.gameObject.GetComponent<Animator>();


        playButton.onClick.AddListener(StartGame);
        pauseButton.onClick.AddListener(PauseGame);
        highScoreButton.onClick.AddListener(HighScoreOpen);
        backButtonFromHighScore.onClick.AddListener(HighScoreClose);
        aboutButton.onClick.AddListener(AboutOpen);
        backButtonFromAbout.onClick.AddListener(AboutClose);
        settingsButton.onClick.AddListener(SettingsOpen);
        backButtonFromSettings.onClick.AddListener(SettingsClose);
        exitButton.onClick.AddListener(ExitOpen);
        noButtonFromExit.onClick.AddListener(ExitNo);
        yesButtonFromExit.onClick.AddListener(ExitYes);
        restartButton.onClick.AddListener(Menu);
        menuButton.onClick.AddListener(Menu);
        tutorialDone.onClick.AddListener(TutDone);
        tutorialButton.onClick.AddListener(TutOpen);
        showAd.onClick.AddListener(ShowAd);
        dontShowAd.onClick.AddListener(DontShowAd);

    }




    public void RewardedAdScreen()
    {
        if (GameManager.elligibleForRevive)
        {
            DontShowAd();
            DeathScreen();
            return;
        }
        GameManager.elligibleForRevive = false;
        GameManager.onMoreLifeAd = true;
        rewardedAdgo.SetActive(true);
        Debug.Log("Ad Screen");
        FindObjectOfType<AudioManager>().Play("buttonclick");
        FindObjectOfType<AudioManager>().Play("swoosh");
        rewardedAdAnim.Play("Popup In");
    }

    public void ShowAd()
    {
        FindObjectOfType<AudioManager>().Play("buttonclick");
        FindObjectOfType<AudioManager>().Play("swoosh");
        rewardedAdAnim.Play("Popup Out");
        GameManager.playRewardAd = true;
    }

    public void DontShowAd()
    {

        FindObjectOfType<AudioManager>().Play("buttonclick");
        FindObjectOfType<AudioManager>().Play("swoosh");
        rewardedAdAnim.Play("Popup Out");
        DeathScreen();
        GameManager.playerDead = true;
    }

    void DeathScreen()
    {
        GameManager.prevScore = 0;
        GameManager.onMoreLifeAd = false;
        deadgo.SetActive(true);
        //ingameCanvas.SetActive(false);
        deadAnim.Play("Popup In");
        if (GameManager.hasNewHighScore)
        {
            gameOverText.text = "New High Score: " + GameManager.score;
        }
        else
        {
            gameOverText.text = "Score: " + GameManager.score;
        }
    }

	void Update () {
        if (GameManager.showAdScreen && GameManager.elligibleForRevive)
        {
            GameManager.elligibleForRevive = false;
            Debug.Log("Inside Invoking RewardAdScreen");
            GameManager.showAdScreen = false;
            Invoke("RewardedAdScreen", 2f);
        }
        else if(GameManager.playerDead && !GameManager.elligibleForRevive)
        {
            if (GameObject.FindGameObjectWithTag("Player") == null)
            {
                Invoke("DeathScreen", 2f);
            }
        }
            

        if (GameManager.restart)
        {
            GameManager.elligibleForRevive = true;
            menugo.SetActive(false);
            GameManager.showAdScreen = false;
        }

        GameManager.volume = volumeSlider.value;
        

    }
}
