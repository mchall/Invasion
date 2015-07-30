using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour 
{
	[HideInInspector] public enum facing { Right, Left }
	[HideInInspector] public facing facingDir;
	
	[HideInInspector] public bool isLeft;
	[HideInInspector] public bool isRight;
	[HideInInspector] public bool isJump;
	
	[HideInInspector] public bool jumping = false;
	[HideInInspector] public bool grounded = false;
	
	[HideInInspector] public bool blockedRight;
	[HideInInspector] public bool blockedLeft;
	[HideInInspector] public bool blockedUp;
	[HideInInspector] public bool blockedDown;
	
	[HideInInspector] public bool alive = true;
	
	public GUIText dieText;
	
	public bool immortal = false;
	
	protected Transform thisTransform;
	
	private float moveVel;
	private float runVel = 4f;
	private Vector3 vel2;
	private Vector3 vel;
	
	private float jumpVel = 16f;
	private float jump2Vel = 14f;
	private float fallVel = 18f;
	
	private int jumps = 0;
    private int maxJumps = 2; // set to 2 for double jump
	
	private float gravityY;
	private float maxVelY = 0f;
		
	private RaycastHit hitInfo;
	private float halfMyX = 0.325f; 
	private float halfMyY = 0.34375f;
	[HideInInspector] public float rayDistUp = 0.375f;
	
	private float absVel2X;
	private float absVel2Y;
	
	// layer masks
	protected int groundMask = 1 << 9 | 1 << 8; // Ground, Block, Rocket
	protected int blockMask = 1 << 8; //Block
	protected int rocketMask = 1 << 10; //Rocket
	
	private float deathVelocity = 3f;
	
	protected bool killedByLeftScreen = true;
		
	public virtual void Awake()
	{
		thisTransform = transform;
	}
	
	// Use this for initialization
	public virtual void Start () 
	{
		moveVel = runVel;
		maxVelY = fallVel;
		vel.y = 0;
		StartCoroutine(StartGravity());
	}
	
	protected virtual void Die()
	{
		if(!immortal && alive)
		{
			dieText.text = "DEATH\n\nInsert COIN and\npress 'R' to reset level \nor 'ESC' to leave";
			
			Global.GameOver = true;
			alive = false;
		}
	}
	
	IEnumerator StartGravity()
	{
		// wait for things to settle before applying gravity
		yield return new WaitForSeconds(0.1f);
		gravityY = 52f;
	}
	
	// Update is called once per frame
	public virtual void UpdateMovement() 
	{
		vel.x = 0;
		
		// pressed right button
		if(isRight == true)
		{
			vel.x = moveVel;
		}
		
		// pressed left button
		if(isLeft == true)
		{			
			vel.x = -moveVel;
		}
		
		// pressed jump button
		if (isJump == true)
		{
			if (jumps < maxJumps)
		    {
				jumps += 1;
				jumping = true;
				if(jumps == 1)
				{
					vel.y = jumpVel;
				}
				if(jumps == 2)
				{
					vel.y = jump2Vel;
				}
		    }
		}
		
		// landed from fall/jump
		if(grounded == true && vel.y == 0)
		{
			jumping = false;
			jumps = 0;
		}
		
		UpdateRaycasts();
		
		// apply gravity while airborne
		if(grounded == false)
		{
			vel.y -= gravityY * Time.deltaTime;
		}
		
		// velocity limiter
		if(vel.y < -maxVelY)
		{
			vel.y = -maxVelY;
		}
		
		// apply movement 
		vel2 = vel * Time.deltaTime;
		thisTransform.position += new Vector3(vel2.x,vel2.y,0f);
	}
	
	// ============================== RAYCASTS ============================== 
	
	private bool CheckDown()
	{
		if (Physics.Raycast(new Vector3(thisTransform.position.x-0.25f,thisTransform.position.y,thisTransform.position.z+1f), Vector3.down, out hitInfo, 0.6f+absVel2Y, groundMask) 
			|| Physics.Raycast(new Vector3(thisTransform.position.x,thisTransform.position.y,thisTransform.position.z+1f), Vector3.down, out hitInfo, 0.6f+absVel2Y, groundMask)
			|| Physics.Raycast(new Vector3(thisTransform.position.x+0.25f,thisTransform.position.y,thisTransform.position.z+1f), Vector3.down, out hitInfo, 0.6f+absVel2Y, groundMask))
		{	
			return true;
		}
		return false;
	}
	
	private bool CheckUp()
	{
		if (Physics.Raycast(new Vector3(thisTransform.position.x-(halfMyX/2),thisTransform.position.y,thisTransform.position.z+1f), Vector3.up, out hitInfo, rayDistUp+absVel2Y, groundMask)
			|| Physics.Raycast(new Vector3(thisTransform.position.x+(halfMyX/2),thisTransform.position.y,thisTransform.position.z+1f), Vector3.up, out hitInfo, rayDistUp+absVel2Y, groundMask))
		{	
			return true;
		}
		return false;
	}
	
	private bool CheckRight()
	{
		if (Physics.Raycast(new Vector3(thisTransform.position.x,thisTransform.position.y,thisTransform.position.z+1f), Vector3.right, out hitInfo, halfMyX+absVel2X, groundMask)
			|| Physics.Raycast(new Vector3(thisTransform.position.x,thisTransform.position.y,thisTransform.position.z), Vector3.right, out hitInfo, halfMyX+absVel2X, groundMask))
		{	
			return true;
		}
		return false;
	}
	
	private bool CheckLeft()
	{
		if (Physics.Raycast(new Vector3(thisTransform.position.x,thisTransform.position.y,thisTransform.position.z+1f), Vector3.left, out hitInfo, halfMyX+absVel2X, groundMask)
			|| Physics.Raycast(new Vector3(thisTransform.position.x,thisTransform.position.y,thisTransform.position.z), Vector3.left, out hitInfo, halfMyX+absVel2X, groundMask))
		{	
			return true;
		}
		return false;
	}
	
	void UpdateRaycasts()
	{
		blockedRight = false;
		blockedLeft = false;
		blockedUp = false;
		blockedDown = false;
		grounded = false;		
		
		absVel2X = Mathf.Abs(vel2.x);
		absVel2Y = Mathf.Abs(vel2.y);
		
		if (CheckDown())
		{	
			if(vel.y <= 0)
			{
				grounded = true;
				vel.y = 0f; // stop falling			
				thisTransform.position = new Vector3(thisTransform.position.x,hitInfo.point.y+halfMyY,0f);
			}
		}
		
		// blocked up
		if (CheckUp())
		{			
			if(hitInfo.rigidbody.velocity.magnitude > deathVelocity)				
			{
				Die();
			}
			BlockedUp();
		}
		
		// blocked on right
		if (CheckRight())
		{	
			if(hitInfo.rigidbody.velocity.magnitude > deathVelocity)				
			{
				Die();
			}
			BlockedRight();
		}
		
		// blocked on left
		if(CheckLeft())
		{
			if(hitInfo.rigidbody.velocity.magnitude > deathVelocity)				
			{
				Die();
			}
			BlockedLeft();
		}		
		
		// Right edge of screen
		if (thisTransform.position.x + halfMyX > (Camera.main.transform.position.x + (6.25 * 4/3))) 
		{
			BlockedRight();
		}
		
		// Left edge of screen
		if (thisTransform.position.x - halfMyX < (Camera.main.transform.position.x - (6.25 * 4/3))) 
		{
			if(killedByLeftScreen)
				Die();
		}
	}	
	
	void OnTriggerEnter(Collider other) 
	{
		if(other.gameObject.layer == 9)
		{
			if(other.GetComponent<Rigidbody>().velocity.magnitude > deathVelocity)
			{
				if(CheckLeft() || CheckRight() || CheckUp())
				{
					Die();
				}
			}
		}
		
		if(other.gameObject.layer == 10)
		{
			if(other.GetComponent<Rigidbody>().velocity.magnitude > deathVelocity)
			{
				Die();
			}
		}
		
		if(other.gameObject.tag == "Explosion")
		{
			Die();
		}
    }
	
	void BlockedUp()
	{
		if(vel.y > 0)
		{
			vel.y = 0f;
			blockedUp = true;
		}
	}

	void BlockedRight()
	{
		if(facingDir == facing.Right)
		{
			blockedRight = true;
			vel.x = 0f;
			thisTransform.position = new Vector3(hitInfo.point.x-(halfMyX-0.01f),thisTransform.position.y, 0f); // .01 less than collision width.
		}
	}
	
	void BlockedLeft()
	{
		if(facingDir == facing.Left)
		{
			blockedLeft = true;
			vel.x = 0f;
			thisTransform.position = new Vector3(hitInfo.point.x+(halfMyX-0.01f),thisTransform.position.y, 0f); // .01 less than collision width.
		}
	}
}
