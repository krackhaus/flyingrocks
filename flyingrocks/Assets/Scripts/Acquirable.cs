using UnityEngine;
using System.Collections;

/**
 * An object that be acquired. It tempts any nearby object with an Acquirer
 * component that enters its collider.
 */
public class Acquirable : MonoBehaviour
{
	/**
	 * The highlighter used to... highlight the acquirable.
	 */
	public IHighlighter highlighter;

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

	/**
	 * Returns the acquirable's distance from the given acquirer.
	 */
	public float DistanceFrom(Acquirer acquirer)
	{
		return (acquirer.transform.position - transform.position).magnitude;
	}

	/**
	 * Highlights the acquirable.
	 */
	public void Highlight()
	{
		highlighter.SwitchOn();
	}

	/**
	 * Finds the highlighter script component.
	 */
	void Awake()
	{
		highlighter = GetComponent(typeof(IHighlighter)) as IHighlighter;
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
			acquirer.ForgetAbout(this);
	}

	/**
	 * Switch the highlighter back off on every update. It's up to the acquirer
	 * to highlight the acquirable if he wants it.
	 */
	void Update()
	{
		highlighter.SwitchOff();
	}
}
