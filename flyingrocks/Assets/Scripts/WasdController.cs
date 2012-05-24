using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Locomotor))]

public class WasdController : MonoBehaviour
{

  private Locomotor locomotor;

  void Start()
  {
    locomotor = GetComponent<Locomotor>();
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
  }

}
