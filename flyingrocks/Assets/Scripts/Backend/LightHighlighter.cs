using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Light))]

/**
 * A simple highligher that can toggle a light.
 */
public class LightHighlighter : MonoBehaviour, IHighlighter
{
	public float startingIntensity = 0.5f;
	public float fadeDelay = 0.5f; // values greater than 0 and less than 1 will fade the light out.
	
	void Start()
	{
		ResetLight();
	}
	
	void ResetLight()
	{
		light.intensity = startingIntensity;
		light.enabled = false;
	}
	
	public void SwitchOn()
	{
		light.enabled = true;
	}

	public void SwitchOff()
	{
		if (fadeDelay > 0 && fadeDelay < 1)
			StartCoroutine(FadeOff());
		else
			light.enabled = false;
	}
	
	IEnumerator FadeOff()
	{
		float fader = (startingIntensity * fadeDelay) * 0.25f;
		while (light.intensity > 0)
		{
			light.intensity -= fader;
			yield return new WaitForSeconds(Time.deltaTime);
		}
		ResetLight();
	}
}
