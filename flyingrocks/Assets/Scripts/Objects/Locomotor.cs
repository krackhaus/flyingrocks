using UnityEngine;

[RequireComponent (typeof(Rigidbody))]

public class Locomotor : MonoBehaviour
{
	public float velocity = 10f;
	public float turnRate = 100f;
	public float jumpForce = 10f;
	public float runningModifier = 1.5f;
	private bool running = false;
	
	void FixedUpdate ()
	{
		rigidbody.velocity = rigidbody.angularVelocity = Vector3.zero;
	}
	
	public void GoForward ()
	{
		transform.Translate (Vector3.forward * (running ? velocity * runningModifier : velocity) * Time.deltaTime);
	}

	public void GoBackward ()
	{
		transform.Translate (Vector3.forward * -(running ? velocity * runningModifier : velocity) * Time.deltaTime * 0.65f);
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
	
	public void MouseLook (float deltaX)
	{
		transform.Rotate(Vector3.up, deltaX);
	}

	public void Jump ()
	{
		rigidbody.AddForce (Vector3.up * jumpForce);
	}
	
	public bool Running {
		set { running = value; }
	}
}
