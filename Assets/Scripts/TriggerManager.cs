
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class TriggerManager : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (this.CompareTag("Player") && other.CompareTag("FreeThickCriminal") || other.CompareTag("FreeSlimCriminal"))
        {
            EventManager.eventManager.OnCriminalTrigger(other.transform);
        }
        if(this.CompareTag("Player") && other.CompareTag("Obstacle"))
        {
            other.enabled = false;

            other.transform.DOShakePosition(.55f, .0725f, 10, 90);
            EventManager.eventManager.OnObstacleTrigger();
        }
        if(this.CompareTag("Player") && other.CompareTag("Air")){
            other.enabled = false;
            EventManager.eventManager.OnTriggerAirTile();
        }
        if (this.CompareTag("Player") && other.CompareTag("DroneTile"))
        {
            other.enabled = false;
            EventManager.eventManager.OnTriggerDroneTile();
        }
        if (this.CompareTag("Player") && other.CompareTag("Badge"))
        {
            EventManager.eventManager.OnBadgeTrigger();
            other.enabled = false;
            other.GetComponent<HoverScript>().enabled = false;
            DOTween.Kill(other.gameObject);
            Vector3 jumpPosition = new Vector3(this.transform.position.x + Random.Range(-5,5), this.transform.position.y+6, this.transform.position.z - 5);

            other.transform.DOJump(jumpPosition, 1, 15, 1);
        }
        if (this.CompareTag("Player") && other.CompareTag("Finish"))
        {
            EventManager.eventManager.OnFinishStarted();
        }
        if (this.CompareTag("Player") && other.CompareTag("Prison"))
        {
            EventManager.eventManager.OnPrisonReached();
        }
    }
}
