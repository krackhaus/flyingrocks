using UnityEngine;
using System.Collections;

public class Food : ObjectOfInterest
{
	public enum Type { BadgerNoms };
	public Type typeOfFood;
	
	override protected void Awake()
	{
		density = Density.Average;
		renderer.material.color = Color.green;
		transform.parent = GameObject.Find("Badger Noms").transform;
		
		base.Awake();
	}
	
	public void Eat()
	{
		if (--density == 0)
			Destroy(gameObject);
	}
}
