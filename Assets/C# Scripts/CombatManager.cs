using UnityEngine;
using System.Collections;

public class CombatManager {

	public void Hit (Character attacker, Character defender)
	{
		int attackDamage = 0;
		bool magicAttack = attacker.stats.MagicAttack;
		bool hit = false, crit = false;

		int hitChance = attacker.stats.HitRate - defender.stats.DodgeRate;
		if (Random.Range(0,100) < hitChance)
		{
			hit = true;
		}

		if (Random.Range(0,100) < attacker.stats.CritRate)
		{
			crit = true;
		}

		if (crit)
		{
			attackDamage = attacker.stats.AttackDamage;
		}
		else
		{
			if (magicAttack)
			{
				attackDamage = attacker.stats.AttackDamage - defender.stats.MagicResist;
			}
			else
			{
				attackDamage = attacker.stats.AttackDamage - defender.stats.Armor;
			}
		}

		//no matter what, no attack does 0 damage
		if (attackDamage < 1)
		{
			attackDamage = 1;
		}

		if (hit)
		{
			defender.TakeDamage(attackDamage);
		}

		if (crit && hit)
		{
			Debug.Log ("Crit!");
		}
		else if (hit)
		{
			Debug.Log ("Hit!");
		}
		else
		{
			Debug.Log ("Miss!");
		}

	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
