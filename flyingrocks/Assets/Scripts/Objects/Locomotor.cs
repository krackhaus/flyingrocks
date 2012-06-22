using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Rigidbody))]

public class Locomotor : MonoBehaviour
{
	public float velocity = 10f;
	public float turnRate = 100f;
	public float jumpForce = 10f;
	public float runningModifier = 1.5f;
	
	private bool hasHead = false;
	private bool running = false;
	private Transform headTransform;
	
	void Awake()
	{
		//this kind of data will come from the object's controller class later on...
		try
		{
			headTransform = transform.FindChild("Face").transform;
			hasHead = true;
		}
		catch{}
	}
	
	void FixedUpdate ()
	{
		//rigidbody.velocity = rigidbody.angularVelocity = Vector3.zero;
		if (hasHead)
			Debug.DrawRay(headTransform.position, (headTransform.TransformDirection(Vector3.forward) * 5), Color.red);
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
		if (hasHead)
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
	
	#region AI specific locomotion
	public IEnumerator TurnToFace(Vector3 position, float rate)
	{
		Quaternion lookAt = Quaternion.LookRotation(position - transform.position, Vector3.up);
		
		lookAt.x = 0;
		
		while ( Quaternion.Angle(transform.rotation, lookAt) > 1 )
		{
			lookAt = Quaternion.LookRotation(position - transform.position, Vector3.up);
			transform.rotation = Quaternion.Lerp(transform.rotation, lookAt, 3 * Time.deltaTime);
			
			//TODO check for attactors in fov while sweeping
			
			yield return new WaitForSeconds(Time.deltaTime);
		}
	}
	#endregion
}
