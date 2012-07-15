using UnityEngine;
using System.Collections;

public class Flock : MonoBehaviour {
	
	GameObject goalPosition;
	
	public float rotationSpeed = 5;
	public float neighborDistance = 2;
	
	float speed = .001f;
	Vector3 averageHeading, averagePosition;
	
	void Start()
	{
		goalPosition = GameObject.Find("GoalPosition");
		speed = Random.Range(.1f,1);
	}
	
	void Update()
	{
		if (Random.Range(0,5) < 1)
			ApplyRules();
		transform.Translate(0,0,Time.deltaTime * speed);
	}
	
	void ApplyRules()
	{
		GameObject[] gos = GameObject.FindGameObjectsWithTag("Crow");
		Vector3 goalPos = goalPosition.transform.position;
		Vector3 vcenter = new Vector3();
		Vector3 vavoid = new Vector3();
		float gSpeed = 0;
		Vector3 wind = new Vector3(1,0,1);
		
		float dist;
		int groupSize = 0;
		
		foreach (GameObject go in gos)
		{
			if (go != this.gameObject)
			{
				dist = Vector3.Distance(go.transform.position, this.transform.position);
				
				if (dist <= neighborDistance)
				{
					vcenter += go.transform.position;
					groupSize++;
					if (dist < .5f)
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
