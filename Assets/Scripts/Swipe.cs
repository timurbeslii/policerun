using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Swipe : MonoBehaviour
{
    public bool tap, swipeLeft, swipeRight, swipeUp, swipeDown, isDragging, isHolding;
    public Vector2 startTouch, swipeDelta;
    public static Swipe swipeManager;
    public bool disableHold, disable, disableLeft, disableRight, holdcheck;

    public int holdCheckIterator;
    public float waitEachCheck;


    private void Awake()
    {

        if (!swipeManager)
        {
            swipeManager = this;
            disable = false;
            //   DontDestroyOnLoad(this);
        }
    }
    //public void increasecheckbutton()
    //{
    //    holdCheckIterator++;
    //    iteratorText.text = "Check Iterator:" + holdCheckIterator;
    //}
    //public void decreasecheckbutton()
    //{
    //    holdCheckIterator--;
    //    iteratorText.text = "Check Iterator:" + holdCheckIterator;
    //}
    IEnumerator checkHold()
    {
        int i;
        for (i = 0; i < holdCheckIterator; i++)
        {
            yield return new WaitForSeconds(waitEachCheck);
            if (!holdcheck)
            {
                StopCoroutine(checkHold());
                break;
            }
            if (!isDragging)
            {
                break;
            }
        }
        if (i == holdCheckIterator)
        {
            Debug.Log(i);
            isHolding = true;
        }
        else
        {
            isHolding = false;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            holdcheck = true;
        }
        else
        {
            holdcheck = false;
        }

        tap = swipeLeft = swipeRight = swipeUp = swipeDown = false;
        if (Input.GetMouseButtonDown(0))
        {
            if (!disableHold)
            {
                StartCoroutine(checkHold());
            }
            isDragging = true;
            tap = true;
            startTouch = Input.mousePosition;

        }
        else if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
            isHolding = false;
            Reset();
            StopCoroutine(checkHold());
        }

        if (Input.touchCount > 0)
        {
            if (Input.touches[0].phase == TouchPhase.Began)
            {
                isDragging = true;
                tap = true;
                startTouch = Input.touches[0].position;
            }
            else if (Input.touches[0].phase == TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Canceled)
            {

                isDragging = false;
                Reset();
            }
        }

        swipeDelta = Vector2.zero;
        if (isDragging)
        {
            if (Input.touches.Length > 0)
                swipeDelta = Input.touches[0].position - startTouch;
            else if (Input.GetMouseButton(0))
                swipeDelta = (Vector2)Input.mousePosition - startTouch;
        }

        if (Mathf.Abs(swipeDelta.x) > 100 || Mathf.Abs(swipeDelta.y) > 100)
        {
            float x = swipeDelta.x;
            float y = swipeDelta.y;
            if (Mathf.Abs(x) > Mathf.Abs(y))
            {
                if (x < 0)
                {
                    if (!disableLeft)
                        swipeLeft = true;
                }
                else
                {
                    if (!disableRight)
                        swipeRight = true;
                }
            }
            else
            {
                if (y < 0)
                    swipeDown = true;
                else
                    swipeUp = true;
            }
            // Reset();
        }

    }


    public void Reset()
    {
        startTouch = swipeDelta = Vector2.zero;
        tap = swipeLeft = swipeRight = swipeUp = swipeDown = false;
        StopCoroutine(checkHold());

    }
}
