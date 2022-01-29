using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testing : MonoBehaviour
{
    public GameObject bah;
    public float las = 1;
    // Choose one thing to omit
    // Said array is a list of strings that get appended. And get appended each order
    // 1. Do 5 orders
    // 2. append all attributes of orders to said array
    // 3. After 5 orders choose a random number from 0-saidarraylen and use that as index.
    // 4. Get that value and that attribute becomes the thing omited on the 6th time.
    // 5. keep appending what isn't omitted
    // Start is called before the first frame update



    // Randomness Controller
    // choose
    List<string> clothesList = new List<string>();

    void Start() {
        clothesList.Add("pants");
        clothesList.Add("shirt");
    }

    // void chooseClothesToColorMapping() {
    //     // Get array of clothes sprites / materials per player
    //     // Girl: pants, shirt
    //     // Guy: pants, shirt
    //     Random.RandomRange()
    // }

    void OnTriggerEnter(Collider collider) {
        Debug.Log("grr");
        bah.transform.position = new Vector3(bah.transform.position.x, las, bah.transform.position.z);
    }
}
