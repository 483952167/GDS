using UnityEngine;
using System.Collections;

public class ActionQueue {

	private Character parentChar;
	private Queue actionQueue = new Queue();

	public void Enqueue (Vector3 targetPos)
	{
		actionQueue.Enqueue (new MovementOrder (targetPos));
	}
	
	public void Enqueue (Ability spell, Character targetChar, Vector3 targetPos)
	{
		actionQueue.Enqueue (new CastOrder (spell, targetChar, targetPos));
	}
	

	// Use this for initialization
	void Start () {
	
	}
	


}










