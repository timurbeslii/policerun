using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager gm { get { return _instance; } }
    public GameObject SliderTutorial;

    public GameObject LevelCompletedUI;
    public Image RankImageBG;
    public Image RankFillImage;
    public GameObject LevelFailedUI;
    public Level[] levels;
    public Image[] finalRankUI;
    public TMP_Text levelText;

    public bool gameStarted,gameEnded,finishReached,finished;
    


    private void Awake()
    {

        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }

        if (!PlayerPrefs.HasKey("levelIndex"))
        {
            PlayerPrefs.SetInt("levelIndex", 0);
            PlayerPrefs.SetInt("elephantLevelIndex", 1);
            PlayerPrefs.SetInt("badgeCount", 0);
        }
        PlayerPrefs.SetInt("badgeCountCollected", 0);
        levelText.text="LEVEL " + (PlayerPrefs.GetInt("levelIndex")+1).ToString();
        Application.targetFrameRate = 1000;
    }
    
    public void ShowLevelEndUI(bool completed)
    {
        if (completed)
        {
            ElephantSDK.Elephant.LevelCompleted(PlayerPrefs.GetInt("levelIndex"));
            LevelCompletedUI.SetActive(true);
        }
        else
        {
            ElephantSDK.Elephant.LevelFailed(PlayerPrefs.GetInt("levelIndex"));
            LevelFailedUI.SetActive(true);
        }
    }
    public void StartLevel()
    {
       ElephantSDK.Elephant.LevelStarted(PlayerPrefs.GetInt("levelIndex"));
        SliderTutorial.SetActive(false);
        gameStarted = true;
        EventManager.eventManager.StartTheGame();
    }
    public void NextButton()
    {
       
        PlayerPrefs.SetInt("levelIndex",PlayerPrefs.GetInt("levelIndex") + 1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void RestartButton()
    {
      
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        
    }
   

    private void Update()
    {
        if (!gameStarted)
        {
            if (Input.GetMouseButtonDown(0))
            {
                StartLevel();
            }
        }
    }



}
[Serializable]
public class Level
{
    public int levelTileLength;
    public int levelEndTileLength;
    public int maximumArrestCount;
    public bool isDroneTileAvailable;
    public bool isMonkeyTileAvailable;
    public float rankFillAmount;
    public float rankFillAmountAtStart;
    public Sprite rankBG;
    public Sprite rankFillImage;

    public Material verticalFogMaterial;
    public Material skyBoxMaterial;
    public Color cameraBackgroundColor;
    public Color fogColor;
    public GameObject planeBG;

    public Material policeHairMat;
    public Material policeUniformMat;
    public Material policeBootMat;
    public Material thiefMat;

    public GameObject Environment;
   

}