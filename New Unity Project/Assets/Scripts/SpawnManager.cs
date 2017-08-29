using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour 
{
	enum MonsterTyoe
	{
		Ogre,
		Troll,
		Orc
	}

	public static SpawnManager instance;
	[SerializeField] private List<Wave> _waves;
	[SerializeField] private Transform _instantiatePostion1;
	[SerializeField] private Transform _instantiatePostion2;
	[SerializeField] private GameObject _ogre;
	[SerializeField] private GameObject _orc;
	[SerializeField] private GameObject _troll;

	private int _wavesCount;
	private int _wavesLeft;
	private int _waveCount;

	

	// Use this for initialization
	void Start ()
	{
		instance = this;
		_wavesCount = 0;
		_wavesLeft = _waves.Count;
	}

	private void IntializeWave()
	{
		if (_wavesLeft != 0)
		{
			--_wavesLeft;
			_waveCount = _waves[_wavesLeft].Ogre + _waves[_wavesLeft].Orc + _waves[_wavesLeft].Troll;
		}
	}

	public void MonsterDied()
	{
		--_waveCount;
		if (_waveCount == 0)
		{
			
		}
	}
	
	
	// Update is called once per frame
	void Update () {
		
	}
}

[Serializable]
public class Wave 
{
	 public int Ogre;
	 public int Orc;
	 public int Troll;
}
