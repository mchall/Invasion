using UnityEngine;
using System;
using System.Collections;
using System.Security.Cryptography;
using System.Text;

public class AutoLogin : MonoBehaviour 
{	
	void Start ()
	{		
		Global.UserName = PlayerPrefs.GetString("UserName");
		if(string.IsNullOrEmpty(Global.UserName))
		{
			Global.UserName = System.Environment.UserName;
		}
		
	    Application.ExternalCall("GJAPI_AuthUser", gameObject.name, "TryLogin");
	}
	
	void Update()
	{
		GetComponent<GUIText>().text = String.Format("Logged in as {0}", Global.UserName);
	}

	public void TryLogin(string response)
	{
		try
		{
		    string[] splittedResponse = response.Split(':');
		    string username = splittedResponse[0];
		    string usertoken = splittedResponse[1];
			
			Global.UserName = username;
			Global.Token = usertoken;
		}
		catch
		{
		}
	}
}