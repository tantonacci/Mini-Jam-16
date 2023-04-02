using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioScript : MonoBehaviour
{
	public AudioSource audioSource;
	
    // Start is called before the first frame update
    void Start()
    {
        audioSource.Play();
		DontDestroyOnLoad(gameObject);
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
