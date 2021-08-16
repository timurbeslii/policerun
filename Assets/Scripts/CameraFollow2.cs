using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;

public class CameraFollow2 : MonoBehaviour
{
    // camera will follow this object
    public Transform Target;
    //camera transform
  
    // offset between camera and target
    public Vector3 Offset;
    public Vector3 FinishOffset;
    // change this value to get desired smoothness
    public float SmoothTime = 0.3f;

    public bool isCamera;
    // This value will change at the runtime depending on target movement. Initialize with zero vector.
    private Vector3 velocity = Vector3.zero;
    bool isFinished;
    private void Start()
    {
        //  Offset = transform.position - Target.position;
        if (transform.CompareTag("MainCamera"))
        {
            //isCamera = true;
        }
    }

    private void LateUpdate()
    {
        if (isCamera)
        {
            // update position
            Vector3 targetPosition = Target.position + Offset;
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, SmoothTime);

            // update rotation
            if (isFinished)
            {
                transform.LookAt(Target);
            }
            
        }

    }
    public void GameFinishCam()
    {
        isFinished = true;
        SmoothTime = 0.125f;
        DOTween.To(() => Offset, x => Offset = x, FinishOffset, 2.5f).OnComplete(() =>
        {
            StartCoroutine(CamLeftRightLoop());

        });
    }
    IEnumerator CamLeftRightLoop()
    {
        Vector3 left = FinishOffset;
        left.x = -2;
        Vector3 right = FinishOffset;
        right.x = 2;
       yield return DOTween.To(() => Offset, x => Offset = x, left, 1.5f).WaitForCompletion();
       yield return DOTween.To(() => Offset, x => Offset = x, right, 1.5f).WaitForCompletion();
        StartCoroutine(CamLeftRightLoop());
    }
    private void FixedUpdate()
    {
        if (!isCamera)
        {
            // update position
            Vector3 targetPosition = Target.position + Offset;
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, SmoothTime);

            // update rotation
            // transform.LookAt(Target);
        }
    }
}