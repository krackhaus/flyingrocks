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
	 * Relative torque to add to the object when launched.
	 */
	public Vector3 torque;

	/**
	 * The type of force to use when applying torque.
	 */
	public ForceMode torqueForceMode = ForceMode.VelocityChange;

	/**
	 * Positions and launches the given game object using the configured amount
	 * of force.
	 */
	public void Launch(GameObject projectile)
	{
		Launch(projectile, force);
	}
	
	/**
	 * Positions and launches the given game object using the given amount of
	 * force.
	 */
	public void Launch(GameObject projectile, float force)
	{
		Position(projectile);
		projectile.rigidbody.AddRelativeForce(Vector3.forward * force, forceMode);
		projectile.rigidbody.AddRelativeTorque(torque, torqueForceMode);
	}

	/**
	 * Position the given game object to where the launcher is.
	 */
	public void Position(GameObject projectile)
	{
		projectile.transform.position = transform.position;
		projectile.transform.rotation = transform.rotation;
	}
}
