using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TutorialScale : MonoBehaviour
{
    public float scaleDifference = 0.25f;
    public float loopTime = 1.5f;
    public Ease ease = Ease.InOutSine;
    public bool grow = true;
    public bool decrease = true;
    private Vector3[] scale = new Vector3[2];
    private Vector3 scaleVector;
    private int i = 0;
    void Start()
    {
        scaleVector = new Vector3(scaleDifference, scaleDifference, scaleDifference);
        if (grow && decrease)
        {
            i = 0;
            scale[0] = transform.localScale - scaleVector / 2;
            scale[1] = transform.localScale + scaleVector / 2;
        }
        else if (grow)
        {
            i = 0;
            scale[0] = transform.localScale;
            scale[1] = transform.localScale + scaleVector;
        }
        else if (decrease)
        {
            i = 1;
            scale[0] = transform.localScale - scaleVector;
            scale[1] = transform.localScale;
        }
        else
        {
            i = 0;
            scale[0] = transform.localScale;
            scale[1] = transform.localScale;
        }

        for (int i = 0; i < scale.Length; i++)
        {
            if (scale[i].x < 0)
            {
                scale[i] = Vector3.zero;
            }
        }

        transform.localScale = scale[i];

        Scale();
    }

    void Update()
    {
        
    }
    
    private void Scale()
    {
        if (i == 1)
        {
            i = 0;
        }
        else
        {
            i = 1;
        }

        transform.DOScale(scale[i], loopTime / 2).SetEase(ease).OnComplete(() =>
        {
            Scale();
        });
    }
}
