using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrderChecker : MonoBehaviour
{
    public AudioSource correct;
    public AudioSource incorrect;
    public bool hasNextOrder = false;
    public List<CharacterInfo> nextOrder;
    public SpawnOptions spawnOptions;
    public Line line;
    public Text earningsDisplay;

    private float earnings = 0;

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
        float baseOrderValue = 3.99f;
        float orderValue = baseOrderValue;
        float penalty = 1.00f;
        foreach (var food_ordered in nextOrder)
        {
            foreach (var made_food in food.donutInfo)
            {
                if (food_ordered.foodAttr.Equals(made_food.attr)) {
                    if (!made_food.strColor.Equals(food_ordered.foodColor)) {
                        Debug.Log("incorrect");
                        Debug.Log(made_food.strColor);
                        Debug.Log(food_ordered.foodColor);
                        Debug.Log(food_ordered.foodAttr);
                        orderValue -= penalty;
                    }
                }
            }
        }
        if (orderValue < baseOrderValue)
        {
            incorrect.Play();
        }
        else
        {
            correct.Play();
            Debug.Log("correct");
        }
        updateEarnings(orderValue);
        line.popAndMove();
    }

    void updateEarnings (float orderValue)
    {
        earnings += orderValue;
        earningsDisplay.text = "Earnings: $" + earnings.ToString();
    }
}