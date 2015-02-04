using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public bool isPaused = true;

    public Room roomPrefab;
    private Room roomInstance;

	private Character selected;

    public Character characterPrefab;
    private Character playerCharA;
	private Character playerCharB;
	private Character playerCharC;

    private Character enemy;

	private CharacterCollection allies = new CharacterCollection();
	private EnemyCollection enemies = new EnemyCollection();

    public Material enemyMaterial;
    public Material heroMaterial;

	// Use this for initialization
	void Start () {
        BeginGame();
		allies.addHero (playerCharA);
		allies.addHero (playerCharB);
		allies.addHero (playerCharC);
		enemies.addEnemy (enemy);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown("Pause"))
		{
			isPaused = !isPaused;
		}

		playerCharA.isPaused = isPaused;
		playerCharB.isPaused = isPaused;
		playerCharC.isPaused = isPaused;
		enemy.isPaused = isPaused;

		if (Input.GetButtonDown ("Select Character A"))
		{
			selected = playerCharA;
		}
		else if (Input.GetButtonDown ("Select Character B"))
		{
			selected = playerCharB;
		}
		else if (Input.GetButtonDown ("Select Character C"))
		{
			selected = playerCharC;
		}

		if (Input.GetMouseButtonDown(1)) //right click
		{
			Vector3 clickPosition = Input.mousePosition; // this needs to be modified, it returns the position on the screen, not our 
														 // floor layer. See raycasting in the Unity Survival Shooter tutorial
														 // (specifically, player turning; same idea)
			selected.Enqueue(clickPosition);
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

    private void BeginGame() {
        roomInstance = Instantiate(roomPrefab) as Room;
        roomInstance.Generate();

        playerCharA = Instantiate(characterPrefab) as Character;
		playerCharA.Generate(new Vector3(-2.7f, -5.0f, 0f));

		playerCharB = Instantiate (characterPrefab) as Character;
		playerCharB.Generate (new Vector3 (-2.7f, 0f, 0f));

		playerCharC = Instantiate (characterPrefab) as Character;
		playerCharC.Generate (new Vector3 (-2.7f, 5.0f, 0f));
		
		enemy = Instantiate(characterPrefab) as Character;
        enemy.Generate(new Vector3(2.7f, 5.0f, 0f));
        enemy.setMaterial(enemyMaterial);

        playerCharA.setTarget(enemy);
        enemy.setTarget(playerCharA);

        enemies.addEnemy(enemy);
    }

    private void RestartGame() {
        Destroy(roomInstance.gameObject);
        BeginGame();
    }


}
