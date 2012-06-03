using UnityEngine;
using System.Collections;

public class ObjectOfInterest : MonoBehaviour
{
	public enum Density { Low = 2, Average = 4, Good = 6, High = 8, Fuckyeah = 10 };
	public Density density;
	public bool randomizeDensities;
	
	virtual protected void Awake()
	{
		if (randomizeDensities)
		{
			switch(Random.Range(0, 10))
			{
			case(2):
				density = Density.Low;
				break;
			case(4):
				density = Density.Average;
				break;
			case(6):
				density = Density.Good;
				break;
			case(8):
				density = Density.High;
				break;
			case(10):
				density = Density.Fuckyeah;
				break;
			}
		}
	}
}