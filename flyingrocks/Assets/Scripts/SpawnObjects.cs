using UnityEngine;
using System.Collections;

public class SpawnObjects : MonoBehaviour
{
	public float gridLength = 10;
	public float gridHeight = 10;
	public int numberOfObjectsOfInterestToCreate = 10;
	public int numberOfEnemiesToCreate = 2;
	public int averageNumberOfBites = 2;
	public GameObject objectOfInterest;
	public GameObject enemy;
	
	void Start()
	{
		CreateObjectsOfInterest(numberOfObjectsOfInterestToCreate);
		CreateEnemies(numberOfEnemiesToCreate);
	}
	
	void CreateObjectsOfInterest(int quantity)
	{
		for (int i = 0; i < quantity; i++)
		{
			GameObject go = Instantiate(objectOfInterest, new Vector3(Random.Range(-gridHeight/2, gridHeight/2), 0, Random.Range(-gridLength/2, gridLength/2)), Quaternion.identity) as GameObject;
			go.transform.parent = transform;
			Vector3 goPos = go.transform.position;
			goPos.y = go.transform.lossyScale.y/2;
			go.transform.position = goPos;
		}
	}
	
	void CreateEnemies(int quantity)
	{
		for (int i = 0; i < quantity; i++)
		{
			GameObject go = Instantiate(enemy, new Vector3(Random.Range(-gridHeight/2, gridHeight/2), 0, Random.Range(-gridLength/2, gridLength/2)), Quaternion.identity) as GameObject;
			go.transform.parent = transform;
			Vector3 goPos = go.transform.position;
			goPos.y = go.transform.lossyScale.y/2;
			go.transform.position = goPos;
		}
	}
	
	//in fact, we can use this class to create anything we need!
}
