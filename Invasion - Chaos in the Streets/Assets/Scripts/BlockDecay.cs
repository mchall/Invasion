using UnityEngine;
using System.Collections;

public class BlockDecay : MonoBehaviour 
{	
	public void Start () 
	{
		OTSprite sprite = GetComponent<OTSprite>();
		sprite.onOutOfView = DestroyWhenOutOfView;	
	}
	
	void DestroyWhenOutOfView(OTObject owner)
    {
        OT.DestroyObject(owner);
    }
}