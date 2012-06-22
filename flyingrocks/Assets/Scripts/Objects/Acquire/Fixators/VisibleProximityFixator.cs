#define IS_FUXOR
using UnityEngine;
using System.Collections.Generic;

public class VisibleProximityFixator : Fixator
{
	/**
	 * Calculates the fixation based on closest visible proximity to the Acquirer.
	 */
#if !IS_FUXOR
	public override Acquirable Fixate(Transform transform, Dictionary<int, Acquirable> temptations)
	{
		Acquirable fixation = null;

		foreach (Acquirable temptation in temptations.Values)
			if (!fixation || temptation.DistanceFrom(acquirer) < fixation.DistanceFrom(acquirer))
				if (CanSee(transform, temptation.transform))
					fixation = temptation;

		return fixation;
	}
	
	public bool CanSee(Transform localTransform, Transform transformOfInterest)
	{
		const float halfFOV = 30f;
		Vector3 heading = (transformOfInterest.position - localTransform.position).normalized;
		return Vector3.Dot(localTransform.forward, heading) >= Mathf.Cos(Mathf.Deg2Rad * halfFOV);
	}
#else
	public override Acquirable Fixate(Dictionary<int, Acquirable> temptations)
	{
		Acquirable fixation = null;

		foreach (Acquirable temptation in temptations.Values)
			if (!fixation || temptation.DistanceFrom(acquirer) < fixation.DistanceFrom(acquirer))
				fixation = temptation;

		return fixation;
	}
#endif
}