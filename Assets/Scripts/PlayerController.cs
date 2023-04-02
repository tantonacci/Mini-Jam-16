using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

#region "variables"
    public bool drawDebugRaycasts = false;

    //Constants
    public float speed = 6f;
    public float maxAccel = 0.8f;

    [SerializeField] private float footOffset = .25f;
    [SerializeField] private float vFootOffset = -0.9f;
    private float groundCheck = 0.1f;





    //Physics Variables
    public float jumpForce;             //Initial jump force
	public float jumpHoldForce;         //Incremental force when jump is held
    public float doubleJumpForce;       //Initial jump force for a mid-air jump

    private float coyoteTime;
    [SerializeField] private float coyoteDuration;
    private bool onGround = true;

    private float gravity;
    private float drag;

    //Movement Variables
    public float jumpHoldDuration;      //How long the jump key can be held
    private float numJumpsUsed = 0;     //Counts how many jumps have been used since ground was last touched

    private bool isJumping = false;
    private float jumpTime;             //Variable to hold max jump duration

    //Animation Variables
    private float originalXScale;
    private int direction = 1;

    //GameState Variables
    public bool alive = true;
    public bool dying = false;
    

    private PlayerInput input;
    public LayerMask groundLayer;
    public LayerMask movableLayer;
    public LayerMask buttontopLayer;
    public LayerMask buttonbtmLayer;
    //public LayerMask enemyLayer;

    [SerializeField] private Rigidbody2D rbody;
    [SerializeField] private Animator animator;

#endregion

#region "Start and Update"

    // Start is called before the first frame update
    void Start()
    {
        input = GetComponent<PlayerInput>();
        rbody = gameObject.GetComponent<Rigidbody2D>();
        originalXScale = transform.localScale.x;

        //currentLevel.SetPlayer(gameObject);
        //currentLevel.FirstTimeSetup();

        gravity = rbody.gravityScale;
        drag = rbody.drag;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        if (alive) {
            PhysicsCheck();

            //if (!isDashing()) {
                //if (dashing)
                //    EndDash();

            GroundMovement();
            midAirMovement();
                
                //handleAbilities();
            //}
        } else if (dying) {
            playerDeath();
        }
    }

#endregion

#region "Physics Functions"

    void PhysicsCheck() {
        RaycastHit2D leftCheck  = Raycast(new Vector2(- footOffset, vFootOffset), Vector2.down, groundCheck);
        RaycastHit2D rightCheck = Raycast(new Vector2(  footOffset, vFootOffset), Vector2.down, groundCheck);

        if (leftCheck || rightCheck) {
            if (!onGround) {
                //usedDoubleJump = false;
                OnLand();
            }
            SetGrounded(true);
        } else {
            SetGrounded(false);
        }
    }

#endregion

#region "GameState Functions

    void playerDeath() {
        //Run death animation
        OnDeath();

        rbody.bodyType = RigidbodyType2D.Static;
        dying = false;

        //StartCoroutine(ResetLevel());
    }

    private IEnumerator ResetLevel()
    {
        yield return new WaitForSeconds(3);
        OnRevive(); 
    }

    private void SetGrounded(bool val) {
        onGround = val;
        SendAnimation("Grounded", val);
    }

    private void OnJump() {
        isJumping = true;
        SendAnimation("Jump");
    }

    private void OnLand() {
        SendAnimation("Land");
    }

    private void OnDeath() {
        alive = false;
        SendAnimation("Death");
    }

    private void OnRevive() {
        alive = true;
        //animator.Play("Base Layer.player_idle");
        SendAnimation("Revive");
        rbody.bodyType = RigidbodyType2D.Dynamic;
        //currentLevel.Setup();
    }

#endregion

#region "Movement"

    void GroundMovement() {
        SendAnimation("Run", input.horizontal != 0);

        float xVelGoal = speed * input.horizontal;

        float accel = Mathf.Clamp(xVelGoal - rbody.velocity.x, -maxAccel, maxAccel); 

        rbody.velocity = new Vector2(rbody.velocity.x + accel, rbody.velocity.y);

        if ( (xVelGoal * direction) < 0f) {
			FlipCharacterDirection();
        }

		//If the player is on the ground, extend the coyote time window
		if (onGround) {
			coyoteTime = Time.time + coyoteDuration;
        }
    }

    void midAirMovement() {
        if (input.jumpPressed && !isJumping && (onGround || coyoteTime > Time.time)) {
            //momentum.y = speed * Input.GetAxis("Vertical");
            rbody.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);

            jumpTime = Time.time + jumpHoldDuration;

            OnJump();
            
        } else if (isJumping) {
			//...and the jump button is held, apply an incremental force to the rigidbody...
			if (input.jumpHeld)
				rbody.AddForce(new Vector2(0f, jumpHoldForce), ForceMode2D.Impulse);

			//...and if jump time is past, set isJumping to false
			if (jumpTime <= Time.time)
				isJumping = false;
		} 
        /* //DoubleJump Code
        else if (input.jumpPressed && !isJumping && !usedDoubleJump && inventory.Remove(Item.Doublejump)) {
            rbody.velocity = new Vector2(rbody.velocity.x, 0);
            rbody.AddForce(new Vector2(0f, doubleJumpForce), ForceMode2D.Impulse);
            usedDoubleJump = true;

            Vector3 pos = new Vector3(transform.position.x, transform.position.y - 1f, transform.position.z);
            Instantiate(fallingBookPrefab, pos, Quaternion.identity);

            OnJump();
        }*/
    }

#endregion

#region "Raycast functions"

    RaycastHit2D Raycast(Vector2 offset, Vector2 rayDirection, float length) {
		//Call the overloaded Raycast() method using the ground layermask and return 
		//the results
        LayerMask standingLayer = groundLayer | movableLayer | buttontopLayer | buttonbtmLayer;

		return Raycast(offset, rayDirection, length, standingLayer);
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

#region "Animation"

    void FlipCharacterDirection() {
		//Turn the character by flipping the direction
		direction *= -1;

		//Record the current scale
		Vector3 scale = transform.localScale;

		//Set the X scale to be the original times the direction
		scale.x = originalXScale * direction;

		//Apply the new scale
		transform.localScale = scale;
	}

    void SendAnimation(string anim) {
        animator.SetTrigger(anim);
    }

    void SendAnimation(string anim, bool val) {
        animator.SetBool(anim, val);
    }

#endregion

}