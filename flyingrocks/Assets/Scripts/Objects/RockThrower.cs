using System;
using System.Collections.Generic;
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
	 * Removes an item of a random type from the inventory and launches it.
	 */
	public void Throw()
	{
		List<string> types = acquirer.inventory.Types();

		if (types.Count > 0)
			Throw(types[(new System.Random()).Next(0, (types.Count - 1))]);
	}

	/**
	 * Removes an item from the inventory and launches it.
	 */
	public void Throw(string type)
	{
		GameObject item;

		if (acquirer.inventory.Remove(type, out item)) {
			item.SetActiveRecursively(true);
			launcher.Launch(item);
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
