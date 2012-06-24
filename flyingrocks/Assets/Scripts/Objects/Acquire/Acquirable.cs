using UnityEngine;
using System.Collections;

/**
 * An object that be acquired. It tempts any nearby object with an Acquirer
 * component that enters its collider.
 */
public class Acquirable : MonoBehaviour
{
	/**
	 * The actual game object that should be inventoried, assumed to be the
	 * parent game object.
	 */
	public GameObject inventoryObject
	{
		get { return transform.parent.gameObject; }
	}

	/**
	 * The identifier that should be used to keep track of the acquirable.
	 */
	public int id
	{
		get { return GetInstanceID(); }
	}

	/**
	 * Materials to apply to the mesh when rendering the acquirable in a
	 * transient state.
	 */
	public Material[] transientMaterials;

	/**
	 * Mesh to use when rendering the acquirable in a transient state.
	 */
	public Mesh transientMesh;

	/**
	 * The acquirable type.
	 */
	public string type;

	/**
	 * Returns the acquirable's distance from the given acquirer.
	 */
	public float DistanceFrom(Acquirer acquirer)
	{
		return (acquirer.transform.position - transform.position).magnitude;
	}

	/**
	 * Tempts the acquirer while he's within the collider.
	 */
	private void OnTriggerStay(Collider other)
	{
		Acquirer acquirer = other.gameObject.GetComponent<Acquirer>();

		if (acquirer != null)
			acquirer.TemptWith(this);
	}

	/**
	 * Tells the acquirer to forget the acquirable when he leaves the collider.
	 */
	private void OnTriggerExit(Collider other)
	{
		Acquirer acquirer = other.gameObject.GetComponent<Acquirer>();

		if (acquirer != null)
			acquirer.Forget(this);
	}
}
