using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallLauncher : MonoBehaviour
{
    [SerializeField] private GameObject ballPrefab;
    private GameObject ballObject;
    public float launchForce = 100f;

    public void LaunchBall (float gauge) {
        float x = Input.mousePosition.x;
        float y = Input.mousePosition.y;
        // Debug.Log ("Cursor coordinate is: " + x + ", " + y);

        Ray ray = Camera.main.ScreenPointToRay (new Vector3 (x, y, 0));        
        
        // Instantiate a ball if there isn't currently any in scene.
        if (ballObject == null) {
            ballObject = Instantiate (ballPrefab, Camera.main.transform.position, Quaternion.identity);
        }
        ballObject.GetComponent <Ball> ().ResetBall ();

        ballObject.GetComponent <Rigidbody> ().AddForce (ray.direction * launchForce * gauge);        
    }
}
