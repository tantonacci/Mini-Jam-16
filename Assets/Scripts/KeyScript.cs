using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyScript : MonoBehaviour
{

	public LayerMask playerLayer;

	// Called when the key collides with another object
    void OnTriggerEnter2D(Collider2D col)
    {



		//PlayerController player = col.GetComponent<PlayerController>();
		if (col.CompareTag("Player")) {
			Destroy(gameObject);
		}
		
		//if(player != null)
		//{
			//put the destroy here if it works
		//}
    }
}
