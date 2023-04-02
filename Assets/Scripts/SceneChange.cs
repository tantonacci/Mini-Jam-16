using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
	public GameObject key;
	public Collider2D exit;
	[SerializeField] private Animator animator;
	private float animationTime;
	[SerializeField] private float animationDuration;
	public bool opening = false;
	
	void OnTriggerEnter2D(Collider2D col)
	{
		if(GameObject.Find("Key") == null)
		{
			animator.SetTrigger("OpenDoor");
			animationTime = Time.time + animationDuration;
			opening = true;
		}
		
	}
	void OnTriggerStay2D(Collider2D col)
	{
		if((GameObject.Find("Key") == null) && (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !animator.IsInTransition(0)))
		{
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
		}
	}
}