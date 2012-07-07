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
	 * Our rock launcher.
	 */
	private Launcher launcher;

	/**
	 * The current readied item.
	 */
	private GameObject readiedItem;

	/**
	 * Transient game object to use when representing the readied item.
	 */
	private Transient transientItem;

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
			if (transientItem)
				transientItem.Restore();

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
	}

	/**
	 * Readies the given item. Sets it up to be launched upon the next call to
	 * Throw() and to render upon updates.
	 *
	 * Finds the first Transient object in the hierarchy and use it to represent
	 * the readied item.
	 */
	private void ReadyItem(GameObject item)
	{
		readiedItem = item;

		// Note that we can't use GetComponentInChildren() here because the
		// children components are inactive.
		foreach (Transform child in item.transform) {
			if (transientItem = child.GetComponent<Transient>()) {
				transientItem.Activate();
				transientItem.gameObject.SetActiveRecursively(true);

				break;
			}
		}
	}

	/**
	 * If an item isn't currently readied, then the next item of the
	 * acquired-item type is readied.
	 */
	private void OnAcquisitionOf(Acquirable acquirable)
	{
		// The item isn't readied directly using ReadyItem() because we want it to
		// be pulled from inventory.
		if (!readiedItem)
			ReadyNext(acquirable.type);
	}

	/**
	 * Resets the readied item and its acquirable.
	 */
	private void ResetReadiedItem()
	{
		readiedItem = default(GameObject);
		transientItem = default(Transient);
	}

	/**
	 * Renders the transient item relative to the launcher.
	 */
	private void LateUpdate()
	{
		if (transientItem)
			launcher.Position(transientItem.gameObject);
	}
}
