using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public Rigidbody rb; 

    private void Awake() {
        rb = GetComponent <Rigidbody> ();
    }

    // Reset ball's position to the camera, zero out existing velocity & angular velocity.
    public void ResetBall () {
        transform.position = Camera.main.transform.position;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }

    private void OnCollisionEnter(Collision other) {

        // Ball has collided with a breakable block
        if (other.gameObject.CompareTag ("Block") && other.gameObject.GetComponent <Block> ().isBreakable == true) {
            BlockManager.instance.UpdateBlocks (other.gameObject, false);
            GameManager.instance.AddScore();
        }
    }
}
