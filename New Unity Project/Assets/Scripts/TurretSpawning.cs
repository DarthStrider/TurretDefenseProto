using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TurretSpawning : MonoBehaviour
{
	[SerializeField] private GameObject _turrentOptions;
	[SerializeField] private GameObject _crossbowTurret;
	[SerializeField] private GameObject _acidTurret;
	[SerializeField] private GameObject _cannonTurret;
	[SerializeField] private TextMeshProUGUI _crossbowCost;
	[SerializeField] private TextMeshProUGUI _cannonCost;
	[SerializeField] private TextMeshProUGUI _acidCost;

	private EconomyManager _economyManager;

	public enum TurretType
	{
		Cannon,
		Acid,
		Crossbow
	}
	
	
	private bool _turretEnabled;
	private bool _isTurretBuilt;
	public bool IsTurretBuilt
	{
		set { _isTurretBuilt = value; }
		
	}

	// Use this for initialization
	void Start ()
	{
		_turretEnabled = false;
		_isTurretBuilt = false;
		_economyManager = EconomyManager.instance;
		_crossbowCost.text = _economyManager.CrossBowTurretCost.ToString();
		_cannonCost.text = _economyManager.CannonTurretCost.ToString();
		_acidCost.text = _economyManager.AcidTurretCost.ToString();

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void TurretOptionsSwitch()
	{
		if (!_isTurretBuilt)
		{
			_turretEnabled = !_turretEnabled;
			_turrentOptions.SetActive(_turretEnabled);
		}
	}

	public void BuildCrossbowTurret()
	{
		if (_economyManager.Gold >= _economyManager.CrossBowTurretCost)
		{
			BuildTurret(_crossbowTurret, TurretType.Crossbow);
			_economyManager.Gold -=  _economyManager.CrossBowTurretCost;
		}
		else
		{
			TurretOptionsSwitch();
		}
	}
	
	public void BuildAcidTurrent()
	{
		if (_economyManager.Gold >= _economyManager.AcidTurretCost)
		{
			BuildTurret(_acidTurret, TurretType.Acid);
			_economyManager.Gold -=  _economyManager.AcidTurretCost;
		}
		else
		{
			TurretOptionsSwitch();
		}	
	}
	public void BuildCannonTurret()
	{
		if (_economyManager.Gold >= _economyManager.CannonTurretCost)
		{
			BuildTurret(_cannonTurret, TurretType.Cannon);
			_economyManager.Gold -= _economyManager.CannonTurretCost;
		}
		else
		{
			TurretOptionsSwitch();
		}	
	}
	

	private void BuildTurret(GameObject turret, TurretType type)
	{
		var gun = Instantiate(turret, transform.position, Quaternion.identity);
		gun.transform.forward = transform.forward;
		if (type == TurretType.Acid)
		{
			gun.GetComponent<AcidTurret>().Spawn = this;
		}
		else if (type == TurretType.Cannon)
		{
			gun.GetComponent<Cannon>().Spawn = this;
		}
		else
		{
			gun.GetComponent<Crossbow>().Spawn = this;
		}
		TurretOptionsSwitch();
		_isTurretBuilt = true;
	}
}
