using UnityEngine;
using System.Collections.Generic;

public class EnemyCollection {
    private static List<Character> enemies = new List<Character>();

    public static void addEnemy(Character enemy) {
        enemies.Add(enemy);
    }

    public static Vector3? getEnemyPosition(int index) {
        if(index > -1 && index < enemies.Count)
        {
            return enemies.ToArray()[0].getCharacterPosition();
        }
        else
        {
            Debug.Log("Invalid enemy index " + index + " for enemies list size " + enemies.Count);
            return null;
        }
    }

}
