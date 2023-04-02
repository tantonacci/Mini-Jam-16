using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeKeyScript : MonoBehaviour
{
	SpriteRenderer fakeKeySprite;
	[SerializeField] private GameObject exit;
	private SceneChange sceneChange;
	public AudioSource audioSource;
	public bool played;
	
    // Start is called before the first frame update
    void Start()
    {
        fakeKeySprite = GetComponent<SpriteRenderer>();
		fakeKeySprite.enabled = false;
		sceneChange = exit.GetComponent<SceneChange>();
		audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(GameObject.Find("Key") == null)
		{
			if(!audioSource.isPlaying && played == false)
			{
				audioSource.enabled = true;
				audioSource.Play();	
				played = true;
			}
			fakeKeySprite.enabled = true;
		}
		if(sceneChange.opening)
		{
			fakeKeySprite.enabled = false;
		}
    }
}
