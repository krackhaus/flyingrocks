using UnityEngine;
using System.Collections;

/**
 * An object that be acquired. It tempts any nearby object with an Acquirer
 * component that enters its collider.
 */
public class Acquirable : MonoBehaviour
{
	/**
	 * The identifier that should be used to keep track of the acquirable.
	 */
	public int id
	{
		get { return GetInstanceID(); }
	}

	/**
	 * The acquirable type. Just goes off the tag value.
	 */
	public string type
	{
		get { return tag; }
	}
	
	/*
	 * It is good for us to know when these rocks are on the gound,
	 * because if they are then they probably shouldn't be doing damage!
	 */
	private bool isGrounded = true;
	
	public bool IsGrounded
	{
		get { return isGrounded; }
	}

	/**
	 * Returns the acquirable's distance from the given acquirer.
	 */
	public float DistanceFrom(Acquirer acquirer)
	{
		return (acquirer.transform.position - transform.position).magnitude;
	}

	/**
	 * Disables the collider when acquired.
	 */
	void OnAcquisition()
	{
		
		// TODO deal with this.
		//collider.enabled = false;
		isGrounded = false; // unfortunatly, however, I don't know of a good place to set it back to true;
	}

	/**
	 * Keeps track of the collider used to 
	 */
	void OnTriggerEnter(Collider other)
	{
		Acquirer acquirer = other.gameObject.GetComponent<Acquirer>();

		if (acquirer != null)
			acquirer.TemptWith(this);
	}

	/**
	 * Tempts the acquirer while he's within the collider.
	 */
	void OnTriggerStay(Collider other)
	{
		Acquirer acquirer = other.gameObject.GetComponent<Acquirer>();

		if (acquirer != null)
			acquirer.TemptWith(this);
	}

	/**
	 * Tells the acquirer to forget the acquirable when he leaves the collider.
	 */
	void OnTriggerExit(Collider other)
	{
		Acquirer acquirer = other.gameObject.GetComponent<Acquirer>();

		if (acquirer != null)
			acquirer.Forget(this);
	}
}
