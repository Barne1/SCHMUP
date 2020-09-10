using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponNameDisplay : MonoBehaviour {
    [SerializeField] private Text text;
    private float r;
    private float g;
    private float b;
    private float opacity = 0;
    private bool fading = false;

    [SerializeField, Range(0f,1f)]
    float fadingSpeed;
    private void Start() {
        text.text = "";
        var color = text.color;
        r = color.r;
        g = color.g;
        b = color.b;
    }

    private void Update() {
        if (fading) {
            opacity -= fadingSpeed * Time.deltaTime;
            if (opacity < 0.0001f) {
                opacity = 0;
                fading = false;
            }
            text.color = new Color(r, g, b, opacity);
        }
    }

    public void SetText(string newText) {
        text.text = newText;
        opacity = 1;
        fading = true;
    }
}
