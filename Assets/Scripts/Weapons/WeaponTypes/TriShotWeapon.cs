using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriShotWeapon : Weapon
{
    [SerializeField, Range(0,1000)] float bulletSpeed;
    [SerializeField, Range(0, 100)] int damage;
    private Vector2 direction;
    [SerializeField] private bool enemyWeapon = false;

    private void Start() {
        if (!enemyWeapon) {
            shootingPattern = WeaponsManager.instance.GetShootingPattern(WeaponsManager.ShootingPattern.TRISHOT);
        }
        direction = enemyWeapon ? Vector2.down : Vector2.up;
    }
    
    protected override void Shoot() {
        shootingPattern.Shoot(direction, bulletSpeed, damage);
    }
}
