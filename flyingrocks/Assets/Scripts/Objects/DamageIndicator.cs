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
		material = transform.GetComponentInChildren<Renderer>().material;
		naturalColor = material.color;
	}
	
	public IEnumerator FlashDamage()
	{
		int timesFlashed = 0;
		while ((timesToFlash * 2) > timesFlashed)	// use of magic no.2 allows for 'timesToFlash' value to indicate on/off cycles.
		{
			if (material.color != damageColor)
				material.color = damageColor;
			else
				ResetMaterial();
			timesFlashed += 1;
			yield return new WaitForSeconds(frequency);
		}
		ResetMaterial();
	}
	
	// This is a patch for a bug where the material color remained with the damage color on.
	public void ResetMaterial()
	{
		material.color = naturalColor;
	}
}
