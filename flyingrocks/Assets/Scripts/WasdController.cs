using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Locomotor))]
[RequireComponent (typeof(Acquirer))]

public class WasdController : MonoBehaviour
{

  private Locomotor locomotor;
  private Acquirer acquirer;

  void Start()
  {
    locomotor = GetComponent<Locomotor>();
    acquirer = GetComponent<Acquirer>();
  }

  void Update()
  {
    if (Input.GetKey(KeyCode.W)) {
      locomotor.GoForward();
    }

    if (Input.GetKey(KeyCode.S)) {
      locomotor.GoBackward();
    }

    if (Input.GetKey(KeyCode.A)) {
      locomotor.TurnLeft();
    }

    if (Input.GetKey(KeyCode.D)) {
      locomotor.TurnRight();
    }

    if (Input.GetKey(KeyCode.G)) {
			acquirer.AcquireFixation();
    }
  }

}
