using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyScript : MonoBehaviour
{
	// Called when the key collides with another object
    void OnTriggerEnter2D(Collider2D col)
    {
		if (col.CompareTag("Player")) {
			Destroy(gameObject);
		}
    }
}
