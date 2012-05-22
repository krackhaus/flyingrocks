using UnityEngine;
using System.Collections;

public class NetworkController : MonoBehaviour
{
	public int numberOfNetworkPlayers = 8;
	public int networkPort = 25000;
	
	public void LaunchServer()
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
