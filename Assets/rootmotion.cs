using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rootmotion : MonoBehaviour
{
    // Start is called before the first frame update
    Animator anim;
    Rigidbody rb;
    public float speed = 1;
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (anim.IsInTransition(0)) {
            rb.velocity = Vector3.zero;
        }
        else if (anim.GetCurrentAnimatorClipInfo(0)[0].clip.name != "Breathing Idle") {
            rb.velocity = Vector3.forward * speed;
            anim.applyRootMotion = false;
        } else {
            rb.velocity = Vector3.zero;
            anim.applyRootMotion = true;
        }
    }
}
