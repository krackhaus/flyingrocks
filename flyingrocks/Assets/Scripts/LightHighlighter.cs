using UnityEngine;

[RequireComponent(typeof(Light))]

/**
 * A simple highligher that can toggle a light.
 */
public class LightHighlighter : MonoBehaviour, IHighlighter
{
	public void switchOn()
	{
		light.enabled = true;
	}

	public void switchOff()
	{
		light.enabled = false;
	}

	void Start()
	{
		switchOff();
	}
}
