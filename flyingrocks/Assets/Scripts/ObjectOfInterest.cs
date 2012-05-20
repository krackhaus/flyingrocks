using UnityEngine;
using System.Collections;

public class ObjectOfInterest : MonoBehaviour
{
	int nibblesRemaining = 2;
	
	public void Eat()
	{
		if (--nibblesRemaining == 0)
			Destroy(gameObject);
	}
	
	public int NibblesRemaining
	{
		set { nibblesRemaining = value; }
	}
}