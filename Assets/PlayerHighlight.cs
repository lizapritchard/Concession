 using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerHighlight : MonoBehaviour
{
    public LayerMask IgnoreMe;
    void Update()
    {
        RaycastHit hit;
        var ray = Camera.main.ScreenPointToRay (Input.mousePosition);
        if (Input.GetMouseButtonDown(0))
        {
            if ( Physics.Raycast( ray, out hit, 2,  ~IgnoreMe) )
            {
                if ( hit.collider.gameObject.tag == "highlightable" )
                {
                    hit.collider.GetComponent<Highlightable>().click();
                }
            }
        }
        else if ( Physics.Raycast( ray, out hit, 2,  ~IgnoreMe) )
        {
            if ( hit.collider.gameObject.tag == "highlightable" )
            {
                hit.collider.GetComponent<Highlightable>().highlight();
            }
        }
    }
}