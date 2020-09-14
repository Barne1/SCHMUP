using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriShot : IShootingPattern {
    const int angle = 15;
    public void Shoot(Vector2 direction, Transform shootPoint, float bulletSpeed, int damage, bool playerBullet) {
        Bullet[] bullets = new Bullet[3];
        for (int i = 0; i < 3; i++) {
            bullets[i] = ObjectPool.instance.GetNextObject().GetComponent<Bullet>();
        }
        //forwards
        bullets[0].Fire(shootPoint.position, direction.normalized, bulletSpeed, damage, playerBullet);
        //right by angle
        Vector2 angledRight = Quaternion.AngleAxis(angle, Vector3.forward) * direction;
        bullets[1].Fire(shootPoint.position, angledRight.normalized, bulletSpeed, damage, playerBullet);
        //left by angle
        Vector2 angledLeft = Quaternion.AngleAxis(-angle, Vector3.forward) * direction;
        bullets[2].Fire(shootPoint.position, angledLeft.normalized, bulletSpeed, damage, playerBullet);
    }
}
