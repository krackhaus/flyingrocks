using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Stats : MonoBehaviour
{
	private float attitude;
	private float damage;
	private float hunger;
	
	class Memory
	{
		
		private List<float> hunger;// = new List<float>(memoryLength);
		private List<float> anger;// = new List<float>(memoryLength);
		private int memoryLength;
		
		#region Getters and Setters
		public int MemoryLength
		{
			get { return memoryLength; }
		}
		
		// HUNGER ------------
		public float CurrentHunger
		{
			get { return hunger[0]; }
		}
		
		public float AverageHunger
		{
			get
			{
				float average = 0.0f;
				int count = 0;
				
				foreach (float f in hunger)
				{
					count++;
					average += f;
				}
				
				average /= count;
				return count;
			}
		}
		
		// ANGER ------------
		public float CurrentAnger
		{
			get { return anger[0]; }
		}
		
		public float AverageAnger
		{
			get
			{
				float average = 0.0f;
				int count = 0;
				
				foreach (float f in anger)
				{
					count++;
					average += f;
				}
				
				average /= count;
				return count;
			}
		}
		#endregion
	}
	private Memory memory = new Memory();
	
	
	
	
}