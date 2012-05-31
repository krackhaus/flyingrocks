using UnityEngine;

[RequireComponent(typeof(Acquirable))]

public class AcquirableHighlighter : MonoBehaviour
{
	/**
	 * The highlighter used to... highlight the acquirable.
	 */
	public IHighlighter highlighter;

	/**
	 * Finds the highlighter script component.
	 */
	void Awake()
	{
		highlighter = GetComponent(typeof(IHighlighter)) as IHighlighter;
	}

	/**
	 * Switches off the highlighter when acquired.
	 */
	void OnAcquisition()
	{
		highlighter.SwitchOff();
	}

	/**
	 * Highlights the acquirable when an acquirer is fixated on it.
	 */
	void OnFixation(Acquirer acquirer)
	{
		highlighter.SwitchOn();
	}

	/**
	 * Switch the highlighter back off on every update.
	 */
	void Update()
	{
		highlighter.SwitchOff();
	}
}
