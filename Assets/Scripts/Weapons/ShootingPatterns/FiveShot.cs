using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiveShot : IShootingPattern {
    const int angle = 45;
    public void Shoot(Vector2 direction, Transform shootPoint, float bulletSpeed, int damage, bool playerBullet) {
        Bullet[] bullets = new Bullet[5];
        for (int i = 0; i < 5; i++) {
            bullets[i] = ObjectPool.instance.GetNextObject().GetComponent<Bullet>();
        }
        //forwards
        bullets[0].Fire(shootPoint.position, direction.normalized, bulletSpeed, damage, playerBullet);

        //right by angle
        Vector2 angledRight = Quaternion.AngleAxis(angle, Vector3.forward) * direction;
        bullets[1].Fire(shootPoint.position, angledRight.normalized, bulletSpeed, damage, playerBullet);
        Vector2 angledHalfRight = Quaternion.AngleAxis(angle/2, Vector3.forward) * direction;
        bullets[2].Fire(shootPoint.position, angledHalfRight.normalized, bulletSpeed, damage, playerBullet);

        //left by angle
        Vector2 angledLeft = Quaternion.AngleAxis(-angle, Vector3.forward) * direction;
        bullets[3].Fire(shootPoint.position, angledLeft.normalized, bulletSpeed, damage, playerBullet);
        Vector2 angledHalfLeft = Quaternion.AngleAxis(-angle/2, Vector3.forward) * direction;
        bullets[4].Fire(shootPoint.position, angledHalfLeft.normalized, bulletSpeed, damage, playerBullet);
    }
}
