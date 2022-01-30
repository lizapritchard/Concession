using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {
    public Material pants;
    public Material shirt;
    public float speed = 1;
    public float radOffset = .5f;

    public float orderTellingAmount = 1;

    public float feedbackAmount = 1;
    public List<CharacterInfo> charInfo;

    public bool shouldTryMove = false;

    public Vector3 target;

    public float lookAheadOfMe = 2;
    public Vector3 truckLocation;

    public bool addedToLine = false;

    public Dictionary<string, Material> strClothesToMat = new Dictionary<string, Material>();

    void Start() {
        strClothesToMat.Add("pants", pants);
        strClothesToMat.Add("shirt", shirt);
    }

    void moveAndLook() {
        transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime * speed);
        transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(transform.position, target, Time.deltaTime * speed * radOffset,  Time.deltaTime * speed));
    }

    void Update() {
        // TODO add Line to line tag
        if (transform.parent.tag == "Line") {
            if (addedToLine) {
                moveAndLook();
                if (transform.position == target) {
                    addedToLine = false;
                    target = truckLocation;
                }
            } else if (shouldTryMove) {
                if (!Physics.Raycast(transform.position, transform.forward, lookAheadOfMe)) {
                    moveAndLook();
                } else {
                    shouldTryMove = false;
                }
            }
        } 
    }

    public void changeClothesColor(List<CharacterInfo> charInfoList) {
        foreach (var item in charInfoList)
        {
            strClothesToMat[item.cloth].color = RandomController.strColorToColor[item.clothColor];
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