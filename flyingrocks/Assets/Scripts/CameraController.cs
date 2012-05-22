using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraController : MonoBehaviour
{
	public float desiredDistance = 30f;
	public float desiredHeight = 3f;
	public float speedDamping = 10f;
	public bool determineFocalPoint = false;
	
	Vector3 offsetVector, tpos, fpos;
	
	List<GameObject> objectsInScene = new List<GameObject>();
	GameObject focalPoint;
	
	void Awake()
	{
		tpos = transform.position;
		focalPoint = GameObject.Find("CameraFocalPoint");
		offsetVector = new Vector3(0, desiredHeight, 0);
	}
	
	public void Bootstrap()
	{
		GameObject[] gos = GameObject.FindObjectsOfType(typeof (GameObject)) as GameObject[]; //.FindObjectsOfType<GameObject>();
		foreach (GameObject go in gos)
		{
			if (go.name == "World")
				continue;
			objectsInScene.Add(go);
		}
		//Debug.Log ("Objects of Interest in Scene = " +objectsInScene.Count);
	}
	
	void Update()
	{
		if (determineFocalPoint)
			FindFocalPoint();
		fpos = focalPoint.transform.position;
		if (desiredHeight >= tpos.y)
			transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(fpos), Time.deltaTime);
		else
			transform.LookAt(fpos);
		float distance = Vector3.Distance(tpos, fpos);
		if (distance >= desiredDistance)
			transform.position = Vector3.Lerp(tpos, (fpos + offsetVector), (distance/(distance * speedDamping)) * Time.deltaTime);
	}
	
	void FindFocalPoint()
	{
		Vector3 pos = new Vector3(0,0,0);
		try
		{
			foreach (GameObject go in objectsInScene)
				pos += go.transform.position;
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
