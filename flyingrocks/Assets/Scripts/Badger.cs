using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Badger : MonoBehaviour
{
	public float decisionTimer = 3;
	public float movementUpdateRate = 1;
	public float eatingUpdateRate = 5;
	public float hearingRange = 5;
	
	List<ObjectOfInterest> objectsOfInterest = new List<ObjectOfInterest>();
	List<Collider> senses = new List<Collider>(5);
	bool[] flags = new bool[6];
	float hungerLevel = 50;
	Transform ooiTarget, roamTarget = null;
	
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
		Hungry = true;
		Active = Tired = Eating = Foraging = KnowsWhereFoodIs = false;
		
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
	
	void OnTriggerExit(Collider trigger)
	{
		if (trigger.transform.Equals(Target))
			StartCoroutine(MoveTowardTarget(false));
	}
	#endregion
	#region Behaviour
	IEnumerator Forage()
	{
		Debug.Log (name +": foraging");
		yield return StartCoroutine(MoveTowardTarget(false));
		StartCoroutine(Eat());
	}
	
	IEnumerator Roam(bool foraging)
	{
		Debug.Log (name + (foraging?": looking for food":" looking around"));
		roamTarget = GetWaypoint();
		yield return StartCoroutine(MoveTowardTarget(foraging));
		if (ReachedTarget && foraging)
			StartCoroutine(Eat());
		else
			MakeDecision();
	}
	
	void Sleep()
	{
		Debug.Log (name +": sleeping");
		Invoke("MakeDecision", decisionTimer * 10);
	}
	
	IEnumerator MoveTowardTarget(bool foraging)
	{
		while (Target && !ReachedTarget && hungerLevel < 150)
		{
			hungerLevel += 0.1f;
			transform.LookAt(Target);
			if (foraging)
			{
				if(FindClosestFood())
				{
					StopCoroutine("MoveTowardTarget");
					Forage();
				}
			}
			if (hungerLevel < 100)
				transform.Translate(Vector3.forward);
			else
				transform.Translate(Vector3.forward/2);
			yield return new WaitForSeconds(movementUpdateRate);
		}
	}
	
	IEnumerator Eat()
	{
		Eating = true;
		Food food = null;
		try
		{
			food = ooiTarget.gameObject.GetComponent<Food>();
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
	#region AI
	/*
	 * VERY simple desicion making...more to come later.
	 */
	void MakeDecision()
	{
		//Debug.Log (name +" making decision");
		Active = false;
		if (Hungry && FindClosestFood())
			StartCoroutine(Forage());
		else if (!Tired)
			StartCoroutine(Roam(true));
		else
		{
			if (Random.value > 0.75)
			{
				Debug.Log (name +": thinking for "+ decisionTimer +"seconds");
				Invoke("MakeDecision", decisionTimer);
			}
			else
				Sleep();
		}
	}
	#endregion
	#region Helpers
	/*
	 * Discover closest Object of Interest and set it as our target
	 * so we can look at and move toward it.
	 */
	bool FindClosestFood()
	{
		bool returnValue = false;
		float lastDistance = Mathf.Infinity;
		CleanObjectsOfInterestList();
		try
		{
			foreach (ObjectOfInterest ooi in objectsOfInterest)
			{
				if (ooi.GetType().Equals(typeof(Food)))
				{
					if (!CanSee(ooi.transform)) continue;
					float distance = Vector3.Distance(transform.position, ooi.transform.position);
					if (distance < lastDistance)
					{
						lastDistance = distance;
						ooiTarget = ooi.transform;
						returnValue = true;
					}
				}
				else
					objectsOfInterest.Remove(ooi);
			}
			return KnowsWhereFoodIs = returnValue;
		}
		catch
		{
			FindClosestFood();
		}
		return KnowsWhereFoodIs = returnValue;
	}
	
	bool CanSee(Transform transformOfInterest)
	{
		const float halfFOV = 30f;
		Vector3 heading = (transformOfInterest.position - transform.position).normalized;
		return Vector3.Dot(transform.forward, heading) >= Mathf.Cos(Mathf.Deg2Rad * halfFOV);
	}
	
	void CleanObjectsOfInterestList()
	{
		for (int i = 0; i < objectsOfInterest.Count; i++)
		{
			if (objectsOfInterest[i] == null)
				objectsOfInterest.RemoveAt(i);
		}
	}
	
	Transform GetWaypoint()
	{
		if (roamTarget != null)
		{
			print (roamTarget +", "+ roamTarget.gameObject.name +", "+ roamTarget.gameObject.tag);
			Destroy(roamTarget.gameObject);
		}
		Transform t = GameObject.FindWithTag("World").GetComponent<SpawnObjects>().GetRandomTransformOnGrid();
		Vector3 tpos = t.position;
		tpos.y += transform.lossyScale.y/2;
		t.position = tpos;
		t.gameObject.tag = "Target";
		return t;
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
	
	public bool KnowsWhereFoodIs
	{
		get { return flags[4]; }
		set { flags[4] = value; }
	}
	
	public bool Tired
	{
		get { return flags[5]; }
		set { flags[5] = value; }
	}
	
	public bool Active
	{
		get { return Roaming & Foraging; }
		set
		{
			if (!value)
			{
				try
				{
					Roaming = Foraging = Eating = value;
					rigidbody.angularVelocity = rigidbody.velocity = Vector3.zero;
					transform.rotation = Quaternion.identity;
					StopAllCoroutines();
				}
				catch
				{
					Debug.LogError ("Could not set Active to false!");
				}
			}
		}
	}
	
	public bool ReachedTarget
	{
		get
		{
			if (Target && Vector3.Distance(transform.position, Target.transform.position) 
			<= Mathf.Max(new float[2] { Target.lossyScale.x, Target.lossyScale.y }))
				return true;
			return false;
		}
	}
	
	Transform Target
	{
		get
		{
			print (name +": "+ ooiTarget +", "+ roamTarget);
			if (ooiTarget != null)
				return ooiTarget;
			if (roamTarget != null)
				return roamTarget;
			return null;
		}
	}
	
	bool Hearing
	{
		get { return senses[0]; }
	}
	#endregion
}