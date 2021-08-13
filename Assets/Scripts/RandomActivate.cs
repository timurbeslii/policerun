using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomActivate : MonoBehaviour
{
   
    void Start()
    {
        int num = Random.Range(0, 100);
        if (num>20)
        {
            this.gameObject.SetActive(false);
        }
    }

}
