using System.Collections.Generic;

public class ProximityFixator : Fixator
{
	/**
	 * Calculates the fixation based on closest proximity to the Acquirer.
	 */
	public override Acquirable Fixate(Dictionary<int, Acquirable> temptations)
	{
		Acquirable fixation = null;

		foreach (Acquirable temptation in temptations.Values)
			if (!fixation || temptation.DistanceFrom(acquirer) < fixation.DistanceFrom(acquirer))
				fixation = temptation;

		return fixation;
	}
}
