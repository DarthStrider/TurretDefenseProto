using System;
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
	

	// Use this for initialization
	public virtual void Start () {
		_economyManager = EconomyManager.instance;
		_isButtonOn = false;
		_currentLevel = 0;
		_level = _levels[_currentLevel];
		_level.Gun.SetActive(true);
		_turretLevelCost.text = _levels[_currentLevel + 1].LevelCost.ToString();
		_maxTurretLevel = _levels.Count - 1;
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
		if (_economyManager.Gold >= _levels[_currentLevel + 1].LevelCost && _currentLevel + 1 < _maxTurretLevel)
		{
			_level.Gun.SetActive(false);
			++_currentLevel;
			_level = _levels[_currentLevel];
			_level.Gun.SetActive(true);
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
