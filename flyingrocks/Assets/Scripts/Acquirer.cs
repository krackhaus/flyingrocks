using UnityEngine;
using System.Collections.Generic;

public class Acquirer : MonoBehaviour
{
	/**
	 * Types of Acquirable objects that this object can acquire.
	 */
	public List<string> acquires;

	/**
	 * Returns whether the acquirer is tempted by the given acquirable object.
	 */
	public bool isTemptedBy(Acquirable other)
	{
		// TODO implement temptation by multiple objects and choosing between them.
		return acquires.Contains(other.type);
	}
}
