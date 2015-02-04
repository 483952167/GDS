using UnityEngine;
using System.Collections.Generic;

public class CharacterCollection {
    private List<Character> heroes = new List<Character>();

    public void addHero(Character hero) {
        heroes.Add(hero);
    }

    public Character getHero(int index) {
        if(-1 < index && index < heroes.Count)
        {
            return heroes.ToArray()[index];
        }
        else
        {
            Debug.Log("Invalid hero index " + index + " for size " + heroes.Count);
            return null;
        }
    }
}
