using UnityEngine;
using System.Collections;

public class DamageIndicator : MonoBehaviour
{
	public Color damageColor = Color.red;
	
	public void DoDamage(Renderer renderer, int timesToFlash, float frequency)
	{
		StartCoroutine(FlashDamage(renderer, timesToFlash, frequency));
	}
	
	IEnumerator FlashDamage(Renderer renderer, int timesToFlash, float frequency)
	{
		Material mat = renderer.material;
		Color naturalColor = mat.color;
		int timesFlashed = 0;
		while (timesToFlash * 2> timesFlashed)
		{
			if (naturalColor == mat.color)
				mat.color = damageColor;
			else
				mat.color = naturalColor;
			timesFlashed++;
			yield return new WaitForSeconds(frequency);
		}
		mat.color = naturalColor;
	}
}
