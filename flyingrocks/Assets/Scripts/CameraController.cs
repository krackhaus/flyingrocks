using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraController : MonoBehaviour
{
	public float desiredDistance = 30f;
	public float desiredHeight = 3f;
	public float speedDamping = 10f;
	public bool setFocalPoint = false;
	
	Vector3 offsetVector;
	
	List<GameObject> objectsInScene = new List<GameObject>();
	GameObject focalPoint;
	
	void Awake()
	{
		focalPoint = GameObject.Find("CameraFocalPoint");
		offsetVector = new Vector3(0, desiredHeight, 0);
	}
	
	public void Bootstrap()
	{
		GameObject[] gos = GameObject.FindObjectsOfType(typeof (GameObject)) as GameObject[]; //.FindObjectsOfType<GameObject>();
		foreach (GameObject go in gos)
		{
			if (go.name == "World") continue;
			objectsInScene.Add(go);
		}
		print ("object count = " +objectsInScene.Count);
	}
	
	void Update()
	{
		if (setFocalPoint)
			FindFocalPoint();
		Vector3 fpos = focalPoint.transform.position;
		transform.rotation = Quaternion.Slerp(transform.rotation, focalPoint.transform.rotation, Time.deltaTime);
		transform.LookAt(fpos);
		if (Vector3.Distance(transform.position, fpos) > desiredDistance)
			transform.position = Vector3.Lerp(transform.position, (fpos + offsetVector), Time.deltaTime / speedDamping);
	}
	
	void FindFocalPoint()
	{
		Vector3 pos = new Vector3(0,0,0);
		try
		{
			foreach (GameObject go in objectsInScene)
			{
				pos += go.transform.position;
				//print ("tpos = " +go.transform.position);
			}
			focalPoint.transform.position /= objectsInScene.Count;
		}
		catch
		{
			CleanSceneObjectsList();
		}
	}
	
	void CleanSceneObjectsList()
	{
		for (int i = 0; i < objectsInScene.Count; i++)
		{
			if (objectsInScene[i] == null)
				objectsInScene.RemoveAt(i);
		}
	}
}
