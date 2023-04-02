using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlowObjectScript : MonoBehaviour
{

	[SerializeField] private Rigidbody2D rbody;

    // Start is called before the first frame update
    void Start()
    {
        rbody = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {	
		if(inFanArea)
		{
			rbody.AddForce(fanArea.GetComponent<FanBlower>().direction * fanArea.GetComponent<FanBlower>().strength);
		}
    }
	
	public bool inFanArea = false;
	public GameObject fanArea;
	
	void OnTriggerEnter2D(Collider2D col)
	{
		if(col.gameObject.tag == "wind")
		{
			fanArea = col.gameObject;
			inFanArea = true;
		}
	}
	void OnTriggerExit2D(Collider2D col)
	{
		if(col.gameObject.tag == "wind")
		{
			inFanArea = false;
		}
	}
}
