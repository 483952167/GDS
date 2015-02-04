using UnityEngine;
using System.Collections;

public class Ability : MonoBehaviour {

	public Character caster;
	public int targetOption;
	public int manaCost;
	public float cooldown, castTime, range;

	public string tooltip;

	// Use this for initialization
	void Start () {
	
	}

	// Resolve function handles actual effects of spell - damage, buff application, healing, etc.
	public void Resolve () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
