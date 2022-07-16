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
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private Image lifeBar;

    private void Awake()
    {
        if (ui == null) { ui = this; return; }
        Destroy(gameObject);
    }

    private void Start()
    {
        UpdateScoreText();
    }

    public void UpdateScoreText()
    {
        scoreText.text = "Score: " + GameController.gc.playerScore;
    }

    public void UpdateLifeBar(PlayerLife playerLife, int totalLife)
    {
        lifeBar.fillAmount = (float)playerLife.currentLife / (float)totalLife;
    }

    public void RetryLevel() { LoadScene(GameController.gc.currentScene); }

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
}