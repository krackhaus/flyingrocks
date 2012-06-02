using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraController : MonoBehaviour
{
	public float desiredDistance = 32f;
	public float desiredHeight = 3f;
	public float speedDamping = 10f;
	public bool determineFocalPoint = false;
	
	List<GameObject> objectsInScene = new List<GameObject>();
	Vector3 offsetVector, tpos, fpos;
	GameObject mainCamera, focalPoint;
	
	void Awake()
	{
		mainCamera = GameObject.FindWithTag("MainCamera");
		focalPoint = GameObject.Find("CameraFocalPoint");
		offsetVector = new Vector3(0, desiredHeight, 0);
	}
	
	public void Bootstrap()
	{
		GameObject[] gos = GameObject.FindObjectsOfType(typeof (GameObject)) as GameObject[];
		foreach (GameObject go in gos)
		{
			if (go.name == "World")
				continue;
			objectsInScene.Add(go);
		}
		if (Debug.isDebugBuild)
		{
			Debug.Log ("Objects of Interest in Scene = " +objectsInScene.Count);
		}
	}
	
	void LateUpdate()
	{
		if (determineFocalPoint)
			FindFocalPoint();
		//fpos = focalPoint.transform.position;
		tpos = mainCamera.transform.position;
		if (desiredHeight >= tpos.y)
			mainCamera.transform.rotation = Quaternion.Slerp(mainCamera.transform.rotation, Quaternion.Euler(fpos), Time.deltaTime);
		else
			mainCamera.transform.LookAt(fpos);
		float distance = Vector3.Distance(tpos, fpos);
		if (distance >= desiredDistance)
			mainCamera.transform.position = Vector3.Lerp(tpos, (fpos + offsetVector), (distance/(distance * speedDamping)) * Time.deltaTime);
	}
	
	/*
	void OnGUI()
	{
		GUI.Label(new Rect(32, (Screen.height/4)-20, 200, 20), "CFD");
		desiredDistance = GUI.VerticalSlider(new Rect(32, Screen.height/4, 20, Screen.height/2), desiredDistance, 18, 64);
	}
	*/
	
	void FindFocalPoint()
	{
		if (focalPoint.GetType().Equals(typeof(Badger))) return;
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
	
	public GameObject FocalPoint
	{
		set { focalPoint = value; }
	}
}
