using UnityEngine;

public class Explosion : IShootingPattern {
    const int bulletAmount = 24;
    const float colliderBounds = 0.2f;
    const float fullCircle = Mathf.PI * 2;
    const float increment = fullCircle / bulletAmount;

    public void Shoot(Vector2 direction, Transform shootPoint, float bulletSpeed, int damage, bool playerBullet) {
        Bullet[] bullets = new Bullet[bulletAmount];
        for (int i = 0; i < bulletAmount; i++) {
            bullets[i] = ObjectPool.instance.GetNextObject().GetComponent<Bullet>();
            Vector2 fireDirection = Vector2.up.RotateWithAngle(i * increment, Vector3.forward, true);
            Vector2 spawnDistance = fireDirection.normalized * colliderBounds;
            bullets[i].Fire(shootPoint.position + (Vector3)spawnDistance, fireDirection, bulletSpeed, damage, playerBullet);
        }
    }
}
