using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonRaycaster : MonoBehaviour
{

    public bool buttonPressed;
    public bool drawDebugRaycasts = false;
    public LayerMask groundLayer;

    private float groundCheck = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // FixedUpdate is called once per Tick
    void FixedUpdate()
    {
        buttonPressed = Raycast(new Vector2(0, 0), Vector2.up, groundCheck);
    }

#region "Raycast functions"

    RaycastHit2D Raycast(Vector2 offset, Vector2 rayDirection, float length) {
		//Call the overloaded Raycast() method using the ground layermask and return 
		//the results
		return Raycast(offset, rayDirection, length, groundLayer);
	}

	RaycastHit2D Raycast(Vector2 offset, Vector2 rayDirection, float length, LayerMask mask) {
		//Record the player's position
		Vector2 pos = transform.position;

		//Send out the desired raycasr and record the result
		RaycastHit2D hit = Physics2D.Raycast(pos + offset, rayDirection, length, mask);

		//If we want to show debug raycasts in the scene...
		if (drawDebugRaycasts)
		{
			//...determine the color based on if the raycast hit...
			Color color = hit ? Color.red : Color.green;
			//...and draw the ray in the scene view
			Debug.DrawRay(pos + offset, rayDirection * length, color);
		}

		//Return the results of the raycast
		return hit;
	}

#endregion

}
