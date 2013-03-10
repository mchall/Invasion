using UnityEngine;
using System.Collections;

public class Civilian : Character 
{	
	float time = 0;
	System.Random random;
	
	// Use this for initialization
	public override void Start () 
	{
		base.Start();
		killedByLeftScreen = false;
		random = new System.Random();
	}
	
	// Update is called once per frame
	public void Update () 
	{
		time += Time.deltaTime;
		if (time > 1)
        {
			if(random.NextDouble() > 0.5)
			{
				isRight = false;
				isLeft = true;
				facingDir = facing.Left;
			}
			else
			{
				isLeft = false;
				isRight = true;
				facingDir = facing.Right;
			}
            time = 0;
        }
		
		UpdateMovement();
	}	
}