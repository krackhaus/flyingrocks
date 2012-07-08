using UnityEngine;
using System.Collections;

public class RoomFollowCamera : MonoBehaviour
{
	public Transform desiredCameraLocation;
	public float distance = 10.0f;
	public float height = 1.0f;
	public float damping = 5.0f;
	public float rotationDamping = 10.0f;
	
	private Transform player;
	
	void Awake()
	{
		player = GameObject.FindWithTag("Player").transform;
	}
	
	void OnTriggerEnter(Collider other)
	{
		if (other.tag == player.tag)
			enabled = true;
	}
	
	void OnTriggerExit(Collider other)
	{
		if (other.tag == player.tag)
			enabled = false;
	}
	
	void LateUpdate()
	{
		FollowPlayer();
	}
	
	private void FollowPlayer()
	{
		// Calculate and set camera position and rotation
		Vector3 cameraPosition = Camera.mainCamera.transform.position = Vector3.Lerp(Camera.mainCamera.transform.position, desiredCameraLocation.position, Time.deltaTime * damping);
		Quaternion desiredRotation = Quaternion.LookRotation(player.position - cameraPosition);
		Camera.mainCamera.transform.rotation = Quaternion.Slerp(Camera.mainCamera.transform.rotation, desiredRotation, Time.deltaTime * rotationDamping);
	}
}
