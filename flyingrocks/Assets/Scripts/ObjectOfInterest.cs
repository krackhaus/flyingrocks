using UnityEngine;
using System.Collections;

public class ObjectOfInterest : MonoBehaviour
{
	float life = 100;
	
	public void Eat()
	{
		life--;
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
