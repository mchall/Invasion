using UnityEngine;
using System.Collections;

public class MusicPlayer : MonoBehaviour 
{		
	private static MusicPlayer instance = null;
    public static MusicPlayer Instance 
	{
        get { return instance; }
    }
	
	public void Update()
	{
	   	transform.position = Camera.main.transform.position;
	}
	
    public void Awake() 
	{
        if (instance != null && instance != this) 
		{
            Destroy(this.gameObject);
            return;
        } 
		else 
		{
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }
}