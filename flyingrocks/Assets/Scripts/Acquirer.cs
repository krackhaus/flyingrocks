using UnityEngine;
using System.Collections.Generic;

public class Acquirer : MonoBehaviour
{
	/**
	 * Types of Acquirable objects that this object can acquire.
	 */
	public List<string> acquires;

	/**
	 * Limits on inventory items. Unfortunately Unity won't expose a Dictionary,
	 * so we have to resort to a List of KeyValuePairs.
	 */
	public InventoryLimit[] inventoryLimits;

	/**
	 * Current fixation. The acquirable that the acquirer is currently most
	 * interested in.
	 */
	private Acquirable fixation, oldFixation;

	/**
	 * Current inventory.
	 */
	// TODO change back to private
	public Inventory inventory;

	/**
	 * Current temptations. The object pool from which a fixation may result.
	 */
	private Dictionary<int, Acquirable> temptations = new Dictionary<int, Acquirable>();

	/**
	 * Broadcasts an intent to acquire the current fixation to both the
	 * acquirable's game object and this game object. The OnAcquisition message
	 * will have a different signature dependending on the target, the
	 * acquirable implementation receiving no object and this game object's
	 * implementation receiving the acquirable.
	 *
	 * Note that the fixation can only be acquired if the inventory will allow
	 * it. Otherwise, OnFailedAcquisitionOf and OnFailedAcquisition are sent
	 * instead.
	 */
	public void AcquireFixation()
	{
		if (fixation != null) {
			if (inventory.Increment(fixation.type)) {
				fixation.gameObject.SendMessage("OnAcquisition");
				gameObject.SendMessage("OnAcquisitionOf", fixation);

				Forget(fixation);
			}
			else {
				fixation.gameObject.SendMessage("OnFailedAcquisition",
						SendMessageOptions.DontRequireReceiver);
				gameObject.SendMessage("OnFailedAcquisitionOf", fixation,
						SendMessageOptions.DontRequireReceiver);
			}
		}
	}

	/**
	 * Removes the current fixation.
	 */
	public void Forget()
	{
		if (fixation != null)
			Forget(fixation);
	}

	/**
	 * Removes the acquirable from the current temptations.
	 */
	public void Forget(Acquirable acquirable)
	{
		temptations.Remove(acquirable.id);

		if (fixation != null && fixation.id == acquirable.id)
			fixation = null;
	}

	/**
	 * Tempts the acquirer with the given acquirable and returns whether the
	 * acquirer is tempted.
	 */
	public bool TemptWith(Acquirable acquirable)
	{
		if (acquires.Contains(acquirable.type)) {
			if (!temptations.ContainsKey(acquirable.id))
				temptations.Add(acquirable.id, acquirable);

			return true;
		}

		return false;
	}

	/**
	 * Initialize inventory.
	 */
	private void Awake()
	{
		inventory = new Inventory(inventoryLimits);
	}

	/**
	 * Calculate the current fixation by shortest distance from the acquirer.
	 */
	private void CalculateFixation()
	{
		foreach (Acquirable temptation in temptations.Values)
			if (!fixation || temptation.DistanceFrom(this) < fixation.DistanceFrom(this))
				fixation = temptation;
		
		if (oldFixation != fixation)
			UpdateFixations();
	}
	
	void UpdateFixations()
	{
		if (oldFixation)
			oldFixation.gameObject.SendMessage("OnFixationDrop", this, SendMessageOptions.DontRequireReceiver);
		if (fixation && !fixation.light.enabled)
			fixation.gameObject.SendMessage("OnFixation", this, SendMessageOptions.DontRequireReceiver);
		oldFixation = fixation;
	}

	/**
	 * Calculate the fixation on every frame.
	 */
	private void Update()
	{
		CalculateFixation();
	}


	/**
	 * Broadcast to the acquirable's game object about our fixation.
	private void LateUpdate()
	{
		if (fixation != null)
			fixation.gameObject.SendMessage("OnFixation", this, SendMessageOptions.DontRequireReceiver);
	}
	 */
}
