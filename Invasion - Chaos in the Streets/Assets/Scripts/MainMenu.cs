using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour 
{
	public GUISkin customGUI;
	
	bool nameMode = false;
	
	float time = 0;
	
#if UNITY_WEBPLAYER
	string[] menuOptions = new string[4];
#else
	string[] menuOptions = new string[5];
#endif
	
	int selectedIndex = 1;
	
	void Start()
	{
		menuOptions[0] = "Tutorial";
		menuOptions[1] = "Play";
		menuOptions[2] = "Play+";
		menuOptions[3] = "ChangeName";
#if !UNITY_WEBPLAYER
		menuOptions[4] = "Exit";
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
		time += Time.deltaTime;
		if(time < 0.1f)
			return;
		
	    if (Input.GetKeyDown(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) 
		{
			selectedIndex = MenuSelection(selectedIndex, "up");
			time = 0;
	    }
			
		if (Input.GetKeyDown(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) 
		{
			selectedIndex = MenuSelection(selectedIndex, "down");
			time = 0;
	    }
		
		if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter)) 
		{
			RunOption();
			time = 0;
	    }
	}
	
	void OnGUI () 
	{		
		GUI.skin = customGUI;
		
		if(nameMode)
		{
			GUI.SetNextControlName("UserNameField");
			Global.UserName = GUI.TextField(new Rect(((Screen.width-300) / 2),((Screen.height / 2)) + 40,300,30), Global.UserName); 
			
			Event e = Event.current;
        	if (e.keyCode == KeyCode.Return || e.keyCode == KeyCode.Escape) 
			{
				if(time > 0.1f)
				{
					UpdateUserName();
				}
			}
			
			if (GUI.Button(new Rect(((Screen.width-300) / 2),((Screen.height / 2) + 80),300,30), "Change")) 
			{
				UpdateUserName();
			} 
			
			if (nameMode && Input.GetKeyDown(KeyCode.Return))
			{
				nameMode = false;
			}
			
			GUI.FocusControl("UserNameField");
			
			return;
		}
		
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
		
		GUI.SetNextControlName("ChangeName");
	    if (GUI.Button(new Rect(((Screen.width-300) / 2),((Screen.height / 2)) + 120,300,30), "Change login")) 
		{
			selectedIndex = 3;
			RunOption();
		}	
	
#if !UNITY_WEBPLAYER
	    GUI.SetNextControlName("Exit");
	    if (GUI.Button(new Rect(((Screen.width-300) / 2),((Screen.height / 2)) + 160,300,30), "Exit")) 
		{
			selectedIndex = 4;
			RunOption();
		}
#endif
	
	    GUI.FocusControl(menuOptions[selectedIndex]);
	}
	
	void UpdateUserName()
	{
		if(string.IsNullOrEmpty(Global.UserName))
		{
			Global.UserName = "NO NAME";
		}
		
		PlayerPrefs.SetString("UserName", Global.UserName);
		nameMode = false;
		Global.Token = null;
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
		else if(selectedIndex == 3)
		{
			nameMode = true;
			time = 0;
		}	
#if !UNITY_WEBPLAYER
		else if(selectedIndex == 4)
		{
			Application.Quit(); 
		}
#endif		
	}
}