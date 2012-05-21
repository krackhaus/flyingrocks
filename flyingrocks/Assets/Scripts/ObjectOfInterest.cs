using UnityEngine;
using System.Collections;

public class ObjectOfInterest : MonoBehaviour
{
	public enum Density { Low = 2, Medium = 5, High = 10 };
	public enum Type { Food, Other };
	public Type type;
	public Density density;
	
	void Awake()
	{
		if (type == Type.Food)
		{
			name = "nom";
			renderer.material.color = Color.green;
		}
	}
	
	public void Eat()
	{
		if (--density == 0)
			Destroy(gameObject);
	}
}