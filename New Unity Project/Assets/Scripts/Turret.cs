using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using TMPro;
using UnityEngine;

public class Turret : MonoBehaviour
{

	[SerializeField] private GameObject _buttons;
	[SerializeField] private TextMeshProUGUI _turretLevelCost;
	[SerializeField] protected List<TurretLevel> _levels;
	[SerializeField] private GameObject _upgradeTurret;
	[SerializeField] protected GameObject _projectile;
	[SerializeField] private float _firingDistance;

	private TurretSpawning _spawn;
	public TurretSpawning Spawn
	{
		get { return _spawn;}
		set { _spawn = value; }
	}

	protected EconomyManager _economyManager;
	protected int _currentLevel;
	private bool _isButtonOn;
	private int _maxTurretLevel;
	protected TurretLevel _level;
	protected float _fireRate;
	protected float _damage;
	protected float _splashDamage;
	

	// Use this for initialization
	public virtual void Start () {
		_economyManager = EconomyManager.instance;
		_isButtonOn = false;
		_currentLevel = 0;
		_level = _levels[_currentLevel];
		_level.Gun.SetActive(true);
		_fireRate = _level.FireRate;
		_damage = _level.Damage;
		_splashDamage = _level.SplashDamage;
		_turretLevelCost.text = _levels[_currentLevel + 1].LevelCost.ToString();
		_maxTurretLevel = _levels.Count - 1;
		StartCoroutine (WaitToShoot());
	}
	
	// Update is called once per frame
	public virtual void Update () {
		
	}

	public void ButtonSwitch()
	{
		_isButtonOn = !_isButtonOn;
		_buttons.SetActive(_isButtonOn);
	}
	
	public virtual void DestroyTurret()
	{
		_spawn.IsTurretBuilt = false;
		Destroy(this.gameObject);
	}

	public void LevelTurret()
	{
		if (_economyManager.Gold >= _levels[_currentLevel + 1].LevelCost && _currentLevel + 1 <= _maxTurretLevel)
		{
			_level.Gun.SetActive(false);
			++_currentLevel;
			_level = _levels[_currentLevel];
			_level.Gun.SetActive(true);
			_fireRate = _level.FireRate;
			_damage = _level.Damage;
			_splashDamage = _level.SplashDamage;
			_economyManager.Gold -= _level.LevelCost;
			if (_currentLevel + 1 != _levels.Count)
			{
				_turretLevelCost.text = _levels[_currentLevel + 1].LevelCost.ToString();
			}
			else
			{
				_upgradeTurret.SetActive(false);
			}
			ButtonSwitch();
		}
		else
		{
			ButtonSwitch();
		}
	}

	private IEnumerator WaitToShoot()
	{
		yield return new WaitForSecondsRealtime(_fireRate);
		FireGun ();
	}

	private void FireGun()
	{
		var enemies = GameObject.FindGameObjectsWithTag("Hitbox");
		float minDistance = _firingDistance;
		GameObject target = null;
		for (int i = 0; i < enemies.Length; i++) {
			float distance = Vector3.Distance (transform.position, enemies[i].transform.position);
			if (distance < _firingDistance) {
				if (distance <= minDistance) {
					target = enemies [i];
					minDistance = distance;
				}
			}

		}
		if (target != null)
		{
			Vector3 heading = target.transform.position - transform.position;
			float distance = heading.magnitude;
			Vector3 direction = heading / distance;
			_level.Gun.transform.forward = direction;
			var projectile = Instantiate(_projectile, _level.Gun.transform.position, Quaternion.identity);
			projectile.transform.forward = _level.Gun.transform.forward;
			projectile.GetComponent<Projectile>().SetData(_level.Damage,target);
		}
		StartCoroutine (WaitToShoot ());
	}
	
}
	
[Serializable]
public class TurretLevel
{
	public GameObject Gun;
	public int LevelCost;
	public float FireRate;
	public float Damage ;
	public float SplashDamage;
}
