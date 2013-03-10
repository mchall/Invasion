using UnityEngine;
using System.Collections;

public class Player : Character 
{	
	public GUIText scoreObject;
	public GameObject enemy;
	public float score;
	public bool recordedScore;
	
	protected override void Die()
	{	
		base.Die();
		if(!recordedScore)
		{
			if(!alive)
			{
				recordedScore = true;
				AddHighScore();
			}
		}
	}
	
	private void AddHighScore()
	{
		if(!string.IsNullOrEmpty(Global.UserName) && !string.IsNullOrEmpty(Global.Token))
		{
			GameJoltApi api = new GameJoltApi();
			StartCoroutine(api.DoAddHighScore(Global.UserName, Global.Token, (int)score));	
		}
	}

	// Update is called once per frame
	public void Update() 
	{	
		if(alive && !Global.GameOver && enemy != null)
		{
			var enemyX = enemy.transform.position.x;
			var myX = thisTransform.position.x;
			score += ((myX - enemyX) / 10) * (Global.HardcoreMode ? 1.2f : 1f);
			
			scoreObject.text = string.Format("Score: {0}", (int)score);			
		}	
		
		if(!recordedScore && Global.GameOver)
		{
			recordedScore = true;
			AddHighScore();
		}
		
		// these are false unless one of keys is pressed
		isLeft = false;
		isRight = false;
		isJump = false;
		
		if(alive)
		{
			if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) 
			{ 
				isLeft = true; 
				facingDir = facing.Left;
			}
			if ((Input.GetKey(KeyCode.D)) || (Input.GetKey(KeyCode.RightArrow)) && isLeft == false) 
			{ 
				isRight = true; 
				facingDir = facing.Right;
			}
			
			if (Input.GetKeyDown(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) 
			{ 
				isJump = true; 
			}
		}		
		
		if(Input.GetKey(KeyCode.R))
		{
			Global.GameOver = false;
			Application.LoadLevel(Application.loadedLevelName); 
		}
		
		if(Input.GetKey(KeyCode.Escape))
		{
			Global.GameOver = false;
			Application.LoadLevel("MainMenu"); 
		}
		
		UpdateMovement();
	}	
}