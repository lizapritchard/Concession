using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderChecker : MonoBehaviour
{
    public bool hasNextOrder = false;
    public List<CharacterInfo> nextOrder;
    public SpawnOptions spawnOptions;
    public Line line;

    public void setNextOrder(List<CharacterInfo> characterInfos_in) {
        hasNextOrder = true;
        nextOrder = characterInfos_in;
        spawnOptions.createDonut();
    }
    IEnumerator waitABit(Collision collision) {
        yield return new WaitForSeconds(2);
        checkOrder(collision.gameObject.GetComponent<Food>());
        Destroy(collision.gameObject);
    }

    // TODO when creating food make sure it has tag food
    void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag == "food" && hasNextOrder) {
            hasNextOrder = false;
            StartCoroutine(waitABit(collision));
        }
    }
    void OnCollisionStay(Collision collision) {
        Debug.Log("working");
        if (collision.gameObject.tag == "food" && hasNextOrder) {
            hasNextOrder = false;
            Debug.Log("when");
            StartCoroutine(waitABit(collision));
        }
    }
    // TODO check it for real
    void checkOrder(Food food) {
        foreach (var food_ordered in nextOrder)
        {
            foreach (var made_food in food.donutInfo)
            {
                if (food_ordered.foodAttr.Equals(made_food.attr)) {
                    if (!made_food.strColor.Equals(food_ordered.foodColor)) {
                        Debug.Log("FALSE");
                        Debug.Log(made_food.strColor);
                        Debug.Log(food_ordered.foodColor);
                        Debug.Log(food_ordered.foodAttr);
                    }
                }
            }
        }
        Debug.Log("TRUE");
        line.popAndMove();
    }
}