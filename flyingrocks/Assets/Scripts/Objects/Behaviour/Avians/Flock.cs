using UnityEngine;
using System.Collections;

public class Flock : MonoBehaviour {
	
	GameObject goalPosition;
	GameObject[] flock;
	
	public float birdSpeed = 2;
	public float rotationSpeed = 5;
	public float flockInclusionDistance = 5;
	public float collisionAvoidanceDistance = 1;
	
	float speed = .001f;
	Vector3 averageHeading, averagePosition;
	Vector3 wind = new Vector3(1, 0, 1);
	
	void Start()
	{
		flock = GameObject.FindGameObjectsWithTag("Crow");
		goalPosition = GameObject.Find("GoalPosition");
		speed = Random.Range(.1f,1);
	}
	
	void Update()
	{
		if (Random.Range(0, 5) < 1)
			ApplyRules();
		transform.Translate(0, 0, Time.deltaTime * speed * birdSpeed);
	}
	
	void ApplyRules()
	{
		Vector3 goalPos = goalPosition.transform.position;
		Vector3 vcenter = Vector3.zero;
		Vector3 vavoid = Vector3.zero;
		float gSpeed = 0;
		
		float dist;
		int groupSize = 0;
		
		foreach (GameObject go in flock)
		{
			if (go != this.gameObject)
			{
				dist = Vector3.Distance(go.transform.position, this.transform.position);
				
				if (dist <= flockInclusionDistance)
				{
					vcenter += go.transform.position;
					groupSize++;
					if (dist < collisionAvoidanceDistance)
						vavoid = vavoid + (this.transform.position - go.transform.position);
					gSpeed = gSpeed + go.GetComponent<Flock>().speed;
				}
			}
		}
		
		if (groupSize != 0)
		{
			vcenter = vcenter/groupSize + wind + (goalPos - this.transform.position);
			speed = gSpeed/groupSize;
			Vector3 direction = (vcenter + vavoid) - transform.position;
			
			if (direction != Vector3.zero)
				transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), rotationSpeed * Time.deltaTime);
		}
	}
}
