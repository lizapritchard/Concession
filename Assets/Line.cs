using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Line : MonoBehaviour
{
    public OrderChecker orderChecker;
    public Queue<Character> characters = new Queue<Character>();
    public Vector3 endOfLine;
    public float orderDespawnTime = 5;
    public TMPro.TMP_Text text;
    public int maxLineLength = 10;

    public void add(Character character) {
        character.addedToLine = true;
        character.setTarget(endOfLine);
        character.gameObject.transform.parent = transform;
        characters.Enqueue(character);
        // Implicitly means there can't be an order in orderCHecker
        if (characters.Count <= 1) {
            move();
        }
    }

    private void Update()
    {
        if (characters.Count > maxLineLength)
        {
            GameOver();
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
    public void sendNextCharacter() {
        if (characters.Count > 0) {
            orderChecker.setNextOrder(characters.Peek().charInfo);
            StartCoroutine(OrderSaid(characters.Peek().charInfo));
        }
    }

    public IEnumerator OrderSaid(List<CharacterInfo> characterInfo) {
        string order = @"";
        order += "I want a donut with\n";
        text.text = order;
        yield return new WaitForSeconds(1);
        for (int i = 0; i < characterInfo.Count; i++)
        {
            var item = characterInfo[i];
            if (item.shouldSayFoodColor) {
                order += item.foodColor;
            }
            order += " " + item.foodAttr;
            if (i != characterInfo.Count - 1) {
                order += " and\n";
            }
            yield return new WaitForSeconds(.5f);
            text.text = order;
        }


        yield return new WaitForSeconds(orderDespawnTime);
        text.text = "";
    }

    public void GameOver()
    {
        Debug.Log("you lose :(");
        RandomController.Reset();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}


// Cases:

// start
// 1 character moves
// 