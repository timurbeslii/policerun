using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoConnectJoint : MonoBehaviour
{
	void Awake()
	{
		if (this.gameObject.hasComponent<CharacterJoint>())
		{
			GetComponent<CharacterJoint>().connectedBody = transform.parent.GetComponent<Rigidbody>();
		}
		else
		{
			GetComponent<FixedJoint>().connectedBody = transform.parent.GetComponent<Rigidbody>();
		}
		
	}
}
