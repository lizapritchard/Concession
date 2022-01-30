using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
    public OrderChecker orderChecker;
    public Queue<Character> characters;

    public void add(Character character) {
        character.gameObject.transform.parent = transform;
        characters.Enqueue(character);
        // Implicitly means there can't be an order in orderCHecker
        if (characters.Count <= 1) {
            move();
        }
    }

    // TODO add pickup animation and wait for that before deleting character

    public void popAndMove() {
        Character character = characters.Dequeue();
        Destroy(character.gameObject);
        move();
    }

    // Call something like characters foreach tryMoving
    void move() {
        // todo
        foreach (var item in characters)
        {
            item.shouldTryMove = true;
        }
    }

    // TODO add collider for them to collide with
    // Called when a new character collides
    void sendNextCharacter() {
        if (characters.Count > 0) {
            orderChecker.setNextOrder(characters.Peek().charInfo);
            characters.Peek().sayOrder();
        }
    }
}


// Cases:

// start
// 1 character moves
// 