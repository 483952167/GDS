using UnityEngine;
using System.Collections;

public class ClickToMove : MonoBehaviour {

	//flag to check if the user has tapped / clicked. 
	//Set to true on click. Reset to false on reaching destination
	private bool flag = false;
	private Vector3 endPoint;
	//alter this to change the speed of the movement of player / gameobject
	private float duration = 5.0f;
	//vertical position of the gameobject
	private float zAxis;
	//0 means clicked on ground, 1 means clicked on character
	private int clickStatus;



	void Start(){
		//save the y axis value of gameobject
		zAxis = gameObject.transform.position.z;
	}
	
	// Update is called once per frame
	void Update () {

		moveByClicking ();

	}

	private void moveByClicking () {
		//check if the screen is clicked   
		if (Input.GetMouseButtonDown(1)) {
			//declare a variable of RaycastHit struct
			RaycastHit hit;
			//Create a Ray on the clicked position
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

			
			//Check if the ray hits any collider
			if(Physics.Raycast(ray, out hit)) {
                Debug.Log(hit.transform.gameObject.tag);
				//set a flag to indicate to move the gameobject
				flag = true;
				//save the click / tap position
				endPoint = hit.point;
				//as we do not want to change the y axis value based on touch position, reset it to original y axis value
				endPoint.z = zAxis;
				if (hit.transform.gameObject.tag == "Ground") {
					clickStatus = 0;
				} 
				else if (hit.transform.gameObject.tag == "Character") {
					clickStatus = 1;
				}
			}
		}
		if (clickStatus == 0) {
			if (flag && !(Vector3.Distance(gameObject.transform.position, endPoint) == 0)) {//!Mathf.Approximately (gameObject.transform.position.magnitude, endPoint.magnitude)) { 
				//move the gameobject to the desired position
				gameObject.transform.position = Vector3.Lerp (gameObject.transform.position, endPoint, 1 / (duration * (Vector3.Distance (gameObject.transform.position, endPoint))));
			}
			//set the movement indicator flag to false if the endPoint and current gameobject position are equal
			else if (flag && Vector3.Distance(gameObject.transform.position, endPoint) == 0) {//Mathf.Approximately (gameObject.transform.position.magnitude, endPoint.magnitude)) {
				flag = false;
				Debug.Log ("I am here");
			}
		} 
		else if (clickStatus == 1){
			if (flag && !(Vector3.Distance(gameObject.transform.position, endPoint) < 1.0f)) { 
				//move the gameobject to the desired position
				gameObject.transform.position = Vector3.Lerp (gameObject.transform.position, endPoint, 1 / (duration * (Vector3.Distance (gameObject.transform.position, endPoint))));
			}
			//set the movement indicator flag to false if the endPoint and current gameobject position are equal
			else if (flag && Vector3.Distance(gameObject.transform.position, endPoint) < 1.0f) {
				flag = false;
				Debug.Log ("I am here");
			}
		}
	}


}
