using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour 
{
	public GUISkin customGUI;
	
#if UNITY_WEBPLAYER
	string[] menuOptions = new string[3];
#else
	string[] menuOptions = new string[4];
#endif
	
	int selectedIndex = 1;
	
	void Start()
	{
		menuOptions[0] = "Tutorial";
		menuOptions[1] = "Play";
		menuOptions[2] = "Play+";
#if !UNITY_WEBPLAYER
		menuOptions[3] = "Exit";
#endif
	}
	
	int MenuSelection(int selectedItem, string direction) 
	{
    	if (direction == "up") 
		{
        	if (selectedItem == 0) 
			{
            	selectedItem = menuOptions.Length - 1;
        	} 
			else 
			{
            	selectedItem -= 1;
        	}
    	}

	    if (direction == "down") 
		{
	        if (selectedItem == menuOptions.Length - 1) 
			{
				selectedItem = 0;
	        } 
			else 
			{
				selectedItem += 1;
	        }
	    }

    	return selectedItem;
	}
	
	void Update()
	{	
	    if (Input.GetKeyDown(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) 
		{
			selectedIndex = MenuSelection(selectedIndex, "up");
	    }
			
		if (Input.GetKeyDown(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) 
		{
			selectedIndex = MenuSelection(selectedIndex, "down");
	    }
		
		if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter)) 
		{
			RunOption();
	    }
	}
	
	void OnGUI () 
	{		
		GUI.skin = customGUI;
		
	    GUI.SetNextControlName("Tutorial");
	    if (GUI.Button(new Rect(((Screen.width-300) / 2),((Screen.height / 2)),300,30), "Tutorial")) 
		{
			selectedIndex = 0;
			RunOption();
		}    
	
	    GUI.SetNextControlName("Play");
	    if (GUI.Button(new Rect(((Screen.width-300) / 2),((Screen.height / 2)) + 40,300,30), "Play Game")) 
		{
			selectedIndex = 1;
			RunOption();
		}	    
		
		GUI.SetNextControlName("Play+");
	    if (GUI.Button(new Rect(((Screen.width-300) / 2),((Screen.height / 2)) + 80,300,30), "Infinite Run")) 
		{
			selectedIndex = 2;
			RunOption();
		}	 
	
#if !UNITY_WEBPLAYER
	    GUI.SetNextControlName("Exit");
	    if (GUI.Button(new Rect(((Screen.width-300) / 2),((Screen.height / 2)) + 120,300,30), "Exit")) 
		{
			selectedIndex = 3;
			RunOption();
		}
#endif
	
	    GUI.FocusControl(menuOptions[selectedIndex]);
	}
	
	void RunOption()
	{
		if(selectedIndex == 0)
		{
			Application.LoadLevel("Tutorial"); 
		}
		else if(selectedIndex == 1)
		{
			Global.HardcoreMode = false;
			Application.LoadLevel("Game"); 
		}		
		else if(selectedIndex == 2)
		{
			Global.HardcoreMode = true;
			Application.LoadLevel("Infinity"); 
		}		
#if !UNITY_WEBPLAYER
		else if(selectedIndex == 3)
		{
			Application.Quit(); 
		}
#endif		
	}
}