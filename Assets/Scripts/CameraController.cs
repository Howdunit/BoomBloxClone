using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject focusObject;
    public float rotateSpeed = 1.0f;

    public void RotateCamera () {
        if (focusObject) {
            float x = Input.GetAxis ("Mouse X");

            transform.RotateAround (focusObject.transform.position, Vector3.up, x * rotateSpeed * Time.deltaTime);
        }        
    }
}
