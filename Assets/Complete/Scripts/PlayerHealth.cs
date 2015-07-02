using UnityEngine;
using System.Collections;

namespace CompleteProject
{
	public class PlayerHealth : MonoBehaviour
	{

		public float Health = 100f;

		public void RecieveDamage (float damage)
		{
			Health -= damage;
		}
	
	}
}