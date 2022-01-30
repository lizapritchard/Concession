using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMouseLook : MonoBehaviour
{
    public float mouseSens = 100f;
    public Transform playerBody;

    public float xRot = 0f;

    void Start() {
        Cursor.lockState = CursorLockMode.Locked;
    }
    void Update() {
        float mouseY = Input.GetAxis("Mouse Y") * mouseSens * Time.deltaTime;
        float mouseX = Input.GetAxis("Mouse X") * mouseSens * Time.deltaTime;

        xRot -= mouseY;
        xRot = Mathf.Clamp(xRot, -90f, 90f);
        transform.localRotation = Quaternion.Euler(xRot, 0, 0);
        playerBody.Rotate(Vector3.up * mouseX);
    }
}