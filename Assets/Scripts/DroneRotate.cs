using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class DroneRotate : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.DOLocalRotate(new Vector3(0, 180, 0), 1500).SetLoops(-1, LoopType.Incremental).SetSpeedBased().SetEase(Ease.OutSine);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
