using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    public Room roomPrefab;
    private Room roomInstance;

    public Character characterPrefab;
    private Character character;

    private Character enemy;

    public Material enemyMaterial;
    public Material heroMaterial;

	// Use this for initialization
	void Start () {
        BeginGame();
	}
	
	// Update is called once per frame
	void Update () {

	}

    private void BeginGame() {
        roomInstance = Instantiate(roomPrefab) as Room;
        roomInstance.Generate();

        character = Instantiate(characterPrefab) as Character;
        character.Generate();

        enemy = Instantiate(characterPrefab) as Character;
        enemy.Generate(new Vector3(2.7f, 5.0f, 0f));
        enemy.setMaterial(enemyMaterial);

        character.setTarget(enemy);
        enemy.setTarget(character);

        EnemyCollection.addEnemy(enemy);
    }

    private void RestartGame() {
        Destroy(roomInstance.gameObject);
        BeginGame();
    }
}
