using UnityEngine;
using System.Collections;

public class Chopper : MonoBehaviour 
{	
	public AudioClip fireRocket;
	public GameObject enemy;
	public GameObject rocket;
	public GUIText victoryText;
	bool generated = false;
	
	void Update()
	{
		if(enemy != null)
		{
			if(!generated && transform.position.x - enemy.transform.position.x < 10)
			{
				Global.GameOver = true;
				
				victoryText.text = "A WINNER IS YOU!\n\nPress 'R' to reset level \nor 'ESC' to leave";
				
				Camera.main.audio.PlayOneShot(fireRocket);
				if(rocket != null)
				{
					GameObject rocketClone = (GameObject)Instantiate(rocket, transform.position, Quaternion.identity);
					rocketClone.transform.parent = transform;
					rocketClone.GetComponent<Rocket>().destroyInvader = true;
					OTAnimatingSprite sprite = rocketClone.GetComponent<OTAnimatingSprite>();
					sprite.onOutOfView = DestroyWhenOutOfView;	
					rocketClone.rigidbody.velocity = (enemy.transform.localPosition - transform.localPosition).normalized * 50; 
					rocketClone.rigidbody.velocity = new Vector3(rocketClone.rigidbody.velocity.x, 0, rocketClone.rigidbody.velocity.z);
				}
				
				generated = true;
			}
		}
	}
	
	void DestroyWhenOutOfView(OTObject owner)
    {
        OT.DestroyObject(owner);
    }
}