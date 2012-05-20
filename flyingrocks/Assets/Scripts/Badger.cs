using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Badger : MonoBehaviour
{
	public float movementUpdateRate = 1;
	public float eatingUpdateRate = 5;
	
	List<ObjectOfInterest> objectsOfInterest = new List<ObjectOfInterest>();
	bool[] flags = new bool[4];
	float hungerLevel = 50;
	Transform target;
	
	#region Overhead
	void Awake()
	{
		Hungry = Foraging = Eating = ReachedTarget = true;
	}
	
	void Start()
	{
		// Generate a list of all Objects of Interest with which to attract Badger to.
		foreach (GameObject go in GameObject.FindGameObjectsWithTag("ObjectOfInterest"))
		{
			Component c = go.GetComponent<ObjectOfInterest>();
			if (c != null)
				objectsOfInterest.Add(c as ObjectOfInterest);
		}
		
		//Bootstrap code
		SetColors();
		Forage();
	}
	
	void SetColors()
	{
		renderer.material.color = Color.red;
	}
	
	void OnTriggerEnter(Collider trigger)
	{
		Debug.Log ("collision with " + trigger.name);
		if (trigger.transform.Equals(target))
			ReachedTarget = true;
	}
	#endregion
	#region Behaviour
	void Forage()
	{
		if (target == null)
			FindClosestObjectOfInterest();
		
		transform.LookAt(target);
		Foraging = true;
		StartCoroutine(MoveTowardTarget());
	}
	
	IEnumerator MoveTowardTarget()
	{
		/*
		 * Badger will stop moving when he is too hungry,
		 * or when he has reached the target.
		 */
		while (!ReachedTarget && hungerLevel < 150)
		{
			hungerLevel++;
			if (hungerLevel < 100)
				transform.Translate(Vector3.forward);
			else
				transform.Translate(Vector3.forward/2);
			yield return new WaitForSeconds(movementUpdateRate);
		}
		Foraging = false;
		if (Hungry)
			StartCoroutine(Eat());
	}
	
	IEnumerator Eat()
	{
		while (Hungry)
		{
			target.gameObject.GetComponent<ObjectOfInterest>().Eat();
			hungerLevel--;
			Debug.Log ("eating, hunger level = " +hungerLevel);
			yield return new WaitForSeconds(eatingUpdateRate);
		}
		Hungry = false;
	}
	
	/*
	 * Discover closest Object of Interest and set it as our target
	 * so we can look at and move toward it.
	 */
	void FindClosestObjectOfInterest()
	{
		float lastDistance = Mathf.Infinity;
		foreach (ObjectOfInterest ooi in objectsOfInterest)
		{
			float distance = Vector3.Distance(transform.position, ooi.transform.position);
			if (distance < lastDistance)
				target = ooi.transform;
		}
		ReachedTarget = false;
	}
	#endregion
	#region Getters and Setters
	public bool Hungry
	{
		get { return flags[0]; }
		set { flags[0] = value; }
	}
			
	public bool Foraging
	{
		get { return flags[1]; }
		set { flags[1] = value; }
	}
	
	public bool Eating
	{
		get { return flags[2]; }
		set { flags[2] = value; }
	}
	
	public bool ReachedTarget
	{
		get { return flags[3]; }
		set { flags[3] = value; }
	}
	#endregion
}