using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour 
{		
	public GameObject rocket;	
	public AudioClip deathSound;
	public AudioClip fireSound;
	public AudioClip fireSound2;
	public AudioClip fireSound3;
	public AudioClip fireSound4;
	public AudioClip fireSound5;
	
	int mode = 1;
	float time = 0;
	
	Vector3 vel;
	Vector3 vel2;
	
	System.Random random;
	
	protected int groundMask = 1 << 9 | 1 << 8; // Ground, Block
	
	float halfX = 1.5f;
	float halfY = 1f;
	
	float startY;
	
	bool dead = false;
	
	public virtual void Awake()
	{
		startY = transform.position.y;
	}
	
	// Update is called once per frame
	public void Update() 
	{	
		time += Time.deltaTime;
		if (time > 1)
        {
			mode = 1 - mode;
            time = 0;
			
			if(mode == 0)
			{
				ShootRocket();
			}
        }
		
		vel = new Vector3(3, 0, 0);		
		vel2 = vel * Time.deltaTime;
		
		float yDiff = startY + (Global.GameOver ? 0 : (mode == 0 ? (Global.HardcoreMode ? -2f : -1.5f) : 0));
		float xDiff = Global.GameOver ? 0 : vel2.x;
		
		if(dead)
			yDiff = transform.position.y - 0.05f;
		
		transform.position = new Vector3(transform.position.x + xDiff, yDiff, transform.position.z);
		
		UpdateRaycasts();
	}
	
	void OnTriggerEnter(Collider other) 
	{
		if(other.gameObject.layer == 10)
		{
			Rocket rocketScript = other.GetComponent<Rocket>();
			if(rocketScript.destroyInvader)
			{
				Camera.main.audio.PlayOneShot(deathSound);
				rocketScript.Explode(other.gameObject.rigidbody.position);
				
				this.GetComponent<OTAnimatingSprite>().looping = false;
				dead = true;
			}
		}
    }
	
	void ShootRocket()
	{
		if(Global.GameOver)
			return;
		
		GameObject rocketClone = (GameObject)Instantiate(rocket, transform.position + new Vector3(halfX, 0, 0), Quaternion.identity);
		rocketClone.transform.parent = transform;
		OTAnimatingSprite sprite = rocketClone.GetComponent<OTAnimatingSprite>();
		sprite.onOutOfView = DestroyWhenOutOfView;	
		rocketClone.rigidbody.velocity = transform.right * 50;
		
		if(random == null)
		{
			random = new System.Random();
		}
		
		var randNum = random.Next(5);
		if(randNum == 0)			
		{
			Camera.main.audio.PlayOneShot(fireSound);
		}
		else if(randNum == 1)
		{
			Camera.main.audio.PlayOneShot(fireSound2);
		}
		else if(randNum == 2)
		{
			Camera.main.audio.PlayOneShot(fireSound3);
		}
		else if(randNum == 2)
		{
			Camera.main.audio.PlayOneShot(fireSound4);
		}
		else
		{
			Camera.main.audio.PlayOneShot(fireSound5);
		}
	}

	void DestroyWhenOutOfView(OTObject owner)
    {
        OT.DestroyObject(owner);
    }
	
	void UpdateRaycasts()
	{		
		RaycastHit hitInfo;
		
		if (Physics.Raycast(new Vector3(transform.position.x + halfX,transform.position.y - halfY, 0), Vector3.right, out hitInfo, 1f, groundMask))
		{
			hitInfo.rigidbody.AddForce(10, 0, 0, ForceMode.Impulse);
		}
		
		if (Physics.Raycast(new Vector3(transform.position.x + halfX,transform.position.y, 0), Vector3.right, out hitInfo, 1f, groundMask))
		{
			hitInfo.rigidbody.AddForce(10, 0, 0, ForceMode.Impulse);;
		}
		
		if (Physics.Raycast(new Vector3(transform.position.x + halfX,transform.position.y + halfY, 0), Vector3.right, out hitInfo, 1f, groundMask))
		{
			hitInfo.rigidbody.AddForce(10, 0, 0, ForceMode.Impulse);
		}
	}
}