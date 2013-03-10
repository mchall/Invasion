using UnityEngine;
using System;
using System.Collections;
using System.Security.Cryptography;
using System.Text;

public class AutoLogin : MonoBehaviour 
{	
	void Start ()
	{
	    Application.ExternalCall("GJAPI_AuthUser", gameObject.name, "TryLogin");
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
			
			GetComponent<GUIText>().text = String.Format("Logged in as {0}", username);
		}
		catch
		{
			GetComponent<GUIText>().text = "Not logged in";
		}
	}
}