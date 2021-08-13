using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public event EventHandler<OnGameStageChangedEventArgs> OnGameStageChanged;

    public static GameController Instance { get; private set; }
    public GameStage GameStage { get; private set; }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        SetGameStage(GameStage.Loaded);
    }

    public void SetGameStage(GameStage gameStage)
    {
        GameStage = gameStage;

        if (gameStage == GameStage.Ended)
        {
            if (SceneManager.GetActiveScene().buildIndex < SceneManager.sceneCountInBuildSettings - 1)
                PlayerPrefs.SetInt("LastLevel", SceneManager.GetActiveScene().buildIndex + 1);
            else
                PlayerPrefs.SetInt("LastLevel", 1);

        }
        
        OnGameStageChanged?.Invoke(this, new OnGameStageChangedEventArgs { gameStage = gameStage});
        
    }

    public class OnGameStageChangedEventArgs: EventArgs
    {
        public GameStage gameStage;
    }
}

public enum GameStage {NotLoaded, Loaded, Started, Ended }
