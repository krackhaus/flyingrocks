using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Locomotor))]
public class PlayerInputController : MonoBehaviour
{
	private Locomotor locomotor;
	private Acquirer acquirer;
	private InventoryThrower thrower;
	
	private string[] joystickNames;
	private Vector2 mouse, position, rotation;
	private float damper = 0.1f;
	private bool sticksActive = false;
	
	public GameObject cursor;
	public bool useCursor = true;
	public bool stickAndMouse = false;
	public bool invertLookYAxis = false;
	public float ySensitivity = 0.5f;
	public float cursorRotationDamping = 10.0f;
	
	void Awake()
	{
		cursor = GameObject.Find("Cursor");
	}

	void Start ()
	{
		SetupJoysticks();
		locomotor = GetComponent<Locomotor> ();
		acquirer = GetComponent<Acquirer> ();
		thrower = GetComponent<InventoryThrower> ();
		Screen.showCursor = false;
	}
	
	/*
	 * TODO -- Pull out joystick initilization and allow multi-player spawning
	 * for each attached joystick.
	 */
	void SetupJoysticks()
	{
		joystickNames = Input.GetJoystickNames() as string[];
		if (!JoystickAttached) return;
		foreach (string controllerName in joystickNames)
			Debug.Log (controllerName+ " is attached.");
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
		if (Input.GetMouseButtonDown (0) || Input.GetButtonDown("Fire")) {
			thrower.Throw();
			thrower.ReadyNext();
		}
		
		if (Input.GetMouseButtonDown (1) || Input.GetButtonDown("Action"))
			acquirer.AcquireFixation ();
		
		if (Input.GetButtonDown("DropRock"))
			thrower.Drop();

		locomotor.Running = Input.GetButtonDown("Run");
		
		// JOYSTICK / MOUSE -----------
		if (Input.GetKeyDown(KeyCode.JoystickButton16))
			sticksActive = !sticksActive;
		if (sticksActive)
		{
			position.x = Input.GetAxis("LeftStickHorizontal") * damper;
			position.y = Input.GetAxis("LeftStickVertical") * damper;
			rotation.x = Input.GetAxis("RightStickHorizontal");
			rotation.y = Input.GetAxis("RightStickVertical") * ySensitivity;
			if (useCursor && cursor)
			{
				cursor.transform.Translate(new Vector3(rotation.x, 0, -rotation.y), transform);
				//temp code
				Quaternion desiredRotation = Quaternion.LookRotation(cursor.transform.position - transform.position);
				transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, Time.deltaTime * cursorRotationDamping);
			}
			else
				locomotor.FreeLook(rotation.x, (invertLookYAxis?-rotation.y:rotation.y));
			locomotor.GoForward(position.y);
			locomotor.Strafe(position.x);
			if (position.magnitude == 0)
				locomotor.Running = false;
		}
		if (!sticksActive || stickAndMouse)
		{
			mouse.x = Input.GetAxis ("Mouse X");
			mouse.y = Input.GetAxis ("Mouse Y") * ySensitivity;
			locomotor.FreeLook(mouse.x, (invertLookYAxis?mouse.y:-mouse.y));
		}
	}
	
	void OnGUI()
	{
		if (JoystickAttached)
		{
			if (sticksActive)
				GUI.Label(new Rect(10, Screen.height-20, 400, 20), "Press 'Home' button to enable joystick.");
			else
				GUI.Label(new Rect(10, Screen.height-20, 400, 20), "Press 'Home' again to disable joystick.");
		}
	}
	
	bool JoystickAttached
	{
		get
		{
			if (joystickNames.Length != 0)
				return true;
			return false;
		}
	}
		
}

/*
 * Dualshock3 Button Numbers:
 * 0 = select
 * 1 = L3
 * 2 = R3
 * 3 = start
 * 4 = up
 * 5 = right
 * 6 = down
 * 7 = left
 * 8 = triangle
 * 9 = circle
 * 10 = L1
 * 11 = R2
 * 12 = L2
 * 13 = R2
 * 14 = X
 * 15 = square
 * 16 = home
 */
