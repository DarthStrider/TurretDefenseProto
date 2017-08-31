using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

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
	[SerializeField] private TextMeshProUGUI _timerText;
	[SerializeField] private GameObject _timer;

	private int _wavesCount;
	private int _waveCount;
	private int _currentWave;

	

	// Use this for initialization
	void Start ()
	{
		instance = this;
		_wavesCount = 0;
		_currentWave = 0;
		IntializeWave();
	}

	private void IntializeWave()
	{
		if (_currentWave < _waves.Count)
		{
			_waveCount = _waves[_currentWave].Ogre + _waves[_currentWave].Orc + _waves[_currentWave].Troll;
			StartCoroutine(StartWave());
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
		var wave = _waves[_currentWave];
		++_currentWave;
		InitializeMonster(MonsterTyoe.Ogre, wave.Ogre);
		InitializeMonster(MonsterTyoe.Orc, wave.Orc);
		InitializeMonster(MonsterTyoe.Troll,wave.Troll);
	}

	private void InitializeMonster(MonsterTyoe type, int amount)
	{
		GameObject monster;
		if (type == MonsterTyoe.Ogre)
		{
			monster = _ogre;
		}
		else if (type == MonsterTyoe.Orc)
		{
			monster = _orc;
		}
		else
		{
			monster = _troll;
		}
		for (int i = 0; i < amount; i++)
		{
			int intializeSpot = Random.Range(1, 3);
			if (intializeSpot == 1)
			{
				var enemy = Instantiate(monster, _instantiatePostion1.position, Quaternion.identity);
				enemy.transform.parent = _instantiatePostion1.transform;
			}
			else
			{
				var enemy = Instantiate(monster, _instantiatePostion2.position, Quaternion.identity);
				enemy.transform.parent = _instantiatePostion2.transform;
			}
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
