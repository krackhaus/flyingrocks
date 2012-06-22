using UnityEngine;
using System;

[RequireComponent (typeof(Rigidbody))]
[ExecuteInEditMode]

/**
 * Scales the mass of the attached Rigidboy, based on a given density.
 */
public class MassScaler : MonoBehaviour
{
	/**
	 * The density of the object from which to calculate the mass.
	 */
	public float density = 0f;

	/**
	 * Upper bound for mass. Set to 10f by default, as is recommended in Unity.
	 */
	public float maximumMass = 10f;

	/**
	 * Lower bound for mass. Set to 0.1f by default, as is recommended in Unity.
	 */
	public float minimumMass = 0.1f;

#if ! UNITY_EDITOR
	void Awake()
	{
		// Don't stick around in play mode.
		Destroy(this);
	}
#endif

	void Update()
	{
		rigidbody.SetDensity(density);

		// Unity will not update the inspector unless we set the member directly.
		// We also want to enforce our bounds.
		rigidbody.mass = Math.Min(Math.Max(rigidbody.mass, minimumMass), maximumMass);
	}
}
