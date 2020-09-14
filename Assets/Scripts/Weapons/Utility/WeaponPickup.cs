﻿using System;
using UnityEngine;
using UnityEngine.Events;

public class WeaponPickup : MonoBehaviour {
    [SerializeField] private Weapon weapon;

    private void OnCollisionEnter2D (Collision2D other) {
        WeaponsManager.instance.AddWeapon(weapon);
        
        Destroy(this.gameObject);
    }
}
