using UnityEngine;
using System.Collections;

public class ObjectOfInterest : MonoBehaviour
{
	float life = 2;
	
	public void Eat()
	{
		life--;
		if (life == 0)
			Destroy(gameObject);
	}
	
	bool IsAlive
	{
		get
		{
			if (life > 0)
				return true;
			else
				return false;
		}
	}
}
