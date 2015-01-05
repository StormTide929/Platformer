using UnityEngine;
using System.Collections;

public class Entity : MonoBehaviour {
	
	public float health;
	public GameObject ragdoll;
	public GameObject bloodSplash;
	
	public void TakeDamage(float dmg) {
		health -= dmg;
		//Ragdoll r = (Instantiate(ragdoll,transform.position,transform.rotation) as GameObject).GetComponent<Ragdoll>();
		Vector3 particleOffset = transform.position;
		particleOffset.y += 2;
		ParticleSystem e = (Instantiate(bloodSplash, particleOffset, Quaternion.Euler(270, 0, 0)) as GameObject).GetComponent<ParticleSystem>();
		if (health <= 0) {
			Die();	
		}
	}
	
	public void Die() {
		Ragdoll r = (Instantiate(ragdoll,transform.position,transform.rotation) as GameObject).GetComponent<Ragdoll>();
		r.CopyPose(transform);
		Destroy(this.gameObject);
	}
}
