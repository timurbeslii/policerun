using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class HoverScript : MonoBehaviour
{
    
    void Start()
    {
       
        transform.DOLocalRotate(new Vector3(0,180,0), 100).SetLoops(-1, LoopType.Incremental).SetSpeedBased().SetEase(Ease.Linear);
        StartCoroutine(AngelAnim());
    }
    IEnumerator AngelAnim()
    {
        yield return transform.DOMoveY(transform.position.y + 0.125f, 1).SetEase(Ease.InSine).WaitForCompletion();
        yield return transform.DOMoveY(transform.position.y - 0.125f, 1).SetEase(Ease.InSine).WaitForCompletion();
        StartCoroutine(AngelAnim());
    }
  
}
