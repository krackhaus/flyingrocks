using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Locomotor))]
[RequireComponent (typeof(Acquirer))]

public class PlayerInputController : MonoBehaviour
{
	private Locomotor locomotor;
	private Acquirer acquirer;
	private RockThrower thrower;
	
	private Vector2 mouse, position, rotation;
	private float damper = 0.1f;

	void Start ()
	{
		locomotor = GetComponent<Locomotor> ();
		acquirer = GetComponent<Acquirer> ();
		thrower = GetComponent<RockThrower> ();
		Screen.showCursor = false;
	}

	void Update ()
	{
		//KEYBOARD --------------------
		if (Input.GetKey (KeyCode.W)) {
			locomotor.GoForward ();
		}

		if (Input.GetKey (KeyCode.S)) {
			locomotor.GoBackward ();
		}

		if (Input.GetKey (KeyCode.D)) {
			locomotor.StrafeRight ();
		}

		if (Input.GetKey (KeyCode.A)) {
			locomotor.StrafeLeft ();
		}

		if (Input.GetKey (KeyCode.E)) {
			locomotor.TurnRight ();
		}

		if (Input.GetKey (KeyCode.Q)) {
			locomotor.TurnLeft ();
		}
		
		if (Input.GetKey(KeyCode.Space))
			locomotor.Jump();
		
		// HYBRID ---------------------
		if (Input.GetMouseButton (0) || Input.GetButtonDown("Fire"))
			thrower.ThrowRock ();
		
		if (Input.GetMouseButton (1) || Input.GetButtonDown("Action"))
			acquirer.AcquireFixation ();

		if (Input.GetButton("Run"))
			locomotor.Running = true;
		
		// MOUSE ----------------------
		mouse.x = Input.GetAxis ("Mouse X");
		mouse.y = Input.GetAxis ("Mouse Y");
		locomotor.FreeLook(mouse.x, mouse.y);
		
		// JOYSTICK -------------------
		position.x = Input.GetAxis("LeftStickHorizontal") * damper;
		position.y = Input.GetAxis("LeftStickVertical") * damper;
		rotation.x = Input.GetAxis("RightStickHorizontal");
		rotation.y = Input.GetAxis("RightStickVertical");
		locomotor.FreeLook(rotation.x, rotation.y);
		locomotor.GoForward(position.y);
		locomotor.Strafe(position.x);
		if (position.x + position.y == 0)
			locomotor.Running = false;
	}

}
