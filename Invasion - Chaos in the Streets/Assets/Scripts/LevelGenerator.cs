using UnityEngine;

public class LevelGenerator : MonoBehaviour 
{	
	public GameObject building1Obj;
	public GameObject building2Obj;
	public GameObject fallenEnemyObj;
	public GameObject lastLevel;
	public GameObject level;

	float time = 0;
	float fallenEnemyTime = 0;
	float buildingTime = 0;
	
	System.Random random;
	
	public void Update() 
	{
		if(Global.GameOver)
			return;
		
		time += Time.deltaTime;
		fallenEnemyTime += Time.deltaTime;
		buildingTime += Time.deltaTime;
		
		if (time > 3.5f)
        {
			GenerateFloor();
            time = 0;
        }
		
		if (fallenEnemyTime > 10f)
        {
			GenerateFallenEnemy();
            fallenEnemyTime = 0;
        }
		
		if (buildingTime > 5f)
        {
			GenerateBuilding();
            buildingTime = 0;
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
	
	void GenerateBuilding()
	{
		if(random == null)
		{
			random = new System.Random();
		}
		
		if(random.NextDouble() > 0.25)
		{
			Instantiate(building1Obj, lastLevel.transform.position + new Vector3(0, 1, 0), Quaternion.identity);
		}
		else
		{
			Instantiate(building2Obj, lastLevel.transform.position + new Vector3(0, 6, 0), Quaternion.identity);
		}
	}
}