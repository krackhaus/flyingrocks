using UnityEngine;
using System.Collections.Generic;

public class Acquirer : MonoBehaviour
{
	/**
	 * Types of Acquirable objects that this object can acquire.
	 */
	public List<string> acquires;

	/**
	 * Current temptations. The object pool from which a fixation may result.
	 */
	private Dictionary<int, Acquirable> temptations = new Dictionary<int, Acquirable>();

	/**
	 * Current fixation. The acquirable that has the acquirer's full attention
	 * and should be highlighted.
	 */
	private Acquirable fixation;

	/**
	 * Removes the acquirable from the current temptations.
	 */
	public bool ForgetAbout(Acquirable acquirable)
	{
		return temptations.Remove(acquirable.id);
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
	 * Calculate the current fixation by shortest distance from the acquirer.
	 */
	private void CalculateFixation()
	{
		fixation = null;

		foreach (Acquirable temptation in temptations.Values)
			if (fixation == null || temptation.DistanceFrom(this) < fixation.DistanceFrom(this))
				fixation = temptation;
	}

	/**
	 * Calculate fixation on every frame.
	 */
	private void Update() { CalculateFixation(); }


	/**
	 * Highlight fixation.
	 */
	void LateUpdate()
	{
		if (fixation != null)
			fixation.Highlight();
	}
}
