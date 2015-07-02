using UnityEngine;
using System.Collections;

namespace CompleteProject
{
	public class EnemyAttack : MonoBehaviour
	{

		public float timeBetweenAttacks = 0.5f;     // The time in seconds between each attack.
		public int attackDamage = 10;               // The amount of health taken away per attack.

		bool playerInRange;
		GameObject player;
		PlayerHealth playerHealth;
		float nextAttackTime;

		void Awake ()
		{
			player = GameObject.FindGameObjectWithTag ("Player");
			playerHealth = player.GetComponent <PlayerHealth> ();
		}
	
		// Update is called once per frame
		void Update ()
		{
	
			if (playerInRange && Time.time > nextAttackTime) {
				nextAttackTime = Time.time + timeBetweenAttacks;
				playerHealth.RecieveDamage (attackDamage);
			}

		}

		void OnTriggerEnter (Collider other)
		{
			// If the entering collider is the player...
			if (other.gameObject == player) {
				// ... the player is in range.
				playerInRange = true;
			}
		}
	
		void OnTriggerExit (Collider other)
		{
			// If the exiting collider is the player...
			if (other.gameObject == player) {
				// ... the player is no longer in range.
				playerInRange = false;
			}
		}
	
	
	}
}