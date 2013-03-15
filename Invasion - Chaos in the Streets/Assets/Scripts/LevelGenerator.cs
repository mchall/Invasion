using UnityEngine;

public class LevelGenerator : MonoBehaviour 
{	
	public GameObject fallenEnemyObj;
	public GameObject lastLevel;
	public GameObject level;

	float time = 0;
	float fallenEnemyTime = 0;
	
	public void Update() 
	{
		if(Global.GameOver)
			return;
		
		time += Time.deltaTime;
		fallenEnemyTime += Time.deltaTime;
		
		if (time > 3.5f)
        {
			GenerateFloor();
            time = 0;
        }
		
		if (fallenEnemyTime > 15f)
        {
			GenerateFallenEnemy();
            fallenEnemyTime = 0;
        }
	}
	
	void GenerateFloor()
	{
		GameObject levelClone = (GameObject)Instantiate(level, lastLevel.transform.position + new Vector3(10, 0, 0), Quaternion.identity);
		lastLevel = levelClone;
	}
	
	void GenerateFallenEnemy()
	{
		Instantiate(fallenEnemyObj, lastLevel.transform.position + new Vector3(0, 6, 0), Quaternion.identity);
	}
}