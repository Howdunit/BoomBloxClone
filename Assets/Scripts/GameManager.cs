using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    private static GameManager _instance;
    public static GameManager instance {get {return _instance;}}
    public int maxScore;
    public int currScore;

    private void Awake() {
        if (_instance == null) {
            _instance = this;
        }

        else {
            Destroy (this);
        }
    }

    private void Start() {
        OnGameStart ();
    }

    public void OnGameStart () {
        ResetScore ();
        BlockManager.instance.ResetBlocks();
        UIManager.instance.ToggleGaugeSlider (false);
        Debug.Log ("Game started");
    }


    public void OnGameEnd () {
        Debug.Log ("You beat the game!");
    }

    public void AddScore () {
        currScore++;
        UIManager.instance.UpdateScoreText (currScore.ToString() + " / " + maxScore);

        if (currScore >= maxScore) {
            GameManager.instance.OnGameEnd ();

            UIManager.instance.UpdateScoreText ("You beat the game!");
        }

        // Debug.Log ("Updated score to: " + currScore);
    }

    public void ResetScore () {
        maxScore = BlockManager.instance.blockDict.Count;
        currScore = 0;

        UIManager.instance.UpdateScoreText (currScore.ToString() + " / " + maxScore);

        Debug.Log ("Resetting scoreboard...");
    }

}
