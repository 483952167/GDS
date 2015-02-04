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
		}
	}

	public void Pop ()
	{
		actionQueue.Dequeue ();
	}

}










