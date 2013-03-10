using UnityEngine;
using System.Collections;

public class FallenEnemy : MonoBehaviour 
{	
	public void Start()
	{
		OTAnimatingSprite sprite = GetComponent<OTAnimatingSprite>();
		sprite.onOutOfView = DestroyWhenOutOfView;	
	}
	
	public void Update() 
	{
		transform.position += new Vector3(-0.01f,-0.01f,0f);
	}
	
	void DestroyWhenOutOfView(OTObject owner)
    {
        OT.DestroyObject(owner);
    }
}