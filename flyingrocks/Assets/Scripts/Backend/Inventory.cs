using System.Collections.Generic;

/**
 * Keeps a finite inventory of objects.
 */
public class Inventory<T>
{
	/**
	 * Global count for all items.
	 */
	private int count;

	/**
	 * Inventory dictionary.
	 */
	private Dictionary<string, List<T>> inventory = new Dictionary<string, List<T>>();

	/**
	 * Limits on items by type. Stored as a dictionary for efficiency.
	 */
	private Dictionary<string, int> limits = new Dictionary<string, int>();

	/**
	 * Global item limit.
	 */
	private int limit;

	/**
	 * Constructor.
	 */
	public Inventory(InventoryLimit[] inventoryLimitsByType, int inventoryLimit)
	{
		foreach (InventoryLimit item in inventoryLimitsByType)
			limits.Add(item.acquirable, item.limit);

		limit = inventoryLimit;
	}

	/**
	 * Attempts to add an item to the inventory. If the inventory limit is
	 * reached, the item is not added and false is returned.
	 */
	public bool Add(string type, T item)
	{
		int typeLimit;
		List<T> typeInventory;

		if (!inventory.TryGetValue(type, out typeInventory))
			typeInventory = new List<T>();

		if ((!limits.TryGetValue(type, out typeLimit) || typeLimit > typeInventory.Count)
				&& (limit == 0 || limit > count)) {

			typeInventory.Add(item);
			inventory[type] = typeInventory;

			count++;

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
	 * Returns the inventory for the given type.
	 */
	public int NumberOf(string type)
	{
		List<T> typeInventory;

		if (inventory.TryGetValue(type, out typeInventory))
			return typeInventory.Count;
		else
			return 0;
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

			count--;

			return true;
		}
		else {
			item = default(T);
			return false;
		}
	}

	/**
	 * Returns all types of objects that are currently in inventory.
	 */
	public List<string> Types()
	{
		List<string> types = new List<string>();

		foreach (KeyValuePair<string, List<T>> item in inventory)
			if (item.Value.Count > 0)
				types.Add(item.Key);

		return types;
	}
}
