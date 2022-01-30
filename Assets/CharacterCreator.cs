using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public static class randomHelper {
    public static System.Random rng = new System.Random();  
    public static void Shuffle<T>(this List<T> list)  
    {  
        int n = list.Count;  
        while (n > 1) {  
            n--;  
            int k = rng.Next(n + 1);  
            T value = list[k];  
            list[k] = list[n];  
            list[n] = value;  
        }
    }
}


public struct CharacterInfo {
    public string cloth;
    public string clothColor;

    public string foodAttr;
    public string foodColor;
    public bool shouldSayFoodColor;
    public CharacterInfo(string c_in, string cl_in, string f_in, string fo_in) {
        cloth = c_in;
        clothColor = cl_in;
        foodAttr = f_in;
        foodColor = fo_in;
        shouldSayFoodColor = false;
    }
}

public class CharacterCreator : MonoBehaviour {
    public int amountOfFullOrders = 5;

    public GameObject charRef;

    public Line line;

    public Vector3 spawnSpot;

    public static bool startSpawning = false;

    public float spawnRate = 5;
    public float timeElapsed = 0;

    public List<CharacterInfo> saidAlready = new List<CharacterInfo>();
    // Choose one thing to omit
    // Said array is a list of strings that get appended. And get appended each order
    // 1. Do 5 orders
    // 2. append all attributes of orders to said array
    // 3. After 5 orders choose a random number from 0-saidarraylen and use that as index.
    // 4. Get that value and that attribute becomes the thing omited on the 6th time.
    // 5. keep appending what isn't omitted
    // Start is called before the first frame update


        // Creates a material from shader and texture references.

    void Update() {
        if (startSpawning) {
            if (timeElapsed >= spawnRate) {
                timeElapsed = 0;
                makeCharacter();
            }
            timeElapsed += Time.deltaTime;
        }
    }

    void makeCharacter() {
        // if amount > 0
        // decrement
        // loop through food attributes and choose a random food color
        // send this map to the character
        // character will use this to change color of clothes so thats all the data we need
        // intialize human with 100% order telling and feedback
        string foodAttrPicked = "";
        List<string> attrs = RandomController.donutAttributes;
        Dictionary<string, string> clothToColor = new Dictionary<string, string>();
        List<CharacterInfo> changes = new List<CharacterInfo>();
        
        if (amountOfFullOrders <= 0) {
            // TODO maybe do this for both clothing items once many orders are success
            int copied_index = Random.Range(0, saidAlready.Count);
            CharacterInfo copied = saidAlready[copied_index];
            copied.shouldSayFoodColor = false;
            foodAttrPicked = copied.foodAttr;
            changes.Add(copied);
        } else {
            amountOfFullOrders -= 1;
        }
    
        for (int i = 0; i < attrs.Count; i++)
        {
            if (foodAttrPicked != attrs[i]) {
                int j = Random.Range(0, RandomController.possibleFoodColors.Count);
                // get random foodattr color
                string rngFoodColor = RandomController.possibleFoodColors[j];
                // get associated cloth
                string cloth = RandomController.foodToClothes[attrs[i]];
                // get cloth color associated with rngFoodColor
                // Debug.Log(cloth);
                // Debug.Log(rngFoodColor);
                // Debug.Log( RandomController.clothToColorsToFoodColors[cloth]["white"]);
                // Debug.Log( RandomController.clothToColorsToFoodColors[cloth]["magenta"]);
                // Debug.Log( RandomController.clothToColorsToFoodColors[cloth]["yellow"]);
                string clothColor = RandomController.clothToColorsToFoodColors[cloth][rngFoodColor];
                clothToColor.Add(cloth, clothColor);
                CharacterInfo temp1 = new CharacterInfo(cloth, clothColor, attrs[i], rngFoodColor);
                changes.Add(temp1);
                CharacterInfo temp2 = new CharacterInfo(cloth, clothColor, attrs[i], rngFoodColor);
                saidAlready.Add(temp2);
            }
        }
        GameObject character = Instantiate(charRef, spawnSpot, Quaternion.identity);
        Character charComponent = character.GetComponent<Character>();
        charComponent.changeClothesColor(changes);
        // intialize human with 100% order telling and feedback
        // add character to the line
        Debug.Log(charComponent);
        line.add(charComponent);
    }
}