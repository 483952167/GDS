using UnityEngine;
using System.Collections;

public class Buff {

	public Character target;
	public float duration, elapsedTime;
	public bool debuff; // if false, treated as "buff" (beneficial), if true, "debuff" (negative) - may matter for other spells
						// ex. a Purge spell that removes debuffs from an ally or buffs from an enemy

	// Use this for initialization
	public void Start () {

	}
	
	// Update is called once per frame
	void Update () {

	}

	// Resolve function handles application of effects - damage, stat changes, etc.
	public void Resolve () {

	}


}
