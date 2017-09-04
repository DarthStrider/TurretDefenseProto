using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ogre : Monster {

	[SerializeField] private int _goldGiven;

	// Use this for initialization
    public override void Start () {
        base.Start(); 
    }

	
	private void TookDamage(float amount)
	{
		if (_health - amount <= 0)
		{
			_health = 0;
			_animator.SetBool ("move", false);
			_animator.SetBool ("dead", true);
			_agent.speed = 0;
			StartCoroutine(WaitToDestroy());
			EconomyManager.instance.Gold += _goldGiven;

		}
		else
		{
			_health -= amount;
		}
		SetHealthBar();
	}
	private void OnCollisionEnter(Collision other)
	{
		if (other.collider.tag == "Projectile")
		{
			var projectile = other.gameObject;
			var pScript = projectile.GetComponent<Projectile>();
			float damage = pScript.Damage;
			_audioSource.Play();
			TookDamage(damage);
			Destroy(projectile);
		}
		if (other.collider.tag == "Finish")
		{
			_animator.SetBool ("dead", true);
			Destroy (this.gameObject);
			SpawnManager.instance.MonsterReachedGoal();
			SpawnManager.instance.MonsterDied();
		}
	}
	
}
