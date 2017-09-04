using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Monster : MonoBehaviour {
	protected Animator _animator;
    protected NavMeshAgent _agent;
    private Transform _goal;
	private float _totalHealth;

    [SerializeField] protected float _health;
    [SerializeField] private float _movementSpeed;
	[SerializeField] private Image _healthBar;
	[SerializeField] protected AudioSource _audioSource;
	

	// Use this for initialization
    public virtual void Start () {
        _agent = GetComponent<NavMeshAgent> ();
        _goal = GameObject.FindGameObjectWithTag ("Finish").transform;
        _agent.speed = _movementSpeed;
        _agent.destination = _goal.position;
	    _animator = gameObject.GetComponent<Animator>();
	    _totalHealth = _health;
	    _animator.SetBool ("move", true);
	    SetHealthBar();
	}


	protected void SetHealthBar()
	{
		float percent = _health / _totalHealth;
		_healthBar.transform.localScale = new Vector3(percent,1,1);
	}


	protected IEnumerator WaitToDestroy()
	{
		yield return new WaitForSeconds(2);
		Destroy(this.gameObject);
		SpawnManager.instance.MonsterDied();
	}
}
