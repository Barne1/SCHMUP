using UnityEngine;

public class SingleShot : IShootingPattern
{
    public void Shoot(Vector2 direction, Transform shootPoint, float bulletSpeed, int damage, bool playerBullet) {
        GameObject bullet = ObjectPool.instance.GetNextObject();
        bullet.GetComponent<Bullet>().Fire(shootPoint.position, direction, bulletSpeed, damage, playerBullet);
    }
}
