using UnityEngine;

[RequireComponent(typeof(Acquirer))]

/**
 * Can pick up and throw rocks.
 */
public class RockThrower : MonoBehaviour
{
	/**
	 * The associated acquirer.
	 */
	private Acquirer acquirer;

	/**
	 * Our rock launcher.
	 */
	private Launcher launcher;

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
	 * Chucks the rock and decrements the acquirer's inventory.
	 */
	public void ThrowRock()
	{
		if (rock == null) return;

		// Re-enable the rock, and launch it.
		rock.SetActiveRecursively(true);
		launcher.Launch(rock);

		rock = null;
		acquirer.inventory.Decrement("Rock");
	}

	/**
	 * Gets the Acquirer and Launcher.
	 */
	private void Awake()
	{
		acquirer = GetComponent<Acquirer>();
		launcher = GetComponentInChildren<Launcher>();
	}

	/**
	 * Places acquired rock in hand.
	 */
	private void OnAcquisitionOf(Acquirable acquirable)
	{
		// The actual rock game object is the parent of the acquirable one.
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
