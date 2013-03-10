using UnityEngine;
using System.Collections;

public class Cam : MonoBehaviour 
{		
	public void LateUpdate()
	{
	   	transform.position = new Vector3(transform.position.x, 2, transform.position.z);
		transform.localRotation = new Quaternion(0, 0, 0, 0);
	}
}