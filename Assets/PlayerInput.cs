using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public OrderChecker orderChecker;
    public Vector3 placeToDropOrder;
    public GameObject donutRef;
    public bool createdFood = false;
    void Update() {
        // TODO make sure donut colors match with RandomController possible colors
        // get key presses and create donuts if one hasn't been created
        // Current colors accepted: yellow, white, magenta
        if (orderChecker.hasNextOrder && !createdFood) {
            List<DonutInfo> donutInfos = new List<DonutInfo>();
            if (Input.GetKeyDown(KeyCode.Space))
            {
                donutInfos.Add(new DonutInfo("sprinkles", Color.yellow, "yellow"));
                donutInfos.Add(new DonutInfo("frosting", Color.white, "white"));
            } else if (Input.GetKeyDown(KeyCode.A)) {
                donutInfos.Add(new DonutInfo("sprinkles", Color.magenta, "magenta"));
                donutInfos.Add(new DonutInfo("frosting", Color.yellow, "yellow"));
            }
            createDonut(donutInfos);
            createdFood = true;
            StartCoroutine(waitABit(4));
        }
    }

    IEnumerator waitABit(float amount) {
        yield return new WaitForSeconds(amount);
        createdFood = false;
    }
    void createDonut(List<DonutInfo> donutInfo) {
        GameObject donut = Instantiate(donutRef, placeToDropOrder, Quaternion.identity);
        // set colors
        donut.GetComponent<Food>().setColors(donutInfo);
        donut.SetActive(true);
    }

}