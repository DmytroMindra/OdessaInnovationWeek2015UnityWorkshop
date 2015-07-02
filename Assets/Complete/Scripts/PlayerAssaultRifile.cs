using UnityEngine;
using System.Collections;

namespace CompleteProject
{
public class PlayerAssaultRifile : MonoBehaviour {
	
	public  float shotsPerSecond = 1;

	private ParticleSystem gunParticles;
	private AudioSource gunAudio;
	private Light gunLight;
	private LineRenderer gunLine;
	private int shootableMask;

	private float timer;
	private float timeBetweenBullets;



	public int damagePerShot = 20;                  // The damage inflicted by each bullet.
	public float range = 100f;                      // The distance the gun can fire.
	Ray shootRay;                                   // A ray from the gun end forwards.
	RaycastHit shootHit;                            // A raycast hit to get information about what was hit.
	float effectsDisplayTime = 0.2f;                // The proportion of the timeBetweenBullets that the effects will display for.


	void Awake ()
	{
		shootableMask = LayerMask.GetMask ("Shootable");
		gunParticles = GetComponent<ParticleSystem> ();
		gunLine = GetComponent <LineRenderer> ();
		gunAudio = GetComponent<AudioSource> ();
		gunLight = GetComponent<Light> ();
		timeBetweenBullets = 1f / shotsPerSecond;
	}

	void Update ()
	{
		// Add the time since Update was last called to the timer.
		timer += Time.deltaTime;
		
		// If the Fire1 button is being press and it's time to fire...
		if(Input.GetButton ("Fire1") && timer >= timeBetweenBullets)
		{
			// ... shoot the gun.
			Shoot ();
		}
		
		// If the timer has exceeded the proportion of timeBetweenBullets that the effects should be displayed for...
		if(timer >= timeBetweenBullets * effectsDisplayTime)
		{
			// ... disable the effects.
			DisableEffects ();
		}
	}
	
	
	public void DisableEffects ()
	{
		// Disable the line renderer and the light.
		gunLine.enabled = false;
		gunLight.enabled = false;
	}
	
	
	void Shoot ()
	{
		// Reset the timer.
		timer = 0f;
		
		// Play the gun shot audioclip.
		gunAudio.Play ();
		
		// Enable the light.
		gunLight.enabled = true;
		
		// Stop the particles from playing if they were, then start the particles.
		gunParticles.Stop ();
		gunParticles.Play ();
		
		// Enable the line renderer and set it's first position to be the end of the gun.
		gunLine.enabled = true;
		gunLine.SetPosition (0, transform.position);
		
		// Set the shootRay so that it starts at the end of the gun and points forward from the barrel.
		shootRay.origin = transform.position;
		shootRay.direction = transform.forward;
		
		// Perform the raycast against gameobjects on the shootable layer and if it hits something...
		if(Physics.Raycast (shootRay, out shootHit, range, shootableMask))
		{
			// Try and find an EnemyHealth script on the gameobject hit.
			var enemyAI = shootHit.collider.GetComponent <EnemyNavMeshAI> ();
			
			// If the EnemyHealth component exist...
			if(enemyAI != null)
			{
				// ... the enemy should take damage.
				enemyAI.TakeDamage (damagePerShot, shootHit.point, shootRay);
			}
			
			// Set the second position of the line renderer to the point the raycast hit.
			gunLine.SetPosition (1, shootHit.point);
		}
		// If the raycast didn't hit anything on the shootable layer...
		else		{
			// ... set the second position of the line renderer to the fullest extent of the gun's range.
			gunLine.SetPosition (1, shootRay.origin + shootRay.direction * range);
		}
	}

}
}