using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Highlightable : MonoBehaviour
{
    public bool highlighted = false;
    public bool clicked = false;
    public float fadeTime = .4f;
    public float elapsed = 0;

    public int index;
    public Color oldColor;
    public void highlight() {
        if (!clicked) {
          GetComponent<Renderer>().material.shader = Shader.Find ("Self-Illumin/Diffuse");
          highlighted = true;
          elapsed = 0;
        }
    }

    public void reset() {
        clicked = false;
        highlighted = false;
        elapsed = 0;
        GetComponent<Renderer>().material.color = oldColor;
    }

    public void click() {
        HighlightParent parent = transform.parent.parent.GetComponent<HighlightParent>();
        if (!clicked && !parent.getClicked()) {
            clicked = true;
            highlighted = false;
            GetComponent<Renderer>().material.shader = Shader.Find("Universal Render Pipeline/Lit");
            GetComponent<Renderer>().material.color = Color.green;
            parent.oneClicked(index, clicked);
        } else if (clicked) {
            clicked = false;
            GetComponent<Renderer>().material.color = oldColor;
            parent.oneClicked(index, clicked);
        }
    }

    public void Start() {
        oldColor = GetComponent<Renderer>().material.color;
        // index = 2;
        // TODO set index
    }

    public void setIndex(int index_in) {
        index = index_in;
    } 
 
    void Update()
    {
        if (highlighted) {
            elapsed += Time.deltaTime;
            if (elapsed > fadeTime) {
                highlighted = false;
                GetComponent<Renderer>().material.shader = Shader.Find("Universal Render Pipeline/Lit");
            }
        }
    }
}