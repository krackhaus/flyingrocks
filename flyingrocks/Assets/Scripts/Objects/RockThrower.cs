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
	 * Drops rock.
	 */
	public void DropRock()
	{
	}

	/**
	 * Removes a rock from the inventory and launches it.
	 */
	public void ThrowRock()
	{
		GameObject rock;

		if (acquirer.inventory.Remove("Rock", out rock)) {
			rock.SetActiveRecursively(true);
			launcher.Launch(rock);
		}
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
	 * Disables the rock upon acquisition.
	 */
	private void OnAcquisitionOf(Acquirable acquirable)
	{
		acquirable.inventoryObject.SetActiveRecursively(false);
	}

	/**
	 * TODO Render rock in hand.
	 */
	private void Update()
	{
	}
}
