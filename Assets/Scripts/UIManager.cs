using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;
    public static UIManager instance {get {return _instance;}}

    [SerializeField] private Slider gaugeSlider;
    [SerializeField] private TextMeshProUGUI scoreText;

    private void Awake() {
        if (_instance == null) {
            _instance = this;
        }

        else {
            Destroy (this);
        }
    }

    public void UpdateScoreText (string text) {
        scoreText.text = text;
    }

    public void UpdateLaunchGaugeSlider (float gauge) {
        gaugeSlider.value = gauge;
        // Debug.Log ("Updating launch gauge slider value to: " + gauge);
    }

    public void ToggleGaugeSlider (bool isVisible) {

        gaugeSlider.gameObject.SetActive (isVisible);

        // Debug.Log ("Setting Gauge Slider visibility to: " + isVisible);
    }
}
