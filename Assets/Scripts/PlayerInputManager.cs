using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputManager : MonoBehaviour
{
    [SerializeField] private CameraController cameraController;
    [SerializeField] private BallLauncher ballLauncher;

    public enum CursorState {
        Pan,
        Unlocked,
        Locked
    }
    private CursorState currCursorState;
    public Texture2D cursorAimLockTexture;
    public Texture2D cursorAimUnlockTexture;
    public Texture2D cursorPanTexture;    
    private bool aimLocked = false;
    private bool isNavigating = false;
    private float currGauge = 0f;
    public float gaugeWindupSpeed;

    private void Awake() {
        if (ballLauncher == null) {
            ballLauncher = GetComponent <BallLauncher> ();
        }

        if (cameraController == null) {
            cameraController = GetComponent <CameraController> ();
        }

        SetCursorState (CursorState.Unlocked);
    }

    // Start is called before the first frame update
    void Update()
    {
        // Move mouse cursor
        if (Input.GetMouseButtonDown (0)) {
            ToggleAimLock();
        }
 
        // Update gauge meter while the space bar is pressed.
        if (Input.GetKey (KeyCode.Space) && aimLocked && !isNavigating) {
            // Update Launch Gauage
            UpdateLaunchGauge ();

            // Cancel launch windup when Q key is pressed.
            if (Input.GetKeyDown (KeyCode.Q)) {
                currGauge = 0;
                UIManager.instance.UpdateLaunchGaugeSlider (currGauge);
                ToggleAimLock ();
            }           
        }
 
        // Launch ball when space bar is unpressed.
        if (Input.GetKeyUp (KeyCode.Space) && aimLocked && !isNavigating) {

            ballLauncher.LaunchBall (currGauge);

            // Reset Gauge & Cursor UI.
            currGauge = 0;
            UIManager.instance.UpdateLaunchGaugeSlider (currGauge);
            ToggleAimLock();

            Debug.Log ("Launching ball!");            
        } 

        // Switch cursor icon to "Pan" when right mouse button is pressed.
        if (Input.GetMouseButtonDown (1)) {
            SetCursorState (CursorState.Pan);
            isNavigating = true;
        }

        // Orbit camera around the current focus object while right mouse button is pressed.
        if (Input.GetMouseButton (1)) {
            cameraController.RotateCamera();
        }  

        // Switch cursor icon to "Aim Unlock" when right mouse button is released.
        if (Input.GetMouseButtonUp (1)) {
            CursorState state = aimLocked ? CursorState.Locked : CursorState.Unlocked;
            isNavigating = false;

            SetCursorState (state);
        }

        // Reset blocks
        if (Input.GetKeyDown (KeyCode.R)) {
            GameManager.instance.OnGameStart();
        }

        // Quit Game
        if (Input.GetKeyDown (KeyCode.Escape)) {
            Application.Quit();
        }

    }

    // Update cursor icon
    public void SetCursorState (CursorState state) {

        currCursorState = state;

        switch (currCursorState) {
            case CursorState.Pan :
            Cursor.SetCursor (cursorPanTexture, new Vector2 (cursorPanTexture.width / 2, cursorPanTexture.height / 2), CursorMode.Auto);
            break;

            case CursorState.Unlocked :
            Cursor.SetCursor (cursorAimUnlockTexture, new Vector2 (cursorAimUnlockTexture.width / 2, cursorAimUnlockTexture.height / 2), CursorMode.Auto);
            break;   

            case CursorState.Locked :
            Cursor.SetCursor (cursorAimLockTexture, new Vector2 (cursorAimLockTexture.width / 2, cursorAimLockTexture.height / 2), CursorMode.Auto);
            break;                     
        }
    }

    public void ToggleAimLock () {
        aimLocked = !aimLocked;
        CursorState state = aimLocked ? CursorState.Locked : CursorState.Unlocked;

        SetCursorState (state);

        UIManager.instance.ToggleGaugeSlider (aimLocked);
    }

    public void UpdateLaunchGauge () {
        currGauge = Mathf.Clamp (currGauge + gaugeWindupSpeed * Time.deltaTime, 0, 1);
        // Debug.Log ("current gauge is: " + currGauge);
        UIManager.instance.UpdateLaunchGaugeSlider (currGauge);        
    }

}
