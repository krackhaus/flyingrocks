using UnityEngine;

[RequireComponent (typeof(Rigidbody))]

public class Locomotor : MonoBehaviour {
  public float velocity = 10f;
  public float turnRate = 100;
  public float jumpForce = 10f;

  public void GoForward() {
    transform.Translate(Vector3.forward * velocity * Time.deltaTime);
  }

  public void GoBackward() {
    transform.Translate(Vector3.forward * velocity * Time.deltaTime * -1);
  }

  public void TurnRight() {
    transform.Rotate(Vector3.up, turnRate * Time.deltaTime);
  }

  public void TurnLeft() {
    transform.Rotate(Vector3.up, -turnRate * Time.deltaTime);
  }

  public void Jump() {
    rigidbody.AddForce(Vector3.up * jumpForce);
  }
}
