using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Stats : MonoBehaviour
{
	public float damage, health, hunger, attitude, anger, intelligence, perception;
	
	public Memory memory = new Memory();
	
	public class Memory
	{
		private List<float> hunger;
		private List<float> anger;
		private int memoryLength = 64;
		
		public Memory()
		{
			hunger = new List<float>(memoryLength);	
			anger = new List<float>(memoryLength);
		}
		
		
		#region Getters and Setters
		public int MemoryLength
		{
			get { return memoryLength; }
		}
		
		// HUNGER ------------
		public float CurrentHunger
		{
			get { return hunger.IndexOf(0); }
			set
			{
				hunger.Insert(0, value);
			}
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
}