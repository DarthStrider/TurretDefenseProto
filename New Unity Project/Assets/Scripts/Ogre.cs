using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ogre : Monster {

	[SerializeField] private int _goldGiven;

	// Use this for initialization
    public override void Start () {
        base.Start(); 
    }
	
	// Update is called once per frame
	public override void Update () {
		base.Update();
	}
	
	private void TookDamage(float amount)
	{
		if (_health - amount <= 0)
		{
			_health = 0;
			_animator.SetBool ("dead", true);
			StartCoroutine(WaitToDestroy());
			EconomyManager.instance.Gold += _goldGiven;

		}
		else
		{
			_health -= amount;
			SetHealthBar();
		}
	}

	
}
