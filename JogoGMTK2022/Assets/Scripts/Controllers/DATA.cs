using UnityEngine;
using System.Collections.Generic;

public class DATA : MonoBehaviour
{
    public static DATA d;

    public int playerHighScore { get; set; }
    [Header("Configs")]
    public float musicVolume = 1;
    public float SFXVolume = 1;
    public float ambientationVolume = 1;

    [Header("Verifiers")]
    public bool playedSplashScreen;
    public bool playedIntro;

    void Awake()
    {
        if (d == null) { d = this; }
        else { Destroy(gameObject); }
        DontDestroyOnLoad(gameObject);
        LoadConfigData();
    }

    public void SaveMatchData()
    {
        SaveSystem.SaveInt("playerHighScore", playerHighScore);
    }
    public void LoadMatchData()
    {
        playerHighScore = SaveSystem.LoadInt("playerHighScore", 0);
    }

    public void SaveConfigData()
    {
        SaveSystem.SaveFloat("musicVolume", musicVolume);
        SaveSystem.SaveFloat("SFXVolume", SFXVolume);
        SaveSystem.SaveFloat("ambientationVolume", ambientationVolume);
    }
    public void LoadConfigData()
    {
        musicVolume = SaveSystem.LoadFloat("musicVolume", 1);
        SFXVolume = SaveSystem.LoadFloat("SFXVolume", 1);
        ambientationVolume = SaveSystem.LoadFloat("ambientationVolume", 1);
    }
}