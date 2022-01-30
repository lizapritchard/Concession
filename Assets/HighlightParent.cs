using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HighlightParent : MonoBehaviour
{
    public bool clicked = false;
    public int indexOfClicked = -1;
    public string attr;

    public void oneClicked(int index, bool wasClicked) {
        if (wasClicked) {
            clicked = true;
            indexOfClicked = index;
        } else {
            reset();
        }
        transform.parent.GetComponent<SpawnOptions>().clicked(attr, indexOfClicked, wasClicked);
    }
    public bool getClicked() {
        return clicked;
    }

    public void setAttr(string attr_in) {
        attr = attr_in;
    }
    public void reset() {
        clicked = false;
        indexOfClicked = -1;
    }
}