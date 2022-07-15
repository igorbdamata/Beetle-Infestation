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

    private void Awake()
    {
        if (ui == null) { ui = this; return; }
        Destroy(gameObject);
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
        gameOverScreen.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text="Score: " + GameController.gc.playerScore;
        gameOverScreen.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text="High score: " + DATA.d.playerHighScore;
    }
}