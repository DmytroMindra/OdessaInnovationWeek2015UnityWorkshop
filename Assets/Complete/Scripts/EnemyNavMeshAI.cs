using UnityEngine;
using System.Collections;
using System;

// NavMesh tutorial: https://unity3d.com/learn/tutorials/modules/beginner/live-training-archive/navmeshes

namespace CompleteProject
{
	public class EnemyNavMeshAI : MonoBehaviour
	{
	
		public GameObject deadPrefab;
		public float damagePerSecond;

		public event Action OnDeath;

		NavMeshAgent navMeshAgent;
		public Transform target;

		public void setTarget (Transform newTarget)
		{
			target = newTarget;
		}

		void Start ()
		{
			navMeshAgent = GetComponent<NavMeshAgent> ();		
		}
	
		// Update is called once per frame
		void Update ()
		{
			if (target)
				navMeshAgent.SetDestination (target.position);
		}

		public void TakeDamage (int damagePerShot, Vector3 point, Ray shootRay)
		{
			var direction = point - transform.position;
			direction = shootRay.direction.normalized;
			this.gameObject.SetActive (false);
			var gameObj = GameObject.Instantiate (deadPrefab, this.transform.position, this.transform.rotation) as GameObject;
			gameObj.GetComponentsInChildren<Rigidbody> () [0].AddForce (direction * 100, ForceMode.Impulse);
		
			if (OnDeath != null)
				OnDeath ();
		
			Destroy (this.gameObject, 0.5f);
		}

	}
}
