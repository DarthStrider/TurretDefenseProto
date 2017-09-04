using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

	[SerializeField] private float _speed;
	private float _damage;
	public float Damage
	{
		get { return _damage; }
	}

	private GameObject _target;

	public void SetData(float damage, GameObject target)
	{
		_damage = damage;
		_target = target;
	}
	
	// Update is called once per frame
	void Update () {
		if (_target != null)
		{
			Vector3 heading = _target.transform.position - transform.position;
			float distance = heading.magnitude;
			Vector3 direction = heading / distance;
			direction = direction.normalized;
			transform.position = transform.position + (direction * _speed * Time.deltaTime);
		}
	}
}
