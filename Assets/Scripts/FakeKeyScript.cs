using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeKeyScript : MonoBehaviour
{
	SpriteRenderer fakeKeySprite;
	
    // Start is called before the first frame update
    void Start()
    {
        fakeKeySprite = GetComponent<SpriteRenderer>();
		fakeKeySprite.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(GameObject.Find("Key") == null)
		{
			fakeKeySprite.enabled = true;
		}
    }
}
