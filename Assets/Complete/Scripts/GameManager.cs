using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace CompleteProject
{
	public class GameManager : MonoBehaviour
	{

		PlayerHealth playerHealth;
		public Slider healthSlider;
		public Text scoreLabel;
		public int zombiesKilled;

		void Awake ()
		{

			var player = GameObject.FindGameObjectWithTag ("Player");
			playerHealth = player.GetComponent <PlayerHealth> ();

		}

		void Update ()
		{
			healthSlider.value = playerHealth.Health;
			scoreLabel.text = zombiesKilled.ToString (); 
		}

		public void OnZombieKilled ()
		{
			zombiesKilled ++;
		}
	}
}
