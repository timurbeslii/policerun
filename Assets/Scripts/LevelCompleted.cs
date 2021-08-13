using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;

public class LevelCompleted : MonoBehaviour
{
    [Header("Level End UI'S")]
    public GameObject LevelCompletedUI;
    public GameObject LevelFailedUI;
    public GameObject NextButton;
    public Image LevelEndImage;
    public GameObject LevelCompletedHeader;
    public Image BlackBackGroundImage;
    public float BlackImageFadeValue;
    public TMP_Text levelcompletedtext;

    [Header("Star Animation")]
    public GameObject stars;
    public ParticleSystem[] starFX;
    public float starAppearDuration;

    [Header("Completed Texts")]
    public TMP_Text CompletedText;
    public string fullAccurateText;
    public string halfAccurateText;
    public string badAccurateText;

    [Header("Completed Status")]
    public bool fullAccurate;
    public bool halfAccurate;
    public bool badAccurate;
    public bool levelFailed;

    private void Start()
    {
        StartCoroutine(ShowEvidenceLevelCompletedUI());
    }
    IEnumerator ShowEvidenceLevelCompletedUI()
    {

       // levelcompletedtext.text = "Level " + PlayerPrefs.GetInt("level") + " Completed!";
        BlackBackGroundImage.DOFade(BlackImageFadeValue, 3);
        if (fullAccurate)
        {
            yield return StartCoroutine(StarAnimation(3));

            yield return new WaitForSecondsRealtime(starAppearDuration);
            CompletedText.DOText(fullAccurateText, 1f);
            CompletedText.gameObject.SetActive(true);
            StartCoroutine(UIManager.UI.FadeInOutObject(LevelCompletedHeader, 1, 2, true));
            LevelEndImage.DOFade(1, 1).OnComplete(() =>
            {
                StartCoroutine(UIManager.UI.FadeInOutObject(NextButton, 1, 1, true));


            });

        }
        else if (halfAccurate)
        {
            yield return StartCoroutine(StarAnimation(2));

            yield return new WaitForSecondsRealtime(starAppearDuration);
            CompletedText.DOText(halfAccurateText, 1f);
            CompletedText.gameObject.SetActive(true);
            StartCoroutine(UIManager.UI.FadeInOutObject(LevelCompletedHeader, 1, 2, true));

            LevelEndImage.DOFade(1, 1).OnComplete(() =>
            {
                StartCoroutine(UIManager.UI.FadeInOutObject(NextButton, 1, 1, true));

            });
        }
        else if (badAccurate)
        {
            yield return StartCoroutine(StarAnimation(1));
            yield return new WaitForSecondsRealtime(starAppearDuration);
            CompletedText.gameObject.SetActive(true);
            CompletedText.DOText(badAccurateText, 1f);
            StartCoroutine(UIManager.UI.FadeInOutObject(LevelCompletedHeader, 1, 2, true));
            LevelEndImage.DOFade(1, 1).OnComplete(() =>
            {
                StartCoroutine(UIManager.UI.FadeInOutObject(NextButton, 1, 1, true));

            });
        }

    }
    IEnumerator StarAnimation(int starCount)
    {
        for (int i = 0; i < starCount; i++)
        {
            yield return new WaitForSecondsRealtime(starAppearDuration);
            stars.transform.GetChild(i).gameObject.SetActive(true);
            stars.transform.GetChild(i).GetComponent<Image>().DOFade(1, 1);
            starFX[i].Play();

        }
    }
}
