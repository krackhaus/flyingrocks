using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Locomotor))]
[RequireComponent (typeof(Acquirer))]

public class WasdController : MonoBehaviour
{

  private Locomotor locomotor;
  private Acquirer acquirer;
  private RockThrower thrower;

  void Start()
  {
    locomotor = GetComponent<Locomotor>();
    acquirer = GetComponent<Acquirer>();
    thrower = GetComponent<RockThrower>();
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

    if (Input.GetKeyDown(KeyCode.G)) {
			acquirer.AcquireFixation();
    }

    if (Input.GetKeyDown(KeyCode.T)) {
			thrower.ThrowRock();
    }
  }

}
