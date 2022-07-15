using System.Collections;
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
    public int currentScene { get; private set; }

    [Space(20)]
    [Header("Title screen")]
    [SerializeField] private Slider ambientationVolSlider, musicVolSlider, SFXVolSlider;
    public EventSystem eventSystem { get; private set; }
    public Camera mainCamera { get; private set; }

    void Awake()
    {
        if (gc == null) { gc = this; }
        else { Destroy(gameObject); }
        eventSystem = FindObjectOfType<EventSystem>();
        mainCamera = Camera.main;
    }
    private void Start()
    {
        Time.timeScale = 1;
        currentScene = SceneManager.GetActiveScene().buildIndex;

        musicVolSlider.value = DATA.d.musicVolume;
        SFXVolSlider.value = DATA.d.SFXVolume;
        ambientationVolSlider.value = DATA.d.ambientationVolume;
    }
}