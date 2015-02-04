using UnityEngine;
using System.Collections;

public class CharacterInstance : MonoBehaviour {

	public Character parentChar;

	public void SetParentChar (Character c)
	{
		parentChar = c;
	}

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
