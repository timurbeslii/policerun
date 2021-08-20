using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomCriminal : MonoBehaviour
{
    [SerializeField] private GameObject[] CriminalTypes;
    
    void Start()
    {
        if (Random.Range(0, 5) > 2)
        {
            int index = Random.Range(0, CriminalTypes.Length);
            CriminalTypes[index].SetActive(true);
            CriminalTypes[index].transform.GetChild(1).GetComponent<SkinnedMeshRenderer>().sharedMaterial= GameManager.gm.levels[PlayerPrefs.GetInt("levelIndex")].thiefMat;
        }
       

    }

}
