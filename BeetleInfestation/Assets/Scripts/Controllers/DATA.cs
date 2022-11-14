using UnityEngine;
using System.Collections.Generic;

public class DATA : MonoBehaviour
{
    public static DATA d;

    public int playerHighScore { get; set; }
    public int currenteWeapon { get; set; }
    public List<Weapon> weapons;
    public float musicVolume { get; set; } = 1;
    public float SFXVolume { get; set; } = 1;

    [Header("Verifiers")]
    public bool playedSplashScreen;
    public bool playedIntro;

    void Awake()
    {
        if (d == null) { d = this; }
        else { Destroy(gameObject); }
        DontDestroyOnLoad(gameObject);
        LoadConfigData(); LoadMatchData();
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
    }
    public void LoadConfigData()
    {
        musicVolume = SaveSystem.LoadFloat("musicVolume", 1);
        SFXVolume = SaveSystem.LoadFloat("SFXVolume", 1);
    }
}