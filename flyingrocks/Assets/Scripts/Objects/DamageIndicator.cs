using UnityEngine;
using System.Collections;

public class DamageIndicator : MonoBehaviour
{
	public Color damageColor = Color.red;
	public int timesToFlash = 2;
	public float frequency = 0.25f;
	
	private Color naturalColor;
	private Material material;
	
	void Awake()
	{
		naturalColor = transform.GetComponentInChildren<Renderer>().material.color;
		material = transform.GetComponentInChildren<Renderer>().material;
	}
	
	public IEnumerator FlashDamage()
	{
		int timesFlashed = 0;
		while (timesToFlash * 2 > timesFlashed)	// use of magic no.2 allows for 'timesToFlash' value to indicate on/off cycles.
		{
			if (naturalColor == material.color)
				material.color = damageColor;
			else
				material.color = naturalColor;
			timesFlashed++;
			yield return new WaitForSeconds(frequency);
		}
		material.color = naturalColor;
	}
}
