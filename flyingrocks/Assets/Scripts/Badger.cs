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
	bool[] flags = new bool[6];
	float hungerLevel = 50;
	Transform target;
	
	#region Overhead
	void Awake()
	{
		transform.parent = GameObject.Find("Generated Enemies").transform;
		renderer.material.color = Color.red;
		rigidbody.freezeRotation = true;
		rigidbody.useGravity = false;
		
		// Setup Senses so we can detect and react to things in the environment
		senses.Insert(0, transform.FindChild("HearingRange").GetComponent<SphereCollider>());
		((SphereCollider)senses[0]).radius += hearingRange;
		
		// Initialize Flags
		Hungry = ReachedTarget = true;
		Active = Tired = Eating = Foraging = false;
		
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
		
		//bootstrap
		MakeDecision();
	}
	
	void OnTriggerEnter(Collider trigger)
	{
		if (trigger.transform.Equals(target))
			ReachedTarget = true;
	}
	
	void OnTriggerExit(Collider trigger)
	{
		if (trigger.transform.Equals(target))
			StartCoroutine(MoveTowardTarget());
	}
	#endregion
	#region Behaviour
	void Forage()
	{
		FindClosestFood();
		if (target != null)
		{
			Foraging = true;
			StartCoroutine(MoveTowardTarget());
		}
	}
	
	void Roam()
	{
		Roaming = true;
		target = GameObject.FindWithTag("World").GetComponent<SpawnObjects>().GetRandomTransformOnGrid();
		StartCoroutine(MoveTowardTarget());
	}
	
	void Sleep()
	{
		Active = false;
		//...
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
		Active = false;
		MakeDecision();
	}
	
	/*
	 * VERY simple desicion making...more to come later.
	 */
	void MakeDecision()
	{
		StopAllCoroutines();
		if (Hungry)
		{
			if (ReachedTarget)
				StartCoroutine(Eat());
			else
				Forage();
		}
		else if (!Tired)
			Roam();
		else
			Sleep();
	}
	
	IEnumerator Eat()
	{
		Eating = true;
		/*
		 * Badger will continue to eat until either;
		 * - No longer hungry
		 * - The Object of Interest is Destroyed
		 * - (planned) Remains unperterbed
		 */
		Food food = null;
		try
		{
			food = target.gameObject.GetComponent<Food>();
		}
		catch
		{
			FindClosestFood();
		}
		while (Hungry && food)
		{
			food.Eat();
			hungerLevel--;
			if (hungerLevel == 0)
				Hungry = false;
			Debug.Log (name +": nomnoms -- hunger level = "+ hungerLevel);
			yield return new WaitForSeconds(eatingUpdateRate);
		}
		Eating = false;
		MakeDecision();
	}
	#endregion
	#region Helpers
	/*
	 * Discover closest Object of Interest and set it as our target
	 * so we can look at and move toward it.
	 */
	void FindClosestFood()
	{
		float lastDistance = Mathf.Infinity;
		CleanObjectsOfInterestList();
		try
		{
			foreach (ObjectOfInterest ooi in objectsOfInterest)
			{
				if (ooi.GetType().Equals(typeof(Food)))
				{
					float distance = Vector3.Distance(transform.position, ooi.transform.position);
					if (distance < lastDistance)
					{
						lastDistance = distance;
						target = ooi.transform;
					}
				}
				else
					objectsOfInterest.Remove(ooi);
			}
		}
		catch
		{
			FindClosestFood();
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
	
	public bool Roaming
	{
		get { return flags[3]; }
		set { flags[3] = value; }
	}
	
	public bool Active
	{
		get { return Roaming & Foraging; }
		set { Roaming = Foraging = value; }
	}
	
	public bool ReachedTarget
	{
		get { return flags[4]; }
		set { flags[4] = value; }
	}
	
	public bool Tired
	{
		get { return flags[5]; }
		set { flags[5] = value; }
	}
	
	bool Hearing
	{
		get { return senses[0]; }
	}
	#endregion
}