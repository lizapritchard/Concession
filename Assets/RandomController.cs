using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class RandomController : MonoBehaviour
{
    // Randomness Controller
    // choose
    public static List<string> clothesList = new List<string>();
    public static List<string> donutAttributes = new List<string>();
    public static Dictionary<string, string> foodToClothes = new Dictionary<string, string>();

    // These must be the same length 
    public static List<string> possibleClothesColors = new List<string>();
    public static List<string> possibleFoodColors = new List<string>();

    static public Dictionary<string, string> foodColorToShirtColor = new Dictionary<string, string>();
    static public Dictionary<string, string> foodColorToPantsColor = new Dictionary<string, string>();
    public static Dictionary<string, Dictionary<string, string>> clothToColorsToFoodColors = new Dictionary<string, Dictionary<string, string>>();
    static public Dictionary<string, Color> strColorToColor = new Dictionary<string, Color>();

    void Start() {
        clothesList.Add("pants");
        clothesList.Add("shirt");

        donutAttributes.Add("frosting");
        donutAttributes.Add("sprinkles");

        possibleClothesColors.Add("green");
        possibleClothesColors.Add("blue");
        possibleClothesColors.Add("red");

        possibleFoodColors.Add("yellow");
        possibleFoodColors.Add("white");
        possibleFoodColors.Add("magenta");

        strColorToColor.Add("green", Color.green);
        strColorToColor.Add("blue", Color.blue);
        strColorToColor.Add("red", Color.red);
        strColorToColor.Add("magenta", Color.magenta);
        strColorToColor.Add("yellow", Color.yellow);
        strColorToColor.Add("white", Color.white);


        // Create mappings of clothes to food and ccolor to fcolor
        foodToClothes = GenericRandomMapping(donutAttributes, clothesList);
        foodColorToShirtColor = GenericRandomMapping(possibleClothesColors, possibleFoodColors);
        foodColorToPantsColor = GenericRandomMapping(possibleClothesColors, possibleFoodColors);
        
        
        clothToColorsToFoodColors.Add("shirt", foodColorToShirtColor);
        clothToColorsToFoodColors.Add("pants", foodColorToPantsColor);

        // start spawning
        CharacterCreator.startSpawning = true;
    }

    static Dictionary<string, string> GenericRandomMapping(List<string> firstAttributeList, List<string> secondAttributeList) {
        // Get array of clothes sprites / materials per player
        // Girl: pants, shirt
        // Guy: pants, shirt
        if (firstAttributeList.Count != secondAttributeList.Count) {
            Debug.LogWarning("NOT GUNNA WORK");
            return null;
        }

        Dictionary<string, string> mapping = new Dictionary<string, string>();
        var numberList = Enumerable.Range(0, firstAttributeList.Count).ToList();
        randomHelper.Shuffle<int>(numberList);
        for (int i = 0; i < firstAttributeList.Count; i++)
        {
            mapping.Add(firstAttributeList[i], secondAttributeList[numberList[i]]);
        }
        return mapping;
    }
}