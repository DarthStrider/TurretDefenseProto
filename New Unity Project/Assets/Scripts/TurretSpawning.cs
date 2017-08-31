using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretSpawning : MonoBehaviour
{
	[SerializeField] private GameObject _turrentOptions;
	[SerializeField] private GameObject _crossbowTurret;
	[SerializeField] private GameObject _acidTurret;
	[SerializeField] private GameObject _cannonTurret;

	public enum TurretType
	{
		Cannon,
		Acid,
		Crossbow
	}
	
	
	private bool _turretEnabled;
	private bool _isTurretBuilt;

	// Use this for initialization
	void Start ()
	{
		_turretEnabled = false;
		_isTurretBuilt = false;
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
		BuildTurret(_crossbowTurret);
	}
	
	public void BuildAcidTurrent()
	{
		BuildTurret(_acidTurret);
	}
	public void BuildCannonTurret()
	{
		BuildTurret(_cannonTurret);
	}
	

	private void BuildTurret(GameObject turret)
	{
		var gun = Instantiate(turret, transform.position, Quaternion.identity);
		gun.transform.forward = transform.forward;
		TurretOptionsSwitch();
		_isTurretBuilt = true;
	}
}
