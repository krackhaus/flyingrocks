using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Acquirer))]

/**
 * Can pick up and throw rocks.
 */
public class InventoryThrower : MonoBehaviour
{
	/**
	 * The associated acquirer.
	 */
	private Acquirer acquirer;

	/**
	 * Mesh renderer used to render the readied item.
	 */
	private MeshFilter itemMeshFilter;

	/**
	 * Mesh renderer used to render the readied item.
	 */
	private MeshRenderer itemRenderer;

	/**
	 * Our rock launcher.
	 */
	private Launcher launcher;

	/**
	 * The current readied item.
	 */
	private GameObject readiedItem;

	/**
	 * Drops the readied item.
	 */
	public void Drop()
	{
		Throw(0);
	}

	/**
	 * Readies an item of a random type from the inventory. Returns whether or
	 * not an item was found.
	 */
	public bool ReadyNext()
	{
		List<string> types = acquirer.inventory.Types();

		if (types.Count > 0)
			return ReadyNext(types[(new System.Random()).Next(0, (types.Count - 1))]);

		return false;
	}

	/**
	 * Readies the next item of the given type from the inventory. Returns
	 * whether or not an item was found and readied.
	 */
	public bool ReadyNext(string type)
	{
		GameObject item;

		bool itemFound = acquirer.inventory.Remove(type, out item);

		if (itemFound)
			ReadyItem(item);

		return itemFound;
	}

	/**
	 * Throws the previously readied item.
	 */
	public bool Throw(float force = 0)
	{
		if (readiedItem) {
			readiedItem.SetActiveRecursively(true);
			launcher.Launch(readiedItem);
			ResetReadiedItem();

			return true;
		}

		return false;
	}

	/**
	 * Gets the Acquirer and Launcher.
	 */
	private void Awake()
	{
		acquirer = GetComponent<Acquirer>();
		launcher = GetComponentInChildren<Launcher>();
		itemRenderer = launcher.GetComponent<MeshRenderer>();
		itemMeshFilter = launcher.GetComponent<MeshFilter>();

		itemRenderer.enabled = false;
	}

	/**
	 * Readies the given item. Sets it up to be launched upon the next call to
	 * Throw() and to render upon updates.
	 */
	private void ReadyItem(GameObject item)
	{
		readiedItem = item;

		// Note that we can't use GetComponentInChildren() here because the
		// children objects are inactive.
		Acquirable acquirable = item.transform.Find("Acquirable").GetComponent<Acquirable>();

		// If the item has a transient mesh set, use it to represent the readied
		// item.
		if (acquirable && acquirable.transientMesh) {
			itemMeshFilter.mesh = acquirable.transientMesh;
			itemRenderer.materials = acquirable.transientMaterials;
			itemRenderer.enabled = true;
		}
	}

	/**
	 * Disables items upon acquisition. If an item isn't currently readied, then
	 * the next item of the acquired-item type is readied.
	 */
	private void OnAcquisitionOf(Acquirable acquirable)
	{
		acquirable.inventoryObject.SetActiveRecursively(false);

		// The item isn't readied directly because we want it to be pulled from
		// inventory.
		if (!readiedItem)
			ReadyNext(acquirable.type);
	}

	/**
	 * Resets the readied item and its acquirable.
	 */
	private void ResetReadiedItem()
	{
		itemRenderer.enabled = false;

		if (itemMeshFilter.mesh) {
			itemMeshFilter.mesh = default(Mesh);

			if (itemRenderer.materials != null)
				Array.Clear(itemRenderer.materials, 0, itemRenderer.materials.Length);
		}

		readiedItem = default(GameObject);
	}
}
