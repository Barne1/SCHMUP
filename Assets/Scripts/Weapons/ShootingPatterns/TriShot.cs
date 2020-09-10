using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriShot : IShootingPattern {
    const int angle = 15;
    public ObjectPool bulletPool { get; set; }
    public Transform shootPoint { get; set; }
    public void Shoot(Vector2 direction, float bulletSpeed, int damage) {
        Bullet[] bullets = new Bullet[3];
        for (int i = 0; i < 3; i++) {
            bullets[i] = bulletPool.GetNextObject().GetComponent<Bullet>();
        }
        //forwards
        bullets[0].Fire(shootPoint.position, direction.normalized, bulletSpeed, damage);
        //right by angle
        Vector2 angledRight = Quaternion.AngleAxis(angle, Vector3.forward) * direction;
        bullets[1].Fire(shootPoint.position, angledRight.normalized, bulletSpeed, damage);
        //left by angle
        Vector2 angledLeft = Quaternion.AngleAxis(-angle, Vector3.forward) * direction;
        bullets[2].Fire(shootPoint.position, angledLeft.normalized, bulletSpeed, damage);
    }

    public void Init(ObjectPool bulletPool, Transform shootPoint) {
        this.bulletPool = bulletPool;
        this.shootPoint = shootPoint;
    }
}
