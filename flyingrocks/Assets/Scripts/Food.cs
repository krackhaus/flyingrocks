using UnityEngine;
using System.Collections;

public class Food : ObjectOfInterest
{
	public enum Type { BadgerNoms };
	public Type typeOfFood;
	
	void Awake()
	{
		renderer.material.color = Color.green;
		transform.parent = GameObject.Find("World").transform;
	}
	
	public void Eat()
	{
		if (--density == 0)
			Destroy(gameObject);
	}
}
