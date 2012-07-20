using UnityEngine;
using System.Collections;

public class RoomGenerator : MonoBehaviour
{
	public Object[] prefabs;
	public Vector2 sizeOfLabyrinth;
	public bool generateOnAwake = true;
	public bool randomizeLightColor;
	public bool useRoomCameras = true;
	
	private GameObject[] labyrinthRooms;
	
	void Awake()
	{
		if (generateOnAwake)
		{
			if (useRoomCameras)
				Camera.mainCamera.GetComponent<FollowCamera>().enabled = false;
			
			GenerateLabrynth();
		}
	}
	
	/// <summary>
	/// Generates the Labrynth according to parameters.
	/// </summary>
	void GenerateLabrynth()
	{
		int prefabToUse = 0; //todo--choose which prefab of those available to use
		
		int roomIndex = 0;
		bool newRow = false;
		int sizeY = (int)sizeOfLabyrinth.y;
		int sizeX = (int)sizeOfLabyrinth.x;
		float startPostionX = transform.position.x;
		Vector2 position = new Vector2(0,0);
		labyrinthRooms = new GameObject[sizeX * sizeY];
		
		print (sizeX);
		
		for (int y = 0; y < sizeY; y++)
		{
			for (int x = 0; x < sizeX; x++)
			{
				float oldY = position.y;
				labyrinthRooms[roomIndex] = Room.Initialize(this.transform, prefabs[prefabToUse], ref position, roomIndex, newRow);
				if (randomizeLightColor)
					labyrinthRooms[roomIndex].GetComponentInChildren<Light>().color = Room.GetRandomColor();
				if (x == sizeX-2)
					newRow = true;
				roomIndex++;
			}
			position.x = startPostionX;
			newRow = false;
		}
		
		BrainDump();
	}
	
	/// <summary>
	/// Releases refrences for garbage collection.
	/// </summary>
	void BrainDump()
	{
		prefabs = null;
	}
}