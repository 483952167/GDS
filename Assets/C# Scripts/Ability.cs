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
	public void Resolve (Character targetChar, Vector3 targetLocation) {

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
