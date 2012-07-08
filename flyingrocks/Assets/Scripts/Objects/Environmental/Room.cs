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
	public static GameObject Initialize(Transform parent, Object prefab, ref Vector2 position, int idx, bool advanceRow)
	{
		Object temp = Object.Instantiate(prefab);
		Debug.Log (temp.name);
		GameObject roomInstance = GameObject.Find(prefab.name + "(Clone)");
		PlaceRoomTransform(parent, ref roomInstance, position);
		position.x += roomInstance.collider.bounds.extents.x * 2;
		if (advanceRow)
		{
			position.x = 0;
			position.y -= roomInstance.collider.bounds.extents.z * 2;
		}
		roomInstance.name = string.Format("Room{0}", idx);
		roomInstance.GetComponent<RoomFollowCamera>().enabled = false;
		PopulateRoom();
		
		return roomInstance;
	}
	
	/// <summary>
	/// Places the room transform.
	/// </summary>
	static void PlaceRoomTransform(Transform parent, ref GameObject instance, Vector2 position)
	{
		RaycastHit hit;
		float heightOffset = 10;
		Vector3 castPos = parent.position + new Vector3(0, heightOffset, 0);
		
		if (Physics.Raycast(castPos, Vector3.down, out hit))
		{
			instance.transform.position = new Vector3(position.x, hit.point.y - 15, position.y);
			instance.transform.parent = parent;
		}
		else
			Object.Destroy(instance);
	}
	
	/// <summary>
	/// Generates Obstacles, Enemies, and Acquirables in the room.
	/// </summary>
	static void PopulateRoom()
	{
		//todo--create and call individual methods for generating stuff.
	}
}