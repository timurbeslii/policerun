using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomCriminal : MonoBehaviour
{
    [SerializeField] private GameObject[] CriminalTypes;
    void Start()
    {
        if (Random.Range(0, 5) >1 )
        {
            CriminalTypes[Random.Range(0, CriminalTypes.Length)].SetActive(true);
        }
       

    }

}
