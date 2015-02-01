using UnityEngine;
using System.Collections.Generic;

public class CharacterCollection {
    private static List<Character> heroes;

    public static void addHero(Character hero) {
        heroes.Add(hero);
    }

    public static Vector3? getHeroPosition(int index) {
        if(-1 < index && index < heroes.Count)
        {
            return heroes.ToArray()[index].getCharacterPosition();
        }
        else
        {
            Debug.Log("Invalid hero index " + index + " for size " + heroes.Count);
            return null;
        }
    }
}
