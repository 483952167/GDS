using UnityEngine;
using System.Collections;

public class InputManager {

	private CharacterCollection allies;
	private EnemyCollection enemies;
	private Character selected;
	private bool isCharacterUnderMouse;
	private Character characterUnderMouse;
	private Vector3 mousePosition;

	int floorMask;
	int characterMask;
	float camRayLength = 100f;

	public CharacterCollection Allies
	{
		get { return allies; }
		set { allies = value; }
	}

	public EnemyCollection Enemies
	{
		get { return enemies; }
		set { enemies = value; }
	}

	// Use this for initialization
	void Start () {
	
	}

	public void Awake () {
		floorMask = LayerMask.GetMask ("Floor");
		characterMask = LayerMask.GetMask ("Character");
	}

	public void Resolve () {
		//Debug.Log ("Inside inputManager.Resolve()");
		UpdateMousePosition ();

		if (Input.GetButtonDown ("Select Character A"))
		{
			selected = allies.getHero(0);
			Debug.Log ("Character A selected");
		}
		else if (Input.GetButtonDown ("Select Character B"))
		{
			selected = allies.getHero(1);
			Debug.Log ("Character B selected");
		}
		else if (Input.GetButtonDown ("Select Character C"))
		{
			selected = allies.getHero(2);
			Debug.Log ("Character C selected");
		}
		
		if (Input.GetMouseButtonDown(1)) //right click
		{
			Debug.Log ("Right click");
			CharacterMouseCheck();

			if (isCharacterUnderMouse == false) //player right clicked on ground, issue move order
			{
				selected.Enqueue(mousePosition);
				Debug.Log ("Queueing move order to " + mousePosition.x + ", " + mousePosition.y);
			}
			else //player right clicked on a character, issue attack order
			{
				selected.Enqueue(characterUnderMouse);
				Debug.Log ("Queueing attack order on " + characterUnderMouse.name);
			}
		}

		if (Input.GetMouseButtonDown (0)) //left click
		{
			CharacterMouseCheck ();
			Debug.Log ("Left click");

			if (isCharacterUnderMouse)
			{
				for (int i = 0; i <= 2; i++)
				{
					if (Allies.getHero(i) == characterUnderMouse)
					{
						selected = characterUnderMouse;
						Debug.Log ("Click selected " + selected.name);
					}
				}
			}
		}
		
		if (Input.GetButtonDown ("Ability 1"))
		{
			Ability spell = selected.stats.abilities.ToArray()[0];
			selected.SpellCast(spell, allies, enemies);
		}
		else if (Input.GetButtonDown ("Ability 2"))
		{
			Ability spell = selected.stats.abilities.ToArray()[1];
			selected.SpellCast(spell, allies, enemies);
		}
		else if (Input.GetButtonDown ("Ability 3"))
		{
			Ability spell = selected.stats.abilities.ToArray()[2];
			selected.SpellCast(spell, allies, enemies);
		}
		else if (Input.GetButtonDown ("Ability 4"))
		{
			Ability spell = selected.stats.abilities.ToArray()[3];
			selected.SpellCast(spell, allies, enemies);
		}
		else if (Input.GetButtonDown ("Ability 5"))
		{
			Ability spell = selected.stats.abilities.ToArray()[4];
			selected.SpellCast(spell, allies, enemies);
		}
	}

	void UpdateMousePosition () {
		// Create a ray from the mouse cursor on screen in the direction of the camera.
		Ray camRay = Camera.main.ScreenPointToRay (Input.mousePosition);
		
		// Create a RaycastHit variable to store information about what was hit by the ray.
		RaycastHit floorHit;
		
		// Perform the raycast and if it hits something on the floor layer...
		if(Physics.Raycast (camRay, out floorHit, camRayLength, floorMask))
		{
			mousePosition = floorHit.point;
			mousePosition.z = 0f;
		}
	}

	void CharacterMouseCheck () {
		Ray camRay = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit characterHit;

		if (Physics.Raycast (camRay, out characterHit, camRayLength, characterMask))
		{
			isCharacterUnderMouse = true;
			characterUnderMouse = characterHit.collider.GetComponent <Character> ();
			Debug.Log ("Character under mouse = " + characterUnderMouse.name);
		}
		else
		{
			isCharacterUnderMouse = false;
		}
	}
}
