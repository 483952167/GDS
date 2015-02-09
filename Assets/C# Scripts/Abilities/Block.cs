using UnityEngine;
using System.Collections;

public class Block : Ability {

	// Use this for initialization
	void Start () {
		targetOption = AbilityTargetOption.SELF;
		manaCost = 5;
		cooldown = 10f;
		castTime = 0f;
		range = 0f;
	}


	
	// Update is called once per frame
	void Update () {
	
	}
}
