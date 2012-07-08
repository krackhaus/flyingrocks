using UnityEngine;
using System.Collections;

public class Cursor : MonoBehaviour
{
	public float rotationDamping = 10.0f;
	
	private GameObject player;
	
	void Awake()
	{
		player = GameObject.FindWithTag("Player");
	}
}
