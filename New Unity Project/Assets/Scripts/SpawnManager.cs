using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.UI;

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
	[SerializeField] private int _waveDelay;
	[SerializeField] private Text _timerText;
	[SerializeField] private GameObject _timer;

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
			IntializeWave();
		}
	}

	private IEnumerator StartWave()
	{
		_timer.SetActive(true);
		int wait = _waveDelay;
		_timerText.text = wait.ToString();
		while (wait >= 0)
		{
			yield return new WaitForSecondsRealtime(1);
			--wait;
			_timerText.text = wait.ToString();
		}
		_timer.SetActive(false);
		InitializeMonsters();
	}

	private void InitializeMonsters()
	{
		
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
