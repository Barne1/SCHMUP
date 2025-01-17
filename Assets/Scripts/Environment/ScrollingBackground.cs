﻿using System;
using UnityEngine;

namespace Environment {
    public class ScrollingBackground : MonoBehaviour {
        [SerializeField] 
        private Material backgroundMat;
        [SerializeField, Range(0f,5f)] 
        private float desiredScrollSpeed = 1f;
        private float scrollSpeed = 0.001f;
        [SerializeField, Range(0f, 5f)]
        private float scrollAcceleration = 0.1f;

        private float _offset = 0f;
        
        private void Awake() {
            if (backgroundMat == null) {
                backgroundMat = GetComponent<MeshRenderer>().material;
            }
        }

        void Update() {
            if(scrollSpeed < desiredScrollSpeed && GameHandler.instance.LevelStarted)
            {
                scrollAcceleration += scrollAcceleration * Time.deltaTime;
                scrollSpeed = Mathf.Min(scrollAcceleration, desiredScrollSpeed);
            }
            _offset += scrollSpeed * Time.deltaTime;
            //prevent overflow
            if (_offset > 1000) {
                _offset = 0;
            }
            backgroundMat.mainTextureOffset = new Vector2(0, _offset);
        }

        private void OnDisable() {
            backgroundMat.mainTextureOffset = Vector2.zero;
            backgroundMat.color = Color.gray;
        }
    }
}
