using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TutorialTouch : MonoBehaviour
{
    public Image image;
    public Sprite[] imageArray;
    public float loopTime = 1f;
    public bool disableOnTouch = true;
    private float waitTime;
    private int currentImage = 0;
    private bool x = false;
    void Start()
    {
        waitTime = loopTime / imageArray.Length;
        StartCoroutine(Play());
    }

    void Update()
    {
        if (disableOnTouch)
        {
            TouchControl();
        }
    }

    private void TouchControl()
    {
        if (Input.GetMouseButtonDown(0))
        {
            x = true;
            gameObject.SetActive(false);
        }
    }

    private IEnumerator Play()
    {
        while (!x)
        {
            
            image.sprite = imageArray[currentImage];
            currentImage++;
            if (currentImage >= imageArray.Length)
            {
                currentImage = 0;
            }
            yield return new WaitForSeconds(waitTime);
        }
    }
}
