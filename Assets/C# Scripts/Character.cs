using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour {
    public CharacterInstance characterPrefab;
    private CharacterInstance character;

    private int maxHealth;
    private int currentHealth;
    private int maxMana;
    private int currentMana;
    private float attackRange = 1.30f;

    private GameObject cube;
    private Character target;

    private int status = CharacterStatus.WAITING;

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
        if(target != null)
        {
            moveToTarget();
        }
	}

    public void Generate() {
        character = Instantiate(characterPrefab) as CharacterInstance;
        character.transform.parent = transform;
        character.transform.localPosition = new Vector3(0f, 0f, 0f);
        cube = character.transform.GetChild(0).gameObject;
    }

    public void Generate(Vector3 pos) {
        character = Instantiate(characterPrefab) as CharacterInstance;
        character.transform.parent = transform;
        character.transform.localPosition = pos;
        cube = character.transform.GetChild(0).gameObject;
    }

    public void MoveForward() {
        character.transform.localPosition += new Vector3(0f, 1f * Time.deltaTime, 0f);
    }

    public void MoveDown() {
        character.transform.localPosition -= new Vector3(0f, 1f * Time.deltaTime, 0f);
    }

    public void setMaterial(Material mat) {
        cube.renderer.material = mat;
    }

    public Vector3 getCharacterPosition() {
        return character.transform.localPosition;
    }

    public void setTarget(Character c) {
        target = c;
    }

    private int moveToTarget() {
        Vector3? directionVector = getDirectionVector();
        if(directionVector != null)
        {
            float dist = directionVector.Value.magnitude;
            if (dist > attackRange)
            {
                character.transform.localPosition += new Vector3(directionVector.Value.x * Time.deltaTime, 
                                                                 directionVector.Value.y * Time.deltaTime, 
                                                                 directionVector.Value.z * Time.deltaTime);

                return CharacterStatus.MOVING;
            }
            return CharacterStatus.ARRIVED;
        }
        return CharacterStatus.WAITING;
        
    }

    private Vector3? getDirectionVector() {
        if(target != null)
        {
            Vector3 targetPos = target.getCharacterPosition();
            Vector3 pos = character.transform.localPosition;
            return targetPos - pos;
        }
        return null;
    }
}
