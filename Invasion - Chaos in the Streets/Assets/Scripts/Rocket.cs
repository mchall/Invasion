using UnityEngine;
using System.Collections;

public class Rocket : MonoBehaviour 
{
	public GameObject fireObject;
	public GameObject explosionObject;
	
	public AudioClip explosionSound;
	public AudioClip explosionSound2;
	public AudioClip explosionSound3;
	
	int groundMask = 1 << 9 | 1 << 8; // Ground, Block
	
	float halfX = 0.5f;
	
	float explosiveRadius = Global.HardcoreMode ? 2.5f : 2f;
    float explosivePower = 15f;
	
	GameObject _tempFireObject;
	Vector3 explosionPos;
	
	public bool destroyInvader = false;

	// Update is called once per frame
	public void Update () 
	{		
		UpdateRaycasts();
	}
	
	void UpdateRaycasts()
	{		
		RaycastHit hitInfo;				
		if (Physics.Raycast(new Vector3(transform.position.x + halfX,transform.position.y, 0), Vector3.right, out hitInfo, 1f, groundMask))
		{			
			Explode(hitInfo.point);
		}
	}
	
	public void Explode(Vector3 pos)
	{
		explosionPos = pos;
		
		System.Random random = new System.Random();
		var randNum = random.Next(3);
		if(randNum == 0)
		{
			Camera.main.GetComponent<AudioSource>().PlayOneShot(explosionSound);
		}
		else if(randNum == 1)
		{
			Camera.main.GetComponent<AudioSource>().PlayOneShot(explosionSound2);
		}
		else
		{
			Camera.main.GetComponent<AudioSource>().PlayOneShot(explosionSound3);
		}
		
		//Create explosion
		GameObject explosion = (GameObject)Instantiate(explosionObject, explosionPos, Quaternion.identity);
		Destroy(explosion, 0.7f);
		
        Collider[] colliders = Physics.OverlapSphere(explosionPos, explosiveRadius);
        foreach (Collider hit in colliders) 
		{
			hit.GetComponent<Rigidbody>().AddForce(explosivePower, 0, 0, ForceMode.Impulse);
			
			if(hit.gameObject.layer != 9) //Not block
				continue;
			
			_tempFireObject = (GameObject)Instantiate(fireObject, this.transform.position, this.transform.rotation);
			_tempFireObject.transform.parent = hit.transform;
			_tempFireObject.transform.localPosition = new Vector3(0f,0f,0f);
			if (_tempFireObject.GetComponent<ParticleEmitter>())
			{
				_tempFireObject.GetComponent<ParticleEmitter>().emit = true;
				Destroy(_tempFireObject, 15);
			}
		}
		
		Destroy(gameObject);
	}
}