using UnityEngine;

[RequireComponent (typeof(Rigidbody), typeof(PlayerInputController))]

public class Locomotor : MonoBehaviour
{
	public float velocity = 10f;
	public float turnRate = 100f;
	public float jumpForce = 10f;
	public float runningModifier = 1.5f;
	private bool running = false;
	private Transform headTransform;
	
	void Awake()
	{
		headTransform = transform.FindChild("Face").transform;
		//rockSpawnTransform = transform.FindChild("RockSpawnLocation").transform;
		//cameraTransform = GameObject.FindWithTag("MainCamera").transform;
	}
	
	void FixedUpdate ()
	{
		rigidbody.velocity = rigidbody.angularVelocity = Vector3.zero;
		Transform tform = transform.FindChild("Face").transform;
		Vector3 direction = tform.TransformDirection(Vector3.forward) * 5;
		Debug.DrawRay(tform.position, direction, Color.red);
	}
	
	public void GoForward ()
	{
		transform.Translate (Vector3.forward * (running ? velocity * runningModifier : velocity) * Time.deltaTime);
	}
	
	public void GoForward (float velocity)
	{
		transform.Translate (Vector3.forward * (running ? velocity * runningModifier : velocity));
	}

	public void GoBackward ()
	{
		transform.Translate (Vector3.forward * -(running ? velocity * runningModifier : velocity) * Time.deltaTime * 0.65f);
	}
	
	public void Strafe (float velocity)
	{
		transform.Translate (Vector3.right * (running ? velocity * runningModifier : velocity));
	}
	
	public void StrafeRight ()
	{
		transform.Translate (Vector3.right * (running ? velocity * runningModifier : velocity) * Time.deltaTime * 0.5f);
	}

	public void StrafeLeft ()
	{
		transform.Translate (Vector3.right * -(running ? velocity * runningModifier : velocity) * Time.deltaTime * 0.5f);
	}

	public void TurnRight ()
	{
		transform.Rotate (Vector3.up, turnRate * Time.deltaTime);
	}

	public void TurnLeft ()
	{
		transform.Rotate (Vector3.up, -turnRate * Time.deltaTime);
	}
	
	public void FreeLook (float deltaX, float deltaY)
	{
		transform.Rotate(Vector3.up, deltaX);
		headTransform.Rotate(Vector3.right, deltaY); // just cause.
	}

	public void Jump ()
	{
		rigidbody.AddForce (Vector3.up * jumpForce);
	}
	
	/*
	 * Dan: I realized why I use PascalCase...because I use the same name as the variable.
	 * I also see you favor using underscores...not sure I can get on board with that, lol.
	 */
	public bool Running
	{
		set { running = value; }
	}
}
