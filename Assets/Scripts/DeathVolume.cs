using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathVolume : MonoBehaviour
{

    private void OnCollisionEnter(Collision other) {

        // Hide blocks and add score.
        if (other.gameObject.CompareTag ("Block")) {
            BlockManager.instance.UpdateBlocks (other.gameObject, false);
            GameManager.instance.AddScore();
            // Debug.Log (other.gameObject.name + "'s state set to: " + false);
        }        
    }
}
