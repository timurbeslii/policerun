using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;

public class LevelCompletedAnim : MonoBehaviour
{
    public GameObject topPart;
    public GameObject topPart_Appear;
    public GameObject rankUI;

   
    public GameObject NextButton;
    public GameObject Coin;
    public TMP_Text coinText;
    public Image bg;
    int amount;
    public bool isLose;
    bool isCoinTextAnimating;
    void Start()
    {
        amount = PlayerPrefs.GetInt("badgeCount");
        bg.DOFade(.65f, 3f);
        topPart.transform.DOMove(topPart_Appear.transform.position, 3).SetEase(Ease.OutBounce).OnComplete(()=>
        {
            if (!isLose)
            {
                GameManager.gm.RankImageBG.sprite = GameManager.gm.levels[PlayerPrefs.GetInt("levelIndex")].rankBG;
                GameManager.gm.RankFillImage.sprite = GameManager.gm.levels[PlayerPrefs.GetInt("levelIndex")].rankFillImage;
                rankUI.transform.DOScale(Vector3.one, 1).OnComplete(() =>
                {
                    GameManager.gm.RankImageBG.fillAmount = GameManager.gm.levels[PlayerPrefs.GetInt("levelIndex")].rankFillAmountAtStart;
                    GameManager.gm.RankFillImage.DOFillAmount(GameManager.gm.levels[PlayerPrefs.GetInt("levelIndex")].rankFillAmount, 1);

                });
            }

            if (true)
            {
                Coin.transform.DOScale(1, 1).OnComplete(() =>
                {
                    amount = 0;
                    isCoinTextAnimating = true;
                    DOTween.To(() => amount, x => amount = x, PlayerPrefs.GetInt("badgeCountCollected"), 1).OnComplete(() =>
                    {
                        isCoinTextAnimating = false;
                        NextButton.transform.DOScale(Vector3.one, 1);
                       
                        PlayerPrefs.SetInt("badgeCount", PlayerPrefs.GetInt("badgeCount") + PlayerPrefs.GetInt("badgeCountCollected"));
                    });

                });
            }
            else
            {
                NextButton.transform.DOScale(Vector3.one, 1);
                PlayerPrefs.SetInt("badgeCount", PlayerPrefs.GetInt("badgeCount") + PlayerPrefs.GetInt("badgeCountCollected"));
            }
        });
    }
    void Update()
    {
        if (isCoinTextAnimating)
        {
            coinText.text = amount.ToString();
        }
    }

}
