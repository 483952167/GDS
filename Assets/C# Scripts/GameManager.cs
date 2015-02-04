using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public bool isPaused = true;

    public Room roomPrefab;
    private Room roomInstance;

	private InputManager inputManager = new InputManager();

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
		/*allies.addHero (playerCharA);
		allies.addHero (playerCharB);
		allies.addHero (playerCharC);
		enemies.addEnemy (enemy);
		inputManager.Awake ();*/
	}
	
	// Update is called once per frame
	void Update () {
		/*inputManager.Allies = allies;
		inputManager.Enemies = enemies;

		if (Input.GetButtonDown("Pause"))
		{
			isPaused = !isPaused;
		}

		playerCharA.isPaused = isPaused;
		playerCharB.isPaused = isPaused;
		playerCharC.isPaused = isPaused;
		enemy.isPaused = isPaused;

		inputManager.Resolve ();*/

	}

    private void BeginGame() {
        roomInstance = Instantiate(roomPrefab) as Room;
        roomInstance.Generate();

        playerCharA = Instantiate(characterPrefab) as Character;
		playerCharA.Generate(new Vector3(-5.0f, -5.0f, -0.5f));
		//playerCharA.actionQueue.ParentChar = playerCharA;
		playerCharA.name = "Hero A";

		/*playerCharB = Instantiate (characterPrefab) as Character;
		playerCharB.Generate (new Vector3 (-2.7f, 0f, 0f));
		//playerCharB.actionQueue.ParentChar = playerCharB;
		playerCharB.name = "Hero B";

		playerCharC = Instantiate (characterPrefab) as Character;
		playerCharC.Generate (new Vector3 (-2.7f, 5.0f, 0f));
		//playerCharC.actionQueue.ParentChar = playerCharC;
		playerCharC.name = "Hero C";*/
		
		enemy = Instantiate(characterPrefab) as Character;
        enemy.Generate(new Vector3(2.7f, 5f, -0.5f));
        enemy.setMaterial(enemyMaterial);
		enemy.name = "Enemy";
		Destroy (enemy.GetComponent<ClickToMove> ());

        //playerCharA.setTarget(enemy);
        //enemy.setTarget(playerCharA);

        enemies.addEnemy(enemy);
    }

    private void RestartGame() {
        Destroy(roomInstance.gameObject);
        BeginGame();
    }


}
