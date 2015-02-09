using UnityEngine;
using System.Collections;

public class Frostbolt : Ability {

	public void Start ()
	{
		targetOption = AbilityTargetOption.TARGET_ENEMY;
		manaCost = 10;
		cooldown = 5f;
		castTime = 3f;
		range = 5f;
		name = "Frostbolt";
	}

	public void Resolve (Character targetChar, Vector3 targetLocation)
	{
		Slow slow = new Slow ();
		targetChar.stats.CurrentHealth -= 5;
		targetChar.stats.AddBuff (slow);
	}

}
