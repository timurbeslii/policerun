using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TutorialSlide : MonoBehaviour
{
    public Transform slideObject;
    public Transform[] borders = new Transform[2];
    public int firstPosBorderIndex = 0;
    public float loopTime = 1.5f;
    public Ease ease = Ease.Linear;
    public bool oneWay = false;
    public bool disableOnSlide = true;
    public bool disableOnSwipe = false;
    public float slideDistance = 300f;
    private int i = 1;
    private Vector2 firstTouchPos = Vector2.zero, currentTouchPos = Vector2.zero;
    private float holdTime;
    
    Tween slide;
    void Start()
    {
        SetStartPos();
        Move();
    }

    void Update()
    {
        if (disableOnSlide || disableOnSwipe)
        {
            SlideSwipeControl();
        }
    }

    private void SetStartPos()
    {
        slideObject.localPosition = new Vector3(borders[firstPosBorderIndex].localPosition.x, slideObject.localPosition.y, slideObject.localPosition.z);
    }

    private void SlideSwipeControl()
    {
        if (Input.GetMouseButtonDown(0))
        {
            holdTime = 0f;
            firstTouchPos = Input.mousePosition;
            currentTouchPos = firstTouchPos;
            if (disableOnSlide)
            {
                slide.Kill();
            }
        }
        else if (Input.GetMouseButton(0))
        {
            holdTime += Time.deltaTime;
            currentTouchPos = Input.mousePosition;
        }

        if (Vector2.Distance(firstTouchPos, currentTouchPos) > slideDistance)
        {
            if (disableOnSlide)
            {
                gameObject.SetActive(false);
            }
            else if (Input.GetMouseButtonUp(0) && holdTime < .2f)
            {
                slide.Kill();
                gameObject.SetActive(false);
            }
        }
    }

    private void Move()
    {
        slide = slideObject.DOLocalMoveX(borders[i].localPosition.x, loopTime / 2f).SetEase(ease).OnComplete(() =>
        {
            if (oneWay)
            {
                SetStartPos();
            }

            if (i == 0)
            {
                i = 1;
            }
            else
            {
                i = 0;
            }
            Move();
        });
    }
}
