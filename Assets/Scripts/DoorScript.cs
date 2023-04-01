using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{

	public GameObject key;
	public Collider2D door;

	void FixedUpdate()
	{
		if(GameObject.Find("Key") == null)
		{
			door.isTrigger = true;
		}
		
	}

}
