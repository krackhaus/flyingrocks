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
	
	public void Bootstrap()
	{
		SpawnObjectsAcrossGrid(numberOfObjectsOfInterestToCreate, objectOfInterest);
		SpawnObjectsAcrossGrid(numberOfEnemiesToCreate, enemy);
	}
	
	public void SpawnObjectsAcrossGrid(int quantity, GameObject g)
	{
		for (int i = 0; i < quantity; i++)
		{
			GameObject go = Instantiate(g, GetRandomPositionOnGrid(), Quaternion.identity) as GameObject;
			Vector3 goPos = go.transform.position;
			goPos.y = go.transform.lossyScale.y/2;
			go.transform.position = goPos;
			go.name = g.name + (i+1);
		}
	}
	
	public Transform GetRandomTransformOnGrid()
	{
		GameObject go = new GameObject();
		go.name = ("target");
		go.transform.position = GetRandomPositionOnGrid();
		go.transform.parent = GameObject.Find("World").transform;
    	go.hideFlags = HideFlags.HideInHierarchy;
		return go.transform;
	}
	
	Vector3 GetRandomPositionOnGrid()
	{
		return new Vector3(Random.Range(-gridHeight/2, gridHeight/2), 0, Random.Range(-gridLength/2, gridLength/2));
	}
}
