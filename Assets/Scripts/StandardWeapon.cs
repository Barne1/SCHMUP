using System;
using UnityEngine;

public class StandardWeapon : Weapon {
    [SerializeField, Range(0,1000)] float bulletSpeed;
    [SerializeField, Range(0, 100)] int damage;
    private ObjectPool bulletPool;
    private Transform shootPoint;

    protected override void Shoot() {
        if (shootPoint == null) {
            shootPoint = WeaponsManager.instance.shootPoint;
        }

        if (bulletPool == null) {
            bulletPool = WeaponsManager.instance.bulletPool;
        }
        GameObject bullet = bulletPool.GetNextObject();
        bullet.GetComponent<Bullet>().Fire(shootPoint.position, Vector2.up, bulletSpeed , damage);
    }
}
