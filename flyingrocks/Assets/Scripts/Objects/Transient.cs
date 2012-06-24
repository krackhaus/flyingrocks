using UnityEngine;

/**
 * Used to identify and retrieve the representation of a game object for use
 * in transient modes, e.g. when carrying an object, etc.
 */
public class Transient : MonoBehaviour
{
	/**
	 * Backed up transform members.
	 */
	private Vector3 originalPosition;
	private Quaternion originalRotation;

	/**
	 * Sets the game object and all children objects to active, and backs up the
	 * object transform so that it may be repositioned. The transform object is
	 * then returned.
	 */
	public Transform Activate()
	{
		originalPosition = transform.position;
		originalRotation = transform.rotation;

		return transform;
	}

	/**
	 * Restores the previously backed up transform.
	 */
	public void Restore()
	{
		transform.position = originalPosition;
		transform.rotation = originalRotation;
	}
}
