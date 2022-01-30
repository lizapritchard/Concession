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
            if (Input.GetKeyDown(KeyCode.Space))
            {
                easyCreate("sprinkles", Color.yellow, "yellow", "frosting", Color.white, "white");
            } else if (Input.GetKeyDown(KeyCode.A)) {
                easyCreate("sprinkles", Color.magenta, "magenta", "frosting", Color.yellow, "yellow");
            }
        }
    }

    void easyCreate(string a1, Color c1, string s1, string a2, Color c2, string s2) {
        List<DonutInfo> donutInfos = new List<DonutInfo>();
        donutInfos.Add(new DonutInfo(a1, c1, s1));
        donutInfos.Add(new DonutInfo(a2, c2, s2));
        createDonut(donutInfos);
        createdFood = true;
        StartCoroutine(waitABit(4));
    }

    IEnumerator waitABit(float amount) {
        yield return new WaitForSeconds(amount);
        createdFood = false;
    }
    void createDonut(List<DonutInfo> donutInfo) {
        GameObject donut = Instantiate(donutRef, placeToDropOrder, Quaternion.identity);
        // set colors
        donut.SetActive(true);
        donut.GetComponent<Food>().setColors(donutInfo);
    }

}