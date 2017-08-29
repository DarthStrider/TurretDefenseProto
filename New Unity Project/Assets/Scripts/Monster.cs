using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Monster : MonoBehaviour {
	private Animator _animator;
    private NavMeshAgent _agent;
    private Transform _goal;
	private float _totalHealth;

    [SerializeField] private float _health;
    [SerializeField] private float _movementSpeed;
	[SerializeField] private Image _healthBar;

	// Use this for initialization
    public virtual void Start () {
        _agent = GetComponent<NavMeshAgent> ();
        _goal = GameObject.FindGameObjectWithTag ("Finish").transform;
        _agent.speed = _movementSpeed;
        _agent.destination = _goal.position;
	    _animator = gameObject.GetComponent<Animator>();
	    _animator.SetBool ("move", true);
	    SetHealthBar();
	}

	
	// Update is called once per frame
	public virtual void Update () {
			if(Vector3.Distance(transform.position,_goal.position) <=4)
			{
				_animator.SetBool ("dead", true);
				Destroy (this.gameObject);
				SpawnManager.instance.MonsterDied();
			}
	}
	
	private void SetHealthBar()
	{
		float percent = _health / _totalHealth;
		_healthBar.transform.localScale = new Vector3(percent,1,1);
	}
	private void TookDamage(float amount)
	{
		if (_health - amount <= 0)
		{
			_health = 0;
			_animator.SetBool ("dead", true);
			StartCoroutine(WaitToDestroy());
		}
		else
		{
			_health -= amount;
			SetHealthBar();
		}
	}

	private IEnumerator WaitToDestroy()
	{
		yield return new WaitForSeconds(2);
		Destroy(this);
		SpawnManager.instance.MonsterDied();
	}
}
