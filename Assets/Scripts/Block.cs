using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public bool isBreakable = false;
    public Vector3 initPos;
    public Quaternion initRot;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent <Rigidbody> ();
    }

    // Store the block's starting position & rotation
    public void InitBlock () {
        initPos = transform.position;
        initRot = transform.rotation;
    }

    // Reset Block's position, rotation, velocity, and angular velocity
    public void ResetBlock () {
        transform.position = initPos;
        transform.rotation = initRot;

        if (rb) {
            rb.velocity = new Vector3 (0,0,0);
            rb.angularVelocity = new Vector3 (0,0,0);
        }
    }
}
