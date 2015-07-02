using UnityEngine;
using System.Collections;

namespace CompleteProject
{
	public class ZombieSpawner : MonoBehaviour
	{

		public GameManager gameManager;
		public GameObject zombiePrefab;
		public GameObject player;
		public float zombieCooldown = 4;
		private float nextSpawnTime;


		// Update is called once per frame
		void Update ()
		{
	
			if (Time.time > nextSpawnTime) {
				GameObject zombie = Instantiate (zombiePrefab, this.transform.position, Quaternion.identity)as GameObject;
				var ai = zombie.GetComponent<EnemyNavMeshAI> ();
				ai.target = player.transform;
				ai.OnDeath += () => gameManager.OnZombieKilled ();
				nextSpawnTime = Time.time + zombieCooldown;
			}

		}
	}
}