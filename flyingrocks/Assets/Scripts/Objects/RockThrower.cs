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
	private Transform launcherTransform;
	
	/* The Rock Mesh (and Material for it) to display while carrying a rock */
	public Material rockMaterial;
	public Mesh rockMesh;
	
	/**
	 * Drops rock.
	 */
	public void DropRock()
	{
		GameObject rock;

		if (acquirer.inventory.Remove("Rock", out rock)) {
			rock.SetActiveRecursively(true);
			launcher.Launch(rock, 0);
		}
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
		launcherTransform = launcher.transform;
	}

	/**
	 * Disables the rock upon acquisition.
	 */
	private void OnAcquisitionOf(Acquirable acquirable)
	{
		acquirable.inventoryObject.SetActiveRecursively(false);
	}
	
	/* Renders the rock that the player is carrying - use of a MeshRenderer would be possible too */
	private void Update()
	{
		DrawRockInHand();
	}
	
	private void DrawRockInHand()
	{
		if (rockMesh && acquirer.inventory.Contains("Rock"))
			Graphics.DrawMesh(rockMesh, launcherTransform.position, launcherTransform.rotation, rockMaterial, 0);
	}
}
