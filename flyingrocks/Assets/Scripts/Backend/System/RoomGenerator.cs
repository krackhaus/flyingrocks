using UnityEngine;
using System.Collections;

public class RoomGenerator : MonoBehaviour
{
	public Object[] prefabs;
	public Vector2 sizeOfLabyrinth;
	public bool generateOnAwake = true;
	
	private GameObject[] labyrinthRooms;
	
	void Awake()
	{
		if (generateOnAwake)
			GenerateLabrynth();
	}
	
	/// <summary>
	/// Generates the Labrynth according to parameters.
	/// </summary>
	void GenerateLabrynth()
	{
		int prefabToUse = 0; //todo--choose which prefab of those available to use
		
		int roomIndex = 0;
		int sizeY = (int)sizeOfLabyrinth.y;
		int sizeX = (int)sizeOfLabyrinth.x;
		labyrinthRooms = new GameObject[sizeX * sizeY];
		
		for (int y = 0; y < sizeY; y++)
			for (int x = 0; x < sizeX; x++)
				labyrinthRooms[roomIndex] = Room.Initialize(this.transform, prefabs[prefabToUse], roomIndex++);
		
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