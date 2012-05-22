using UnityEngine;
using System.Collections;

public class Bootstrap : MonoBehaviour
{
	public bool runInQuickMode = false;
	public bool runAsServer = false;
	public int numberOfNetworkPlayers = 8;
	public int networkPort = 25000;
	
	void Start()
	{
		new GameObject("Generated Enemies");
		GameObject.Find("World").GetComponent<SpawnObjects>().Bootstrap();
		GameObject.FindWithTag("MainCamera").GetComponent<CameraController>().Bootstrap();
		if (runInQuickMode)
			Time.timeScale = 5;
		else if (runAsServer)
			LaunchServer();
	}
	
	void LaunchServer()
	{
		bool useNat = Network.HavePublicAddress();
		Debug.Log ("The Network connection "
			+(useNat?"has a Public Address; Using NAT.":"does not have a Public Address."));
		Network.InitializeServer(numberOfNetworkPlayers, networkPort, useNat);
	}
	
	void OnServerInitialized()
	{
		Debug.Log ("Server initialized; Accepting incomming connections.");
	}
}
