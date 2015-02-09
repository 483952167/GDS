using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour
{
    private Character thisCharater;
    private Character target;
    private Vector3 originalPosition;
    private Vector3 currentPosition;
    private Vector3 targetPosition;
    private Vector3 patrolPosition1;
    private Vector3 patrolPosition2;
    private float detectRange = 5.0f;
    private float patrolRange = 2.0f;

    //controls the speed of the enemy
    private float duration = 20.0f;
    private int patrolSwitch;

    // Use this for initialization
    void Start() {
        enemyInitialization();
    }

    // Update is called once per frame
    void Update() {
        move();
        currentPosition = gameObject.transform.position;
        targetPosition = target.gameObject.transform.position;
        //Debug.Log ("enemy position" + currentPosition);
        //Debug.Log ("target position" + targetPosition);
    }

    private void move() {
        // when enemy detects a player
        if (Vector3.Distance(currentPosition, targetPosition) < detectRange && Vector3.Distance(currentPosition, targetPosition) > 1.0f)
        {
            gameObject.transform.position = Vector3.Lerp(currentPosition, targetPosition, 1 / (duration * (Vector3.Distance(currentPosition, targetPosition))));
        }

            // patrol movement of enemy
        else if (Vector3.Distance(currentPosition, targetPosition) > detectRange)
        {
            // outside of patrol range
            if (Vector3.Distance(currentPosition, originalPosition) > patrolRange)
            {
                gameObject.transform.position = Vector3.Lerp(currentPosition, originalPosition, 1 / (2 * duration * (Vector3.Distance(currentPosition, originalPosition))));
            }
            // inside of patrol range
            else
            {
                if (patrolSwitch == 1 && Vector3.Distance(currentPosition, patrolPosition1) != 0)
                {
                    gameObject.transform.position = Vector3.Lerp(currentPosition, patrolPosition1, 1 / (2 * duration * (Vector3.Distance(currentPosition, patrolPosition1))));
                }
                else if (patrolSwitch == 1 && Vector3.Distance(currentPosition, patrolPosition1) == 0)
                {
                    patrolSwitch = 2;
                }
                else if (patrolSwitch == 2 && Vector3.Distance(currentPosition, patrolPosition2) != 0)
                {
                    gameObject.transform.position = Vector3.Lerp(currentPosition, patrolPosition2, 1 / (2 * duration * (Vector3.Distance(currentPosition, patrolPosition2))));
                }
                else if (patrolSwitch == 2 && Vector3.Distance(currentPosition, patrolPosition2) == 0)
                {
                    patrolSwitch = 1;
                }
            }
        }
    }

    private void enemyInitialization() {
        originalPosition = gameObject.transform.position;
        currentPosition = originalPosition;
        patrolPosition1 = new Vector3(originalPosition.x + patrolRange, originalPosition.y, originalPosition.z);
        patrolPosition2 = new Vector3(originalPosition.x - patrolRange, originalPosition.y, originalPosition.z);
        patrolSwitch = 1;
        thisCharater = GetComponent<Character>();
        target = thisCharater.getTarget();
        targetPosition = target.gameObject.transform.position;
    }
}
