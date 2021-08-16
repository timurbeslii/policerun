using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CableLineCodes : MonoBehaviour
{
    public Transform[] linePos = new Transform[2];

    public float length;
    //public CapsuleCollider capsuleCollider;

    private void Start()
    {
        /*transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(1).gameObject.SetActive(false);

        Invoke("Active", 0.1f);*/
    }
    private void Update()
    {
        //linePos[0].rotation = Quaternion.LookRotation(linePos[1].forward, linePos[1].up);
        
        /*linePos[0].LookAt(linePos[1], Vector3.up);
        Vector3 line0Rot = linePos[0].rotation.eulerAngles;
        line0Rot.x += 90f;
        linePos[0].rotation = Quaternion.Euler(line0Rot);

        Vector2 pos0 = new Vector2(linePos[0].position.x, linePos[0].position.y);
        Vector2 pos1 = new Vector2(linePos[1].position.x, linePos[1].position.y);
        length = Vector2.Distance(pos0, pos1);*/

        //capsuleCollider.height = length;
        //capsuleCollider.center = new Vector3(0, length / 2, 0);
    }
    public void SetPosition(int index, Vector3 position)
    {
        linePos[index].position = position;
    }

    public Vector3 GetPosition(int index)
    {
        return linePos[index].position;
    }

    void Active()
    {
        linePos[0].gameObject.SetActive(true);
        linePos[1].gameObject.SetActive(true);
    }

}
