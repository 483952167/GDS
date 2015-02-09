using UnityEngine;
using System.Collections;

public class Heal : Ability {

	public Character caster;

	public void Start ()
	{
		targetOption = AbilityTargetOption.TARGET_ALLY;
		manaCost = 10;
		cooldown = 5f;
		castTime = 3f;
		range = 5f;
		name = "Heal";
	}
	
	public void Resolve (Character targetChar, Vector3 targetLocation)
	{
		targetChar.stats.CurrentHealth += 10;
		if (targetChar.stats.CurrentHealth > targetChar.stats.MaxHealth)
		{
			targetChar.stats.CurrentHealth = targetChar.stats.MaxHealth;
		}
	}

	public void Add ()
	{

	}
}
