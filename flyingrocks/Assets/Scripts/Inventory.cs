using System.Collections.Generic;

/**
 * Keeps a finite inventory of items.
 */
public class Inventory
{
	/**
	 * Inventory dictionary.
	 */
	private Dictionary<string, int> inventory = new Dictionary<string, int>();

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
	 * Attempts to increment the number of specific items in inventory. If the
	 * inventory limit is reached, the number is not incremented and false is
	 * returned.
	 */
	public bool Increment(string type)
	{
		int limit;
		int typeInventory;

		if (!inventory.TryGetValue(type, out typeInventory))
			typeInventory = 0;

		if (!limits.TryGetValue(type, out limit) || limit > typeInventory++) {
			inventory[type] = typeInventory;

			return true;
		}

		return false;
	}

	public void Decrement(string type)
	{
		inventory[type] = inventory[type] - 1;
	}
}
