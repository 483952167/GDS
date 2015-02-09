using UnityEngine;
using System.Collections;

public class ActionQueue {

	private Character parentChar;
	private Queue actionQueue = new Queue();

	public Character ParentChar
	{
		get { return parentChar; }
		set { parentChar = value; }
	}

	public void Enqueue (Vector3 targetPos)
	{
		actionQueue.Enqueue (new MovementOrder (targetPos));
	}

	public void Enqueue (Character targetChar)
	{
		actionQueue.Enqueue (new AttackOrder (targetChar));
	}
	
	public void Enqueue (Ability spell, Character targetChar, Vector3 targetPos)
	{
		actionQueue.Enqueue (new CastOrder (spell, targetChar, targetPos));
	}

	public void Overwrite (Vector3 targetPos)
	{
		Clear ();
		actionQueue.Enqueue (new MovementOrder (targetPos));
	}

	public void Overwrite (Character targetChar)
	{
		Clear ();
		actionQueue.Enqueue (new AttackOrder (targetChar));
	}

	public void Overwrite (Ability spell, Character targetChar, Vector3 targetPos)
	{
		Clear ();
		actionQueue.Enqueue (new CastOrder (spell, targetChar, targetPos));
	}

	public void Clear ()
	{
		parentChar.PlayerCastInterrupt ();
		actionQueue.Clear ();
	}
	

	// Use this for initialization
	void Start () {
	
	}
	
	public void Resolve ()
	{
		if (actionQueue.Count > 0)
		{
			if (actionQueue.Peek() is MovementOrder)
			{
				MovementOrder currentOrder = (MovementOrder) actionQueue.Peek ();
				parentChar.ResolveMovementOrder(currentOrder);
			}
			if (actionQueue.Peek () is AttackOrder)
			{
				AttackOrder currentOrder = (AttackOrder) actionQueue.Peek ();
				parentChar.ResolveAttackOrder(currentOrder);
			}
			if (actionQueue.Peek () is CastOrder)
			{
				CastOrder currentOrder = (CastOrder) actionQueue.Peek ();
				parentChar.ResolveCastOrder(currentOrder);
			}
		}
	}

	public void Pop ()
	{
		actionQueue.Dequeue ();
	}

}










