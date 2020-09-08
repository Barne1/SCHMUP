using System;
using System.Security.Cryptography;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class WeaponPickup : MonoBehaviour {
    [SerializeField] private Weapon weapon;

    private void OnCollisionEnter2D (Collision2D other) {
            WeaponsManager.instance.AddWeapon(weapon);
        
        Destroy(this.gameObject, 1f);
    }
}
