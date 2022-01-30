using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderChecker : MonoBehaviour
{
    public GameObject donut;

    public bool hasNextOrder = false;
    public List<CharacterInfo> nextOrder;
    public Line line;

    public void setNextOrder(List<CharacterInfo> characterInfos_in) {
        hasNextOrder = true;
        nextOrder = characterInfos_in;
    }

    // TODO when creating food make sure it has tag food
    void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag == "food") {
            checkOrder(collision.gameObject.GetComponent<Food>());
            Destroy(collision.gameObject);
        }
    }
    // TODO check it for real
    void checkOrder(Food food) {
        foreach (var food_ordered in nextOrder)
        {
            foreach (var made_food in food.donutInfo)
            {
                if (food_ordered.foodAttr == made_food.attr) {
                    if (made_food.strColor != food_ordered.foodColor) {
                        Debug.Log("FALSE");
                    }
                }
            }
        }
        Debug.Log("TRUE");
        line.popAndMove();
    }
}