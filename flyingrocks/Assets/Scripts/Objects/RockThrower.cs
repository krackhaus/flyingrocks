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
	public float strength = 20;

	/**
	 * The rock in hand.
	 */
	private GameObject rock;
	
	/* The location to spawn the rock in before it is thrown */
	private Transform rockSpawnLocation;
	
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
		if (rock == null) return;
		rock.SetActiveRecursively(true);
		rockSpawnLocation = transform.Find("RockSpawnLocation").transform;
		rock.transform.position = rockSpawnLocation.position;
		rock.transform.rotation = rockSpawnLocation.rotation;
		rock.rigidbody.AddRelativeForce(Vector3.forward * strength, ForceMode.VelocityChange);
		//rock.rigidbody.AddForce(Vector3.up * strength * 0.25f, ForceMode.Impulse);

		rock = null;

		GetComponent<Acquirer>().inventory.Decrement("Rock");
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
