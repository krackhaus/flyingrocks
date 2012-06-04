using System.Collections.Generic;

/**
 * Keeps a finite inventory of objects.
 */
public class Inventory<T>
{
	/**
	 * Inventory dictionary.
	 */
	private Dictionary<string, List<T>> inventory = new Dictionary<string, List<T>>();

	/**
	 * Limits on items by type. Stored as a dictionary for efficiency.
	 */
	private Dictionary<string, int> limits = new Dictionary<string, int>();

	/**
	 * Constructor.
	 */
	public Inventory(InventoryLimit[] inventoryLimits)
	{
		foreach (InventoryLimit item in inventoryLimits)
			limits.Add(item.acquirable, item.limit);
	}

	/**
	 * Attempts to add an item to the inventory. If the inventory limit is
	 * reached, the item is not added and false is returned.
	 */
	public bool Add(string type, T item)
	{
		int limit;
		List<T> typeInventory;

		if (!inventory.TryGetValue(type, out typeInventory))
			typeInventory = new List<T>();

		if (!limits.TryGetValue(type, out limit) || limit > typeInventory.Count) {
			typeInventory.Add(item);
			inventory[type] = typeInventory;

			return true;
		}

		return false;
	}

	/**
	 * Returns whether the inventory contains an item of the given type.
	 */
	public bool Contains(string type)
	{
		List<T> typeInventory;

		return inventory.TryGetValue(type, out typeInventory) && typeInventory.Count > 0;
	}

	/**
	 * Removes an item from the inventory.
	 */
	public bool Remove(string type, out T item)
	{
		List<T> typeInventory;

		if (inventory.TryGetValue(type, out typeInventory) && typeInventory.Count > 0) {
			int index = typeInventory.Count - 1;

			item = typeInventory[index];
			inventory[type].RemoveAt(index);

			return true;
		}
		else {
			item = default(T);
			return false;
		}
	}
}
