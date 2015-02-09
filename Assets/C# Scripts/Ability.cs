using UnityEngine;
using System.Collections;

public class Ability {

	public Character caster;
	public int targetOption;
	public int manaCost;
	public float cooldown, castTime, range;

	public string name, tooltip;

	// Use this for initialization
	public void Start () {
	
	}

	// Resolve function handles actual effects of spell - damage, buff application, healing, etc.
	public virtual void Resolve (Character targetChar, Vector3 targetLocation) {
		Debug.Log ("Ability resolve - you should not see this");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
