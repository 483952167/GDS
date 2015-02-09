using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour {
    public CharacterInstance characterPrefab;
	public CharacterStats stats = new CharacterStats();
	public ActionQueue actionQueue = new ActionQueue();
	private bool actionInProgress = false;
	public bool playerCasting = false; //player is inputting a spell, not character is casting
    private CharacterInstance character;
	private float attackCooldown = 0f;
	private float timeUntilCast = 0f;
	private CombatManager combatManager = new CombatManager();
	private InputManager inputManager = new InputManager();
	public Ability currentSpell;

    private GameObject cube;
    private Character target;
	public bool isPaused;
	public bool isDead = false;

    private int status = CharacterStatus.WAITING;

	// Use this for initialization
	void Start () {
	    
	}

	public void SetCastTime (float castTime)
	{
		timeUntilCast = castTime;
	}
	
	// Update is called once per frame
	void Update () {
		if (isPaused == false)
		{
			DeathCheck ();
			stats.ResolveBuffs ();
			actionQueue.Resolve ();
			AttackCooldownDecrement ();

        	//if(target != null)
        	//{
            //	moveToTarget();
        	//}
		}
	}

	void DeathCheck ()
	{
		if (stats.CurrentHealth <= 0)
		{
			stats.CurrentHealth = 0;
			isDead = true;
		}
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

	public void Overwrite (Vector3 position)
	{
		actionQueue.Overwrite (position);
	}

	public void Enqueue (Character c) //attack order
	{
		actionQueue.Enqueue (c);
	}

	public void Overwrite (Character c)
	{
		actionQueue.Overwrite (c);
	}

	public void Enqueue (Ability s, Character c, Vector3 p) //cast order
	{
		actionQueue.Enqueue (s, c, p);
	}

	public void Overwrite (Ability s, Character c, Vector3 p)
	{
		actionQueue.Overwrite (s, c, p);
	}

	public void SpellCast (Ability spell, CharacterCollection allies, EnemyCollection enemies)
	{
		if (playerCasting == true)
		{
			if (spell.targetOption == AbilityTargetOption.SELF)
			{
				Enqueue (spell, new Character(), new Vector3());
				playerCasting = false;
			}
			else if (spell.targetOption == AbilityTargetOption.TARGET_ALLY)
			{
				Debug.Log ("Target ally");
				currentSpell = spell;
			}
			else if (spell.targetOption == AbilityTargetOption.TARGET_ENEMY)
			{
				Debug.Log ("Target enemy");
				currentSpell = spell;
			}
			else if (spell.targetOption == AbilityTargetOption.TARGET_LOCATION)
			{
				Debug.Log ("Target location");
			}
			else if (spell.targetOption == AbilityTargetOption.NONE)
			{
				Enqueue (spell, new Character(), new Vector3());
				playerCasting = false;
			}
		}
	}

	//for when the player hits a spell key, but then issues a different command or cancels
	public void PlayerCastInterrupt()
	{
		Debug.Log (stats.Name + " cast interrupt");
		playerCasting = false;
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

    public void setCharacterPosition(Vector3 pos) {
        transform.localPosition = pos;
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
		DeathCheck ();
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

		if (attackTarget.isDead == true)
		{
			Debug.Log ("Attack target of " + stats.Name + " is dead. Cancelling attack order");
			actionQueue.Pop ();
		}

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
				Debug.Log ("Character " + stats.Name + " attacks " + attackTarget.stats.Name + ".");
				Debug.Log (attackTarget.stats.Name + "'s HP is now " + attackTarget.stats.CurrentHealth);
				attackCooldown = stats.AttackRate;
			}
		}
	}

	public void ResolveCastOrder (CastOrder currentOrder)
	{
		Character targetCharacter = currentOrder.targetCharacter;
		Vector3 targetLocation = currentOrder.targetLocation;
		Ability spell = currentOrder.spell;

		Vector3 characterVector = targetCharacter.getCharacterPosition () - character.transform.localPosition;
		float targetDistance = characterVector.magnitude;

		if (stats.CurrentMana < spell.manaCost)
		{
			Debug.Log ("Not enough mana!");
		}
		else if (targetDistance > spell.range)
		{
			characterVector.Normalize();
			character.transform.localPosition += characterVector * Time.deltaTime * stats.MoveSpeed;
		}
		else
		{
			if (timeUntilCast > 0f)
			{
				Debug.Log ("timeUntilCast = " + timeUntilCast);
				timeUntilCast -= Time.deltaTime;
			}
			else
			{
				stats.CurrentMana -= spell.manaCost;
				Debug.Log ("Spell " + spell.name + " resolving");
				spell.Resolve (targetCharacter, targetLocation);
				actionQueue.Pop();
			}
		}
	}
}
