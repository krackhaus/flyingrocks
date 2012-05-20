using UnityEngine;
using System.Collections;

public class SpawnObjectsOfInterest : MonoBehaviour
{
	public float gridLength = 10;
	public float gridHeight = 10;
	public int numberOfObjectsToCreate = 10;
	public int averageNumberOfBites = 2;
	public GameObject objectOfInterest;
	
	void Start()
	{
		CreateObjectsOfInterest();
	}
	
	void CreateObjectsOfInterest()
	{
		for (int i = 0; i < numberOfObjectsToCreate; i++)
		{
			Vector3 newPos = new Vector3(Random.Range(-gridHeight/2, gridHeight/2), 0, Random.Range(-gridLength/2, gridLength/2));
			GameObject go = Instantiate(objectOfInterest, newPos, Quaternion.identity) as GameObject;
			go.GetComponent<ObjectOfInterest>().NibblesRemaining = Mathf.CeilToInt(Random.Range(averageNumberOfBites * Random.value, averageNumberOfBites * Random.value));
			go.transform.parent = GameObject.FindWithTag("World").transform;
		}
	}
}
