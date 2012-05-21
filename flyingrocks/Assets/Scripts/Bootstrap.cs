using UnityEngine;
using System.Collections;

public class Bootstrap : MonoBehaviour
{
	void Start()
	{
		new GameObject("Generated Enemies");
		GameObject.Find("World").GetComponent<SpawnObjects>().Bootstrap();
		GameObject.FindWithTag("MainCamera").GetComponent<CameraController>().Bootstrap();
	}
}
