using UnityEngine;

[RequireComponent(typeof(Acquirer))]

/**
 * Can pick up and throw rocks.
 */
public class RockThrower : MonoBehaviour
{
	/**
	 * The strength of the rock thrower.
	 */
	public float strength;

	/**
	 * The rock in hand.
	 */
	private GameObject rock;

	/**
	 * Drops rock.
	 */
	public void DropRock()
	{
	}

	/**
	 * Chucks it.
	 */
	public void ThrowRock()
	{
		//rock.transform.position = transform.position + (transform.position.forward * 2);
		//rock.rigidbody.AddForce(transform.position.forward * strength, ForceMode.VelocityChange);
	}

	/**
	 * Places acquired rock in hand.
	 */
	private void OnAcquisitionOf(Acquirable acquirable)
	{
		// NOTE The actual rock game object is the parent of the acquirable one.
		rock = acquirable.transform.parent.gameObject;

		// For now, we'll just disable the rock game object.
		rock.SetActiveRecursively(false);
	}

	/**
	 * TODO Render rock in hand.
	 */
	private void Update()
	{
	}
}
