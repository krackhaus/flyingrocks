using UnityEngine;

[RequireComponent(typeof(Acquirer))]

public class RockGrabber : MonoBehaviour
{

	private Acquirable rock;

	private void OnAcquisitionOf(Acquirable acquirable)
	{
		rock = acquirable;
	}

	private void Update()
	{
		if (rock != null)
			rock.transform.parent.position = transform.position;
	}

}
