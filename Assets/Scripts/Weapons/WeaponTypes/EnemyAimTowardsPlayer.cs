using System;
using UnityEngine;

public class EnemyAimTowardsPlayer : Weapon {
    [SerializeField, Range(0,1000)] float bulletSpeed;
    [SerializeField, Range(0, 100)] int damage;
    [SerializeField, Range(0f, 180f)] float maxAngleOfShot = 90f;
    Transform player;

    private void Start() {
        shootingPattern = WeaponsManager.instance.GetShootingPattern(WeaponsManager.ShootingPattern.SINGLESHOT);
        player = GameHandler.instance.player.transform;
    }
    
    protected override void Shoot() {
        Vector2 playerDirection = (player.position - transform.position);
        playerDirection.Normalize();
        if(Vector3.Angle(Vector3.down, playerDirection) < maxAngleOfShot)
        {
            direction = playerDirection;
        }
        else
        {
            direction = Vector3.down;
        }

        shootingPattern.Shoot(direction, transform, bulletSpeed, damage, playerWeapon);
    }
}
