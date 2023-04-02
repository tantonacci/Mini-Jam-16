using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
	public GameObject key;
	public Collider2D exit;
/*
	void Update()
	{
		if(GameObject.Find("Key") == null)
		{
			exit.isTrigger = true;
		}
		
	}
	}*/
	
	void OnTriggerEnter2D(Collider2D col)
	{
		if(GameObject.Find("Key") == null)
		{
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
		}
		
	}
}
