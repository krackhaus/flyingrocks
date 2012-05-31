using UnityEngine;
using System.Collections;

/**
 * An object that be acquired. It tempts any nearby object with an Acquirer
 * component that enters its collider.
 */
public class Acquirable : MonoBehaviour
{

	private IHighlighter highlighter;

	/**
	 * The acquirable type. Just goes off the tag value.
	 */
	public string type
	{
		get { return tag; }
	}

	void Awake()
	{
		highlighter = GetComponent(typeof(IHighlighter)) as IHighlighter;
	}

  /**
	 * Tempts Acquirer game objects that are within the collider.
   */
  void OnTriggerStay(Collider other)
  {
		Acquirer acquirer = other.gameObject.GetComponent<Acquirer>();

		if (acquirer != null)
			if (acquirer.isTemptedBy(this))
				highlighter.SwitchOn();
			else
				highlighter.SwitchOff();
	}

  void OnTriggerExit(Collider other)
  {
		Acquirer acquirer = other.gameObject.GetComponent<Acquirer>();

		if (acquirer != null)
			highlighter.SwitchOff();
	}
}
