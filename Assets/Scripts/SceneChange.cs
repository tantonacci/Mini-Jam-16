using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
	public GameObject key;
	public Collider2D exit;
	[SerializeField] private Animator animator;

	
	void OnTriggerEnter2D(Collider2D col)
	{
		if(GameObject.Find("Key") == null)
		{
			animator.SetTrigger("OpenDoor");
			//SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
		}
		
	}
}
