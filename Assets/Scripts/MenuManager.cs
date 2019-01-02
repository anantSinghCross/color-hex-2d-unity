using System.Collections;
using System.Collections.Generic;
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

    private BannerView bannerView;

    void Start()
    {
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
        this.RequestBanner();
    }
    private void RequestBanner()
    {
        string adUnitId = "ca-app-pub-3940256099942544/6300978111";
        bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Bottom);

        AdRequest request = new AdRequest.Builder().Build();

        bannerView.LoadAd(request);
        BannerHide();
    }

    public void BannerHide()
    {
        bannerView.Hide();
    }

    public void BannerShow()
    {
        bannerView.Show();
    }

    public void PlayGame()
    {
        player.SetActive(true);
        knob.SetActive(true);
        spawner.SetActive(true);

        mainMenuPanel.SetActive(false);
        endMenuPanel.SetActive(false);
        scoreTextObj.SetActive(true);
        scoreText.text = "0";
        //hide ad
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
        //show ad
        BannerShow();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f;
    }

    public void ReplayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f;
        //show ad
        BannerShow();
    }

    public void QuitGame()
    {
        //Debug.Log("QUIT GAME");
        Application.Quit();
    }

    public void PauseGame()
    {
        isGamePaused = true;
        pauseMenuPanel.SetActive(true);

        //show ad
        BannerShow();
        Time.timeScale = 0f;

    }

    public void ResumeGame()
    {
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
}
