using UnityEngine;
using System.Collections;

public class Room
{
	/// <summary>
	/// Instantiates the Room and returns the created GameObject.
	/// </summary>
	/// <param name='parent'>
	/// Which Transform to use as parent.
	/// </param>
	/// <param name='prefab'>
	/// The prefab to instantiate.
	/// </param>
	/// <param name='idx'>
	/// The room index number as assigned by the Generator.
	/// </param>
	public static GameObject Initialize(Transform parent, Object prefab, int idx)
	{
		Object temp = Object.Instantiate(prefab);
		GameObject roomInstance = GameObject.Find(prefab.name);
		PlaceRoomTransform(parent, ref roomInstance);
		roomInstance.transform.parent = parent;
		roomInstance.name = string.Format("Room{0}", idx);
		PopulateRoom();
		Object.Destroy(temp);
		return roomInstance;
	}
	
	/// <summary>
	/// Places the room transform.
	/// </summary>
	static void PlaceRoomTransform(Transform parent, ref GameObject instance)
	{
		RaycastHit hit;
		float heightOffset = 10;
		float preferredHeight = instance.transform.position.y;
		Vector3 mePos = parent.position;
		Vector3 castPos = mePos + new Vector3(0, heightOffset, 0);
		
		if (Physics.Raycast(castPos, Vector3.down, out hit))
			instance.transform.position = new Vector3(mePos.x, hit.point.y + preferredHeight, mePos.z);
	}
	
	/// <summary>
	/// Generates Obstacles, Enemies, and Acquirables in the room.
	/// </summary>
	static void PopulateRoom()
	{
		//todo--create and call individual methods for generating stuff.
	}
}