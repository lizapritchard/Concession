 using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Button : MonoBehaviour
{
    public GameObject colorInd;
    public void changeColor(Color color) {
        colorInd.GetComponent<Renderer>().material = new Material(Shader.Find("Universal Render Pipeline/Lit"));
        colorInd.GetComponent<Renderer>().material.color = color;
    }
}