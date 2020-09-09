using System;
using UnityEngine;

public class StandardWeapon : Weapon {
    [SerializeField, Range(0,1000)] float bulletSpeed;
    [SerializeField, Range(0, 100)] int damage;
    private ObjectPool bulletPool;
    private Transform shootPoint;
    private bool setup = false;

    public override void SetUp() {
        shootPoint = WeaponsManager.instance.shootPoint;
        bulletPool = WeaponsManager.instance.bulletPool;
    }
    
    protected override void Shoot() {
        if (!setup) {
            SetUp();
        }
        GameObject bullet = bulletPool.GetNextObject();
        bullet.GetComponent<Bullet>().Fire(shootPoint.position, Vector2.up, bulletSpeed , damage);
    }
}
