#define USE_OLD_CODE

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public delegate void OnAngerEvent(float anger);
public delegate void OnHungerEvent(float hunger);
public delegate void OnDangerEvent(float danger);

[RequireComponent(typeof(DamageIndicator), typeof(Stats))]
[RequireComponent(typeof(Rigidbody), typeof(Locomotor), typeof(Stats))]

public class Essence: MonoBehaviour
{
	public event OnAngerEvent HandleOnAnger;
	public event OnHungerEvent HandleOnHunger;
	public event OnDangerEvent HandleOnDanger;
	
#if USE_OLD_CODE
	public float quickness = 5;
	private float gridLength = 30;
	private float gridHeight = 30;
	
	List<ObjectOfInterest> objectsOfInterest = new List<ObjectOfInterest>();
	//bool[] states = new bool[5];
	bool[] flags = new bool[6];
	float hungerLevel = 50;			// refactor to store in stats
	float healthLevel = 50;			// refactor to store in stats
	Transform ooiTarget, roamTarget = null;	// replace with Acquirer
#endif
	
	public float intellgence = 1;
	public int memoryLength = 64;	//size of array, must be an int.
	
	private Animation animator;
	public AnimationClip animationClip;
	private AudioSource audioSource;
	private Locomotor locomotor;
	private Acquirer acquirer;
	private Stats stats;
	
	#region Overhead
	void Awake()
	{
		// Setup Dependancies
		animator = GetComponent<Animation>();
		audioSource = GetComponent<AudioSource>();
		locomotor = GetComponent<Locomotor>();
		acquirer = GetComponent<Acquirer>();
		stats = GetComponent<Stats>();
		
		// Setup Components
		animationClip.wrapMode = WrapMode.Loop;
		animator.clip = animationClip;
		animator.Play();
		rigidbody.freezeRotation = true;
		//rigidbody.useGravity = false;
		Hungry = true;
		
	}
	
	void Start()
	{
		//transform.parent = GameObject.Find("Generated Enemies").transform;
		
		// Generate a list of all Objects of Interest with which to attract Badger to.
		//  -- remove after successful integration of Acquirer.
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
	
	void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.tag == "Rock" && collision.rigidbody.velocity.magnitude > 0)
			StartCoroutine(DoDamage());
	}
	
	public void MakeAlert(bool danger)
	{
		if (danger) 
			Hungry = false;
		Active = false;
		MakeDecision();
	}
	#endregion
	#region Behaviour
	IEnumerator DoDamage()
	{
		healthLevel -= 10;
		//Debug.Log ("Doing damage to Badger.  Health now at " +healthLevel+ ".");
		yield return StartCoroutine(GetComponent<DamageIndicator>().FlashDamage());
		GetComponent<DamageIndicator>().ResetMaterial();
		if(healthLevel <= 0)
			Destroy(gameObject);
		
	}
	
	IEnumerator Forage()
	{
		//Debug.Log (name +": foraging");
		yield return StartCoroutine(MoveTowardTarget(false));
		StartCoroutine(Eat());
	}
	
	IEnumerator Roam(bool foraging)
	{
		//Debug.Log (name + (foraging?": looking for food":" looking around"));
		roamTarget = GetWaypoint();
		yield return StartCoroutine(MoveTowardTarget(foraging));
		if (ReachedTarget && foraging)
			StartCoroutine(Eat());
		else
			MakeDecision();
	}
	
	void Sleep()
	{
		//Debug.Log (name +": sleeping");
		Invoke("MakeDecision", (quickness*0.75f) * 10);
	}
	
	IEnumerator MoveTowardTarget(bool foraging)
	{
		yield return StartCoroutine(locomotor.TurnToFace(Target.position, 5)); // move turn speed to pub var deg/sec
		while (Target && !ReachedTarget && hungerLevel < 150)
		{
			hungerLevel += 0.1f;
			rigidbody.velocity = rigidbody.angularVelocity = Vector3.zero;
			if (foraging)
			{
				if(FindClosestFood())
				{
					StopCoroutine("MoveTowardTarget");
					Forage();
				}
			}
			if (hungerLevel < 100)
				locomotor.GoForward(0.1f);
			else
				locomotor.GoForward(0.05f);
			yield return new WaitForSeconds(Time.deltaTime);
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
			//Debug.Log (name +": nomnoms -- hunger level = "+ hungerLevel);
			yield return new WaitForSeconds(quickness/5);
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
		if (audioSource) audioSource.Play();
		Active = false;
		if (Hungry && FindClosestFood())
			StartCoroutine(Forage());
		else if (!Tired)
			StartCoroutine(Roam(true));
		else
		{
			if (Random.value > 0.75)
			{
				Debug.Log (name +": thinking for "+ (quickness*0.75f) +"seconds");
				Invoke("MakeDecision", (quickness*0.75f));
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
	
	public bool CanSee(Transform transformOfInterest)
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
		if (roamTarget)
			Destroy(roamTarget.gameObject);
		Transform t = GetRandomGameObjectOnGrid("target", "World", true).transform;
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
		get { return stats.memory.CurrentHunger > 25; } // 25 = magic hunger threshold value
		set { stats.memory.CurrentHunger = value ? 50 : 0; }
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
		get { return Roaming ^ Foraging ^ Eating; }
		set
		{
			if (!value)
			{
				try
				{
					Roaming = Foraging = Eating = value;
					rigidbody.angularVelocity = rigidbody.velocity = Vector3.zero;
					//transform.rotation = Quaternion.identity; -- what was this here for?
					StopAllCoroutines();
				}
				catch
				{
					Debug.LogError ("Could not set Active flags (for " +name+ ") to false!");
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
	
	public Transform Target
	{
		get
		{
			if (ooiTarget != null)
				return ooiTarget;
			if (roamTarget != null)
				return roamTarget;
			return null;
		}
	}
	
	public GameObject GetRandomGameObjectOnGrid(string name, string parentGameObjectName, bool hideInHierarchy)
	{
		GameObject go = new GameObject();
		go.name = (name);
		go.transform.position = GetRandomVectorOnGrid();
		go.transform.parent = GameObject.Find(parentGameObjectName).transform;
		if (hideInHierarchy)
    		go.hideFlags = HideFlags.HideInHierarchy;
		return go;
	}
	
	Vector3 GetRandomVectorOnGrid()
	{
		return new Vector3(Random.Range(-gridHeight/2, gridHeight/2), 0, Random.Range(-gridLength/2, gridLength/2));// + transform.position;
	}
	#endregion
}
