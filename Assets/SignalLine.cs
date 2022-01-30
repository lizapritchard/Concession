using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignalLine : MonoBehaviour
{
    public Line line;

    void OnTriggerEnter(Collider collider) {
        if (collider.gameObject.tag == "character") {
            line.sendNextCharacter();
        }
    }
}
