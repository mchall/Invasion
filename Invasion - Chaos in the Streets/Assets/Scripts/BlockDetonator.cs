using UnityEngine;
using System.Collections;

public class BlockDetonator : MonoBehaviour 
{	
	public GameObject block;
	public GameObject explosionObject;
	public GameObject fireObject;
	public AudioClip explosionSound;
	bool generated = false;
	
	float explosiveRadius = 2f;
    float explosivePower = 15f;
	
	GameObject _tempFireObject;
	
	void OnTriggerEnter(Collider other) 
	{
		if(generated)
			return;
		
		if(other.gameObject.tag == "Player")
		{
			generated = true;
			Camera.main.audio.PlayOneShot(explosionSound);
		
			//Instantiate(explosionObject, explosionPos, Quaternion.identity);
			
			var explosionPos = block.transform.position;
			
			GameObject explosion = (GameObject)Instantiate(explosionObject, explosionPos, Quaternion.identity);
			Destroy(explosion, 0.7f);
			
	        Collider[] colliders = Physics.OverlapSphere(explosionPos, explosiveRadius);
	        foreach (Collider hit in colliders) 
			{
				hit.rigidbody.AddForce(explosivePower, 0, 0, ForceMode.Impulse);
				
				if(hit.gameObject.layer != 9) //Not block
					continue;
				
				_tempFireObject = (GameObject)Instantiate(fireObject, this.transform.position, this.transform.rotation);
				_tempFireObject.transform.parent = hit.transform;
				_tempFireObject.transform.localPosition = new Vector3(0f,0f,0f);
				if (_tempFireObject.particleEmitter)
				{
					_tempFireObject.particleEmitter.emit = true;
					Destroy(_tempFireObject, 15);
				}
			}
		}
    }
}