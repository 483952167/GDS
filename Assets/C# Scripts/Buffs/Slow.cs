using UnityEngine;
using System.Collections;

public class Slow : Buff {

	// Use this for initialization
	void Start () {
		duration = 5.0f;
		debuff = true;
		elapsedTime = 0f;
		target.stats.MoveSpeed = .5f * target.stats.MoveSpeed; //cut speed in half
	}

	void Resolve () {
		if (elapsedTime >= duration)
		{
			target.stats.MoveSpeed = 2 * target.stats.MoveSpeed; //return speed to normal
			target.stats.RemoveBuff(this);
		}
		elapsedTime += Time.deltaTime;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
