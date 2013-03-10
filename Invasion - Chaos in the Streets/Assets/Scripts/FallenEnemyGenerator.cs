using UnityEngine;
using System.Collections;

public class FallenEnemyGenerator : MonoBehaviour 
{	
	public GameObject fallenEnemy;
	public AudioClip enemyDeath;
	bool generated = false;
	
	void OnTriggerEnter(Collider other) 
	{
		if(generated)
			return;
		
		if(other.gameObject.tag == "Player")
		{
			generated = true;
			Camera.main.audio.PlayOneShot(enemyDeath);
		
			Instantiate(fallenEnemy, transform.localPosition + new Vector3(10, 0, 0), Quaternion.identity);
		}
    }
}