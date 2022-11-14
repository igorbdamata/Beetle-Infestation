using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEditor;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class UI : MonoBehaviour
{
    public static UI ui;
    [SerializeField] private GameObject loadingScreenPrefab;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private Image lifeBar;
    private PlayerLife pLife;

    private void Awake()
    {
        if (ui == null) { ui = this; return; }
        Destroy(gameObject);
    }

    private void Start()
    {
        if (GameController.gc.currentScene == 2){ UpdateScoreText();pLife = FindObjectOfType<PlayerLife>(); }
    }

    private void Update()
    {
        if(GameController.gc.currentScene==2 && Input.GetKeyDown(KeyCode.Escape) && !pLife.isDead)
        {
            Pause();
        }
    }

    public void UpdateScoreText()
    {
        scoreText.text = "Score: " + GameController.gc.playerScore;
    }

    public void UpdateLifeBar(PlayerLife playerLife, int totalLife)
    {
        lifeBar.fillAmount = (float)playerLife.currentLife / (float)totalLife;
    }

    public void RetryLevel() { LoadScene(1); }

    public void LoadScene(int index)
    {
        SaveSystem.SaveInt("SceneToLoad", index);
        Instantiate(loadingScreenPrefab, transform.position, transform.rotation);
    }
    public void Quit() { Application.Quit(); }

    public void SetGameOverScreen()
    {
        gameOverScreen.SetActive(true);
        if (GameController.gc.playerScore > DATA.d.playerHighScore)
        {
            DATA.d.playerHighScore = GameController.gc.playerScore;
            DATA.d.SaveMatchData();
        }
        gameOverScreen.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "Score: " + GameController.gc.playerScore;
        gameOverScreen.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = "High score: " + DATA.d.playerHighScore;
    }

    public void Pause()
    {
        GameController.gc.isPaused = !GameController.gc.isPaused;
        Time.timeScale = GameController.gc.isPaused ? 0 : 1;
        pauseMenu.SetActive(GameController.gc.isPaused);
        foreach(Animator t in FindObjectsOfType<Animator>())
        {
            t.enabled = !GameController.gc.isPaused;
        }
    }

}