using UnityEngine;
using System.Collections;

[RequireComponent(typeof(NetworkController))]
public class Bootstrap : MonoBehaviour
{
	public bool runInQuickMode = false;
	public bool runAsServer = false;
	
	private NetworkController networkController;
	
	void Start()
	{
		ModeSelect();
		new GameObject("Generated Enemies");
		GameObject.Find("World").GetComponent<SpawnObjects>().Bootstrap();
		GameObject.FindWithTag("MainCamera").GetComponent<CameraController>().Bootstrap();
	}
	
	void ModeSelect()
	{
		if (runInQuickMode)
			Time.timeScale = 5;
		else if (runAsServer)
		{
			networkController = GetComponent<NetworkController>() as NetworkController;
			networkController.LaunchServer();
		}
	}
}
