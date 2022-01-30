using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct DonutInfo {
    public string attr;
    public Color color;
    public string strColor;
    public DonutInfo(string a, Color c, string s) {
        attr = a;
        color = c;
        strColor = s;
    }
}

public class Food : MonoBehaviour 
{
    public Material sprinkles;
    public Material frosting;
    public List<DonutInfo> donutInfo;
    public Dictionary<string, Material> strFoodToMat = new Dictionary<string, Material>();

    public void setColors(List<DonutInfo> donutInfo_in) {
        strFoodToMat.Add("frosting", frosting);
        strFoodToMat.Add("sprinkles", sprinkles);
        donutInfo = donutInfo_in;
        foreach (var item in donutInfo)
        {
            strFoodToMat[item.attr].color = item.color;
        }
    }
}