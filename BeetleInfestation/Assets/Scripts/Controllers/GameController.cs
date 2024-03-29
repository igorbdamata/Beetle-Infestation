﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static GameController gc;

    public bool isPaused { get; set; }
    public bool inLoading { get; set; }
    public bool finishedAllLevel { get; set; }
    public bool finishedGrounds { get; set; }
    public int currentScene { get; private set; }
    private int _playerScore;
    public int playerScore
    {
        get => _playerScore;
        set
        {
            _playerScore = value;
            UI.ui.UpdateScoreText();
        }
    }

    [Space(20)]
    [Header("Title screen")]
    [SerializeField] private Slider ambientationVolSlider, musicVolSlider, SFXVolSlider;
    public EventSystem eventSystem { get; private set; }
    public Camera mainCamera { get; private set; }
    public CameraShakeController camShake { get; private set; }

    public List<Transform> enemies { get; set; } = new List<Transform>();


    void Awake()
    {
        if (gc == null) { gc = this; }
        else { Destroy(gameObject); }
        eventSystem = FindObjectOfType<EventSystem>();
        mainCamera = Camera.main;
        camShake = FindObjectOfType<CameraShakeController>();
    }
    private void Start()
    {
        Time.timeScale = 1;
        currentScene = SceneManager.GetActiveScene().buildIndex;

        if (currentScene == 0)
        {
            musicVolSlider.value = DATA.d.musicVolume;
            SFXVolSlider.value = DATA.d.SFXVolume;
            SoundController.sc.PlayMusic(SoundController.sc.titleScreenMusic);
        }
        else if(currentScene==1)
        {
            SoundController.sc.PlayMusic(SoundController.sc.gamePlayMusic);
        }
    }
}