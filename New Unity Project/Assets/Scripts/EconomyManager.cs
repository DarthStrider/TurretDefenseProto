using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EconomyManager : MonoBehaviour
{

	public static EconomyManager instance;

	[SerializeField] private TextMeshProUGUI _goldText;
	[SerializeField] private int _gold;
	public int Gold
	{
		get {return _gold;}
		set
		{
			_gold = value;
			_goldText.text = _gold.ToString();
		}
	}

	[SerializeField] private int _crossBowTurretCost;
	[SerializeField] private int _acidTurretCost;
	[SerializeField] private int _cannonTurretCost;

	public int CrossBowTurretCost
	{
		get { return _crossBowTurretCost; }
	}

	public int AcidTurretCost
	{
		get { return _acidTurretCost; }
	}

	public int CannonTurretCost
	{
		get { return _cannonTurretCost; }
	}
	

	// Use this for initialization
	void Awake ()
	{
		instance = this;
	}

	private void Start()
	{
		_goldText.text = _gold.ToString();
	}

	// Update is called once per frame
	void Update () {
		
	}
}
