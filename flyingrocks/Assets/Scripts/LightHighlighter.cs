using UnityEngine;

[RequireComponent(typeof(Light))]

/**
 * A simple highligher that can toggle a light.
 */
public class LightHighlighter : MonoBehaviour, IHighlighter
{
	public void SwitchOn()
	{
		light.enabled = true;
	}

	public void SwitchOff()
	{
		light.enabled = false;
	}

	void Start()
	{
		SwitchOff();
	}
}
