using System;
using UnityEngine;
using UnityEngine.Events;

public class WeaponPickup : MonoBehaviour {
    [SerializeField] private Weapon weapon;
    [SerializeField, Range(0f, 10f)] float fallingSpeed;

    private void OnCollisionEnter2D (Collision2D other) {
        WeaponsManager.instance.AddWeapon(weapon);
        
        Destroy(this.gameObject);
    }

    private void Update()
    {
        //MoveDown
        transform.position += Vector3.down * fallingSpeed * Time.deltaTime;
    }
}
