using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Badger : MonoBehaviour
{
	public float movementUpdateRate = 1;
	public float eatingUpdateRate = 5;
	public float hearingRange = 5;
	
	List<ObjectOfInterest> objectsOfInterest = new List<ObjectOfInterest>();
	List<Collider> senses = new List<Collider>(5);
	bool[] flags = new bool[4];
	float hungerLevel = 50;
	Transform target;
	
	#region Overhead
	void Awake()
	{
		// Setup Senses so we can detect and react to things in the environment
		senses.Insert(0, transform.FindChild("HearingRange").GetComponent<SphereCollider>());
		((SphereCollider)senses[0]).radius += hearingRange;
		
		Hungry = Foraging = Eating = ReachedTarget = true;
		rigidbody.useGravity = false;
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
		// let's set the material color to red for now, so that we know it's an baddie.
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
			hungerLevel += 0.1f;
			transform.LookAt(target);
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
		/*
		 * Badger will continue to eat until either;
		 * - No longer hungry
		 * - The Object of Interest persists
		 * - (planned) Remains unperterbed
		 */
		ObjectOfInterest ooi = target.gameObject.GetComponent<ObjectOfInterest>();
		while (Hungry && ooi)
		{
			ooi.Eat();
			hungerLevel--;
			if (hungerLevel == 0)
				Hungry = false;
			Debug.Log ("nomnomnom -- hunger level = " +hungerLevel);
			yield return new WaitForSeconds(eatingUpdateRate);
		}
		if (Hungry)
			Forage();
	}
	
	/*
	 * Discover closest Object of Interest and set it as our target
	 * so we can look at and move toward it.
	 */
	void FindClosestObjectOfInterest()
	{
		float lastDistance = Mathf.Infinity;
		CleanObjectsOfInterestList();
		foreach (ObjectOfInterest ooi in objectsOfInterest)
		{
			float distance = Vector3.Distance(transform.position, ooi.transform.position);
			if (distance < lastDistance)
			{
				lastDistance = distance;
				target = ooi.transform;
			}
		}
		ReachedTarget = false;
	}
	
	void CleanObjectsOfInterestList()
	{
		for (int i = 0; i < objectsOfInterest.Count; i++)
		{
			if (objectsOfInterest[i] == null)
				objectsOfInterest.RemoveAt(i);
		}
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
	
	bool Hearing
	{
		get { return senses[0]; }
	}
	#endregion
}