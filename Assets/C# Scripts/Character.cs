using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour {
    public CharacterInstance characterPrefab;
	public CharacterStats stats = new CharacterStats();
	public ActionQueue actionQueue = new ActionQueue();
	private bool actionInProgress = false;
	private bool playerCasting = false; //player is inputting a spell, not character is casting
    private CharacterInstance character;
	private float attackCooldown = 0f;
	private CombatManager combatManager = new CombatManager();

    private GameObject cube;
    public Character target;
	public bool isPaused;

    private int status = CharacterStatus.WAITING;

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
		/*if (isPaused == false)
		{
			stats.ResolveBuffs ();
			actionQueue.Resolve ();
			AttackCooldownDecrement ();

        	//if(target != null)
        	//{
            //	moveToTarget();
        	//}
		}*/
	}

	void AttackCooldownDecrement ()
	{
		if (attackCooldown > 0)
		{
			attackCooldown -= Time.deltaTime;
		}
		else if (attackCooldown < 0)
		{
			attackCooldown = 0;
		}
	}
	
	public void Enqueue (Vector3 position) //move order
	{
		actionQueue.Enqueue (position);
	}

	public void Overwrite (Vector3 position) //move order
	{
		actionQueue.Overwrite (position);
	}

	public void Enqueue (Character c)
	{
		actionQueue.Enqueue (c);
	}

	public void Enqueue (Ability s, Character c, Vector3 p) //cast order
	{
		actionQueue.Enqueue (s, c, p);
	}

	public void SpellCast (Ability spell, CharacterCollection allies, EnemyCollection enemies)
	{
		playerCasting = true;
		while (playerCasting == true)
		{
			if (spell.targetOption == AbilityTargetOption.SELF)
			{
				Enqueue (spell, new Character(), new Vector3());
				playerCasting = false;
			}
			else if (spell.targetOption == AbilityTargetOption.TARGET_ALLY)
			{	//for now, targeting based on pressing numbers - will implement click-targeting
				if (Input.GetButtonDown ("Ability 1"))
				{
					Enqueue (spell, allies.getHero(0), new Vector3());
					playerCasting = false;
				}
				else if (Input.GetButtonDown ("Ability 2"))
				{
					Enqueue (spell, allies.getHero(1), new Vector3());
					playerCasting = false;
				}
				else if (Input.GetButtonDown ("Ability 3"))
				{
					Enqueue (spell, allies.getHero(2), new Vector3());
					playerCasting = false;
				}
			}
			else if (spell.targetOption == AbilityTargetOption.TARGET_ENEMY)
			{
				if (Input.GetButtonDown ("Ability 1"))
				{
					Enqueue (spell, enemies.getEnemy(0), new Vector3());
					playerCasting = false;
				}
				else if (Input.GetButtonDown ("Ability 2"))
				{
					Enqueue (spell, enemies.getEnemy(1), new Vector3());
					playerCasting = false;
				}
				else if (Input.GetButtonDown ("Ability 3"))
				{
					Enqueue (spell, enemies.getEnemy(2), new Vector3());
					playerCasting = false;
				}
			}
			else if (spell.targetOption == AbilityTargetOption.TARGET_LOCATION)
			{
					
			}
			else if (spell.targetOption == AbilityTargetOption.NONE)
			{
				Enqueue (spell, new Character(), new Vector3());
				playerCasting = false;
			}
		}
	}

	//for when the player hits a spell key, but then issues a different command
	public void PlayerCastInterrupt()
	{
		playerCasting = false;
	}

    public void Generate() {
        character = Instantiate(characterPrefab) as CharacterInstance;
        character.transform.parent = transform;
        transform.localPosition = new Vector3(0f, 0f, -0.5f);
        cube = character.transform.GetChild(0).gameObject;
		//cube.transform.position = character.transform.localPosition;
    }

    public void Generate(Vector3 pos) {
        character = Instantiate(characterPrefab) as CharacterInstance;
        character.transform.parent = transform;
        transform.localPosition = pos;
        cube = character.transform.GetChild(0).gameObject;
		//cube.transform.position = pos;
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
            if (dist > stats.AttackRange)
            {
                character.transform.localPosition += new Vector3(directionVector.Value.x * Time.deltaTime * stats.MoveSpeed / dist, 
                                                                 directionVector.Value.y * Time.deltaTime * stats.MoveSpeed / dist, 
                                                                 directionVector.Value.z * Time.deltaTime * stats.MoveSpeed / dist);

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

	public void TakeDamage (int damage)
	{
		stats.CurrentHealth -= damage;
	}

	public void ResolveMovementOrder(MovementOrder currentOrder)
	{
		Vector3 movementDirection = currentOrder.destination - character.transform.localPosition;
		if (movementDirection.magnitude < .1)
		{
			actionQueue.Pop();
			Debug.Log ("Character " + name + " completed movement order");
		}
		else
		{
			movementDirection.Normalize();
			character.transform.localPosition += movementDirection * Time.deltaTime * stats.MoveSpeed;
		}
	}

	public void ResolveAttackOrder(AttackOrder currentOrder)
	{
		Character attackTarget = currentOrder.target;
		Vector3 attackVector = attackTarget.getCharacterPosition () - character.transform.localPosition;
		float attackDistance = attackVector.magnitude;

		if (attackDistance > stats.AttackRange)
		{ //if out of range, move towards target
			attackVector.Normalize();
			character.transform.localPosition += attackVector * Time.deltaTime * stats.MoveSpeed;
		}
		else
		{
			if (attackCooldown == 0)
			{
				combatManager.Hit(this, attackTarget);
			}
		}
	}
}
