using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {
    public Renderer pants;
    public Renderer shirt;
    public float speed = 1;
    public float radOffset = .5f;

    public float orderTellingAmount = 1;

    public float feedbackAmount = 1;
    public List<CharacterInfo> charInfo;

    public bool shouldTryMove = false;

    public Vector3 target;

    public float lookAheadOfMe = 2;

    public Animator anim;
    public Vector3 truckLocation;

    public bool addedToLine = false;

    public Dictionary<string, Renderer> strClothesToMat = new Dictionary<string, Renderer>();

    void moveAndLook() {
        // Debug.Log(transform.position);
        transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime * speed);
        Vector3 dir = target - transform.position;
        Quaternion rot = Quaternion.LookRotation(dir, Vector3.up);
        transform.rotation = Quaternion.Lerp(transform.rotation, rot, Time.deltaTime * speed);
    }

    void Update() {
        if (transform.parent != null && transform.parent.tag == "Line") {
            Vector3 upABit = transform.position + new Vector3(0, 1, 0);
            if (addedToLine) {
                moveAndLook();
                anim.SetBool("walking", true);
                Debug.DrawRay(upABit, transform.forward, Color.green, 2);
                if (Physics.Raycast(upABit, transform.forward, lookAheadOfMe)) {
                    addedToLine = false;
                    target = truckLocation;
                }
                else if (transform.position == target) {
                    addedToLine = false;
                    shouldTryMove = true;
                    target = truckLocation;
                }
            } else if (shouldTryMove) {
                 anim.SetBool("walking", true);
                if (!Physics.Raycast(upABit, transform.forward, lookAheadOfMe)) {
                    moveAndLook();
                } else {
                    shouldTryMove = false;
                }
            } else {
                anim.SetBool("walking", false);
            }
        } 
    }

    public void setTarget(Vector3 place) {
        target = place;
    }
    public void changeClothesColor(List<CharacterInfo> charInfoList) {
        strClothesToMat.Add("pants", pants);
        strClothesToMat.Add("shirt", shirt);
        foreach (var item in charInfoList)
        {
            strClothesToMat[item.cloth].material = new Material(Shader.Find("Universal Render Pipeline/Lit"));
            strClothesToMat[item.cloth].material.color = RandomController.strColorToColor[item.clothColor];
        }
        charInfo = charInfoList;
    }
    // TODO say order
    public void sayOrder() {
        Debug.Log("I want a donut with ");
        foreach (var item in charInfo)
        {
            Debug.Log(item.foodColor + " " + item.foodAttr + " and ...");
        }
    }
    // TODO tell animator moving so it can switch animations
    // TODO Add box colliders for each player
    public void setShouldTryMove(bool input) {
        shouldTryMove = input;
    }
}




// Mapping creation
// Player spawning
// Player movement once in line
// Players place order
// 