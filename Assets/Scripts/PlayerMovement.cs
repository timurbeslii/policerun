using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Animancer;
public class PlayerMovement : MonoBehaviour
{
    [Header("Animation Section")]
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private AnimationClip walkClip;
    [SerializeField] private AnimationClip stumbleClip;


    [Header("Movement")]
    public float playerSpeed;
    public bool released;


    private Vector3 currentPos, deltaPos, lastPos;
    public float horizontalLimit = 1.5f;
    private Touch touch;
    bool isDead,isStarted;

    public bool isOnDroneTile;
    public void StartWalking()
    {
        playerAnimator.Play("walk");
        isStarted = true;
    }
    public void PlayerCrashed()
    {
        playerAnimator.Play("stumble");
    }
    private void FixedUpdate()
    {
        if (!isDead && isStarted && !GameManager.gm.finished)
        {
            transform.Translate(playerSpeed * Vector3.forward * Time.deltaTime);
            Move();
            SetMouseDelta();
        }
        if (!isOnDroneTile)
        {
             transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.identity, 50f * Time.deltaTime);
        }
       
    }
    public void SpeedUpThePlayer()
    {
        DOTween.To(() => playerSpeed, x => playerSpeed = x, playerSpeed * 2, 10).OnComplete(() =>
        {
          

        });

    }
    public void SlowDownThePlayer()
    {
        DOTween.To(() => playerSpeed, x => playerSpeed = x, 0, 1).OnComplete(() =>
        {
            playerSpeed = 0;

        });

    }
    public void SlowDownAndDie()
    {
        isDead = true;
        SlowDownThePlayer();
        playerAnimator.Play("die");
        GameManager.gm.ShowLevelEndUI(false);
       
    }
    private void SetMouseDelta()
    {
        deltaPos = Vector3.zero;
        if (SystemInfo.deviceType == DeviceType.Handheld)
        {
            touch = Input.GetTouch(0);
            deltaPos = touch.deltaPosition;
        }
        else if (SystemInfo.deviceType == DeviceType.Desktop)
        {
            currentPos = Input.mousePosition;
            deltaPos = currentPos - lastPos;
            lastPos = currentPos;
        }
    }

    private void Move()
    {
        Vector3 movePos = Vector3.zero;
        float moveSpeed = 0.0075f;

        if (SystemInfo.deviceType == DeviceType.Desktop)
        {
            moveSpeed = 0.0075f * 561 / Screen.width;
        }

        movePos = Vector3.right * deltaPos.x * moveSpeed;

        Vector3 desiredPos = transform.localPosition + movePos;
        Vector3 rot= new Vector3(0, 0, 0);
        if (deltaPos.x > 0)
        {
            rot = new Vector3(0, 0, -1);
        }
        else if ( deltaPos.x<0)
        {
            rot = new Vector3(0, 0, 1);
        }
        Vector3 desiredRot = transform.localRotation.eulerAngles + rot;

        desiredPos.x = Mathf.Clamp(desiredPos.x, -horizontalLimit, horizontalLimit);
        desiredRot.z = ClampAngle(desiredRot.z, -25, 25);
        if ((SystemInfo.deviceType == DeviceType.Handheld) || Input.GetMouseButton(0))
        {
            if (isOnDroneTile)
            {
                transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(desiredRot), 50f * Time.deltaTime);
            }
           
            transform.localPosition = Vector3.Lerp(transform.localPosition, desiredPos, 250f * Time.deltaTime);
        }
        else if(!Input.GetMouseButton(0))
        {
           
                transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.identity, 50f * Time.deltaTime);
            
        }
    }
    public float ClampAngle(float angle, float min, float max)
    {
        angle = Mathf.Repeat(angle, 360);
        min = Mathf.Repeat(min, 360);
        max = Mathf.Repeat(max, 360);
        bool inverse = false;
        var tmin = min;
        var tangle = angle;
        if (min > 180)
        {
            inverse = !inverse;
            tmin -= 180;
        }
        if (angle > 180)
        {
            inverse = !inverse;
            tangle -= 180;
        }
        var result = !inverse ? tangle > tmin : tangle < tmin;
        if (!result)
            angle = min;

        inverse = false;
        tangle = angle;
        var tmax = max;
        if (angle > 180)
        {
            inverse = !inverse;
            tangle -= 180;
        }
        if (max > 180)
        {
            inverse = !inverse;
            tmax -= 180;
        }

        result = !inverse ? tangle < tmax : tangle > tmax;
        if (!result)
            angle = max;
        return angle;
    }
}
