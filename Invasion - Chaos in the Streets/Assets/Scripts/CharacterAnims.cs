using UnityEngine;
using System.Collections;

public class CharacterAnims : MonoBehaviour 
{
	public enum anim { None, WalkLeft, WalkRight, StandLeft, StandRight, FallLeft, FallRight }
	
	OTAnimatingSprite mySprite;
	OTAnimatingSprite playerSprite;
	
	anim currentAnim;
	Character character;
	
	// Use this for initialization
	void Start () 
	{
		character = GetComponent<Character>();
		mySprite = GetComponent<OTAnimatingSprite>();
	}
	
	void Update() 
	{
		// Dead
		if(!character.alive)
		{
			currentAnim = anim.FallRight;
			mySprite.Play("fallLeft");
			mySprite.rotation = 90;
			return;
		}
		
		// run left
		if(character.isLeft && character.grounded == true && currentAnim != anim.WalkLeft)
		{
			currentAnim = anim.WalkLeft;
			mySprite.Play("runLeft");
		}
		if(!character.isLeft && character.grounded == true && currentAnim != anim.StandLeft && character.facingDir == Character.facing.Left)
		{
			currentAnim = anim.StandLeft;
			mySprite.Play("standLeft");
		}
		
		// run right
		if(character.isRight && character.grounded && currentAnim != anim.WalkRight)
		{
			currentAnim = anim.WalkRight;
			mySprite.Play("runRight");
		}
		if(!character.isRight && character.grounded && currentAnim != anim.StandRight && character.facingDir == Character.facing.Right)
		{
			currentAnim = anim.StandRight;
			mySprite.Play("standRight");
		}
		
		// falling
		if(character.grounded == false && currentAnim != anim.FallLeft && character.facingDir == Character.facing.Left)
		{
			currentAnim = anim.FallLeft;
			mySprite.Play("fallLeft");
		}
		if(character.grounded == false && currentAnim != anim.FallRight && character.facingDir == Character.facing.Right)
		{
			currentAnim = anim.FallRight;
			mySprite.Play("fallRight");
		}
	}
}
