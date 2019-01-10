using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using GoogleMobileAds.Api;


public class MenuManager : MonoBehaviour
{
    public GameObject player;
    public GameObject knob;
    public GameObject spawner;
    public GameObject mainMenuPanel;
    public GameObject endMenuPanel;
    public GameObject scoreTextObj;
    public GameObject pauseMenuPanel;
    public GameObject instructionsPanel;
    public Text scoreText;
    public static bool isGamePaused = false;
    ScoreManager scoreManager;
    public AudioSource source;
    public AudioClip deathClip;
    private bool isAdRequested;
    public Text VOLUME;
    private float originalVolume;

    public BannerView bannerView;


    void Start()
    {
        originalVolume = PlayerPrefs.GetFloat("Volume",.2f);
        source = GameObject.Find("MenuManager").GetComponent<AudioSource>();
        source.Play();
        source.volume = originalVolume;
        VOLUME.text = PlayerPrefs.GetString("VolumeText", "VOLUME : ON");

        player = GameObject.Find("Player");
        knob = GameObject.Find("Knob");
        spawner = GameObject.Find("Spawner");
        mainMenuPanel = GameObject.Find("MainMenuPanel");
        endMenuPanel = GameObject.Find("EndMenuPanel");
        scoreTextObj = GameObject.Find("ScoreTextObj");
        pauseMenuPanel = GameObject.Find("PauseMenuPanel");
        instructionsPanel = GameObject.Find("InstructionsPanel");
        scoreManager = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();

        player.SetActive(false);
        knob.SetActive(false);
        spawner.SetActive(false);
        endMenuPanel.SetActive(false);
        scoreTextObj.SetActive(false);
        pauseMenuPanel.SetActive(false);
        instructionsPanel.SetActive(false);
        scoreText.text = "";

        string appId = "ca-app-pub-6755498980044352~2620595610";
        MobileAds.Initialize(appId);
        RequestBanner();
    }

    private void Update()
    {
        if (isAdRequested == true)
        {
            BannerHide();
            isAdRequested = false;
        }
    }

    private void RequestBanner()
    {
        //adUnitId = ca-app-pub-6755498980044352/7620930411
        //adUnitId for testing = ca-app-pub-3940256099942544/6300978111
        string adUnitId = "ca-app-pub-6755498980044352/7620930411";
        bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Bottom);

        AdRequest request = new AdRequest.Builder().Build();

        bannerView.LoadAd(request);
        isAdRequested = true;
    }

    public void BannerHide()
    {
        bannerView.Hide();
    }

    public void BannerShow()
    {
        bannerView.Show();
    }

    public void BannerDestroy()
    {
        bannerView.Destroy();
    }

    public void PlayGame()
    {
        //destroy the ads when starting the game
        BannerDestroy();
        player.SetActive(true);
        knob.SetActive(true);
        spawner.SetActive(true);

        mainMenuPanel.SetActive(false);
        endMenuPanel.SetActive(false);
        scoreTextObj.SetActive(true);
        scoreText.text = "0";
        //create but hide the ads so that they are available but hidden and
        //can be shown whenever needed
        RequestBanner();
        BannerHide();
    }

    public void EndGame()
    {
        player.SetActive(false);
        knob.SetActive(false);
        spawner.SetActive(false);

        scoreTextObj.SetActive(false);
        endMenuPanel.SetActive(true);
        scoreManager.correctScore();
        source.Stop();
        source.PlayOneShot(deathClip, PlayerPrefs.GetFloat("Volume",.2f));
        //show ad
        BannerShow();
    }

    public void RestartGame()
    {
        
        Time.timeScale = 1f;
        //destroy ad
        BannerDestroy();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }

    public void ReplayGame()
    {
        Time.timeScale = 1f;
        //destroy ad
        BannerDestroy();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void PauseGame()
    {
        isGamePaused = true;
        pauseMenuPanel.SetActive(true);
        //source.volume = (source.volume)/2;
        //show ad
        BannerShow();
        Time.timeScale = 0f;

    }

    public void ResumeGame()
    {
        //source.volume = originalVolume;
        Time.timeScale = 1f;
        //hide ad
        BannerHide();
        pauseMenuPanel.SetActive(false);
    }

    public void Instructions()
    {
        mainMenuPanel.SetActive(false);
        instructionsPanel.SetActive(true);
    }

    public void Back()
    {
        instructionsPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }

    public void ToggleVolume()
    {
        if (PlayerPrefs.GetFloat("Volume") > 0f)
        {
            source.volume = 0f;
            PlayerPrefs.SetFloat("Volume",0f);
            VOLUME.text = "VOLUME : OFF";
            PlayerPrefs.SetString("VolumeText", VOLUME.text);
        }
        else
        {
            source.volume = .2f;
            PlayerPrefs.SetFloat("Volume", .2f);
            VOLUME.text = "VOLUME : ON";
            PlayerPrefs.SetString("VolumeText", VOLUME.text);
        }
    }
}
