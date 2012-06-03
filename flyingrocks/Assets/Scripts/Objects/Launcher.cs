using UnityEngine;

/**
 * Behavior for a game object that launches other game objects. The bound game
 * object is used as a spawn point and will orient the other object to its
 * current position and rotation before applying force.
 */
public class Launcher : MonoBehaviour
{
	/**
	 * Amount of force with which to launch objects.
	 */
	public float force = 20f;

	/**
	 * The type of force to use when launching objects.
	 */
	public ForceMode forceMode = ForceMode.VelocityChange;

	/**
	 * Orients and launches the given game object.
	 */
	public void Launch(GameObject projectile)
	{
		projectile.transform.position = transform.position;
		projectile.transform.rotation = transform.rotation;
		projectile.rigidbody.AddRelativeForce(Vector3.forward * force, forceMode);
	}
}
