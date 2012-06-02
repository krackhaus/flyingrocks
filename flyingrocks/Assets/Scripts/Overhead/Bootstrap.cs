using UnityEngine;
using System.Collections;

/// <summary>
/// Bootstrap.
/// </summary>
/// <remarks>
/// Manual starting of Game Objects which have dependencies.
/// </remarks>

[RequireComponent(typeof(CameraController), 
	typeof(SpawnObjects), typeof(NetworkController))]
public class Bootstrap : MonoBehaviour
{
	public bool runInQuickMode = false;
	public bool runAsServer = false;
	
	private NetworkController networkController;
	
	void Start()
	{
		// Figure out what we're doing
		OptionSelect();
		
		// Put things where they belong
		GameObject[] gos = new GameObject[2]
		{
			new GameObject("Generated Enemies"),
			new GameObject("Badger Noms")
		};
		foreach (GameObject go in gos)
			go.transform.parent = GameObject.Find("World").transform;
		
		// ...and let'er rip!
		GetComponent<SpawnObjects>().Bootstrap();
		GetComponent<CameraController>().Bootstrap();
	}
	
	void OptionSelect()
	{
		if (runAsServer)
		{
			networkController = GetComponent<NetworkController>() as NetworkController;
			networkController.LaunchServer();
		}
		else if (runInQuickMode)
			Time.timeScale = 5;
	}
}
