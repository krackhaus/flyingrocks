using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Locomotor))]
[RequireComponent (typeof(Acquirer))]

public class PlayerInputController : MonoBehaviour
{

  private Locomotor locomotor;
  private Acquirer acquirer;
  private RockThrower thrower;

  void Start()
  {
    locomotor = GetComponent<Locomotor>();
    acquirer = GetComponent<Acquirer>();
    thrower = GetComponent<RockThrower>();
	Screen.showCursor = false;
  }

  void Update()
  {
    if (Input.GetKey(KeyCode.W)) {
      locomotor.GoForward();
    }

    if (Input.GetKey(KeyCode.S)) {
      locomotor.GoBackward();
    }

    if (Input.GetKey(KeyCode.D)) {
      locomotor.StrafeRight();
    }

    if (Input.GetKey(KeyCode.A)) {
      locomotor.StrafeLeft();
    }

    if (Input.GetKey(KeyCode.E)) {
      locomotor.TurnRight();
    }

    if (Input.GetKey(KeyCode.Q)) {
      locomotor.TurnLeft();
    }

    if (Input.GetKeyDown(KeyCode.LeftShift)) {
		locomotor.Running = true;
    }

    if (Input.GetKeyUp(KeyCode.LeftShift)) {
		locomotor.Running = false;
    }

    if (Input.GetMouseButton(1)) {
		acquirer.AcquireFixation();
    }

    if (Input.GetMouseButton(0)) {
		thrower.ThrowRock();
    }
  }

}
