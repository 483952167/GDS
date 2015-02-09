using UnityEngine;
using System.Collections.Generic;

public class EnemyCollection {
    private List<Character> enemies = new List<Character>();

    public void addEnemy(Character enemy) {
        enemies.Add(enemy);
    }

	public int NumberOfEnemies ()
	{
		return enemies.Count;
	}

    public Character getEnemy(int index) {
        if(index > -1 && index < enemies.Count)
        {
            return enemies.ToArray()[0];
        }
        else
        {
            Debug.Log("Invalid enemy index " + index + " for enemies list size " + enemies.Count);
            return null;
        }
    }

}
