using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{

    public static UIManager UI;
    [Header("Level UI's")]
    public GameObject LevelCompletedUI;
    private void Awake()
    {
        if (UI == null)
        {
            UI = this;
        }
    }

    public IEnumerator FadeInOutObject(GameObject obj, float fadeValue, float duration, bool isChild)
    {
        for (int i = 0; i < obj.transform.childCount; i++)
        {
            if (obj.transform.GetChild(i).gameObject.hasComponent<Image>())
            {
                obj.transform.GetChild(i).gameObject.GetComponent<Image>().DOFade(fadeValue, duration);
            }
            if (obj.transform.GetChild(i).gameObject.hasComponent<TMP_Text>())
            {
                obj.transform.GetChild(i).gameObject.GetComponent<TMP_Text>().DOFade(fadeValue, duration);
            }
        }

        yield return new WaitForSecondsRealtime(duration);
        if (obj.GetComponentInChildren<Button>())
        {
            obj.GetComponentInChildren<Button>().enabled = true;
        }
    }
    public IEnumerator FadeInOutObject(GameObject obj, float fadeValue, float duration)
    {
        if (obj.hasComponent<Image>())
        {
            obj.GetComponent<Image>().DOFade(fadeValue, duration);
        }
        if (obj.hasComponent<TMP_Text>())
        {
            obj.GetComponent<TMP_Text>().DOFade(fadeValue, duration);
        }
        yield return new WaitForSecondsRealtime(duration);
    }
    public IEnumerator FadeInOutObject(GameObject[] objArray, float fadeValue, float duration)
    {
        foreach (GameObject obj in objArray)
        {
            if (obj.hasComponent<Image>())
            {
                obj.GetComponent<Image>().DOFade(fadeValue, duration);
            }
            if (obj.hasComponent<TMP_Text>())
            {
                obj.GetComponent<TMP_Text>().DOFade(fadeValue, duration);
            }
        }
        yield return new WaitForSecondsRealtime(duration);
    }

    public void ShowLevelCompletedUI()
    {
        LevelCompletedUI.SetActive(true);
    }

}
