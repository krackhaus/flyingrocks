using System.Collections.Generic;

/**
 * Fixator delegate.
 */
public delegate Acquirable FixatorDelegate(Dictionary<int, Acquirable> temptations);

/**
 * Base Fixator class.
 */
public abstract class Fixator
{
	/**
	 * Available fixator types.
	 */
	public enum Type { Proximity };

	/**
	 * The acquirer.
	 */
	public Acquirer acquirer;

	/**
	 * Factory method for instantiating Fixators and returning their Fixate
	 * methods.
	 */
	public static FixatorDelegate Delegate(Type type, Acquirer acq)
	{
		Fixator fixator;

		switch (type) {
			// Proximity is the default type
			default:
				fixator = new ProximityFixator();
				break;
		}

		fixator.acquirer = acq;

		return fixator.Fixate;
	}

	/**
	 * Calculates the fixation out of all given Acquirable objects.
	 */
	public abstract Acquirable Fixate(Dictionary<int, Acquirable> temptations);
}
