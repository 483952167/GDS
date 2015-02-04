using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour {

	private Vector3 currentPosition;
	private Character thisCharater;
	private Character target;
	private Vector3 targetPosition;
	private float detectRange = 5.0f;
	private float duration = 20.0f;


	// Use this for initialization
	void Start () {
		currentPosition = gameObject.transform.position;

		thisCharater = GetComponent<Character> ();
		target = thisCharater.target;
		targetPosition = target.gameObject.transform.position;

	}
	
	// Update is called once per frame
	void Update () {
		move ();
		currentPosition = gameObject.transform.position;
		targetPosition = target.gameObject.transform.position;
		Debug.Log ("enemy position" + currentPosition);
		Debug.Log ("target position" + targetPosition);
	}

	private void move () {
		if (Mathf.Abs (Vector3.Distance (currentPosition, targetPosition)) < detectRange && Mathf.Abs (Vector3.Distance (currentPosition, targetPosition)) > 1.0f) {
			gameObject.transform.position = Vector3.Lerp (currentPosition, targetPosition, 1 / (duration * (Vector3.Distance (currentPosition, targetPosition))));
		} 
	}

}
