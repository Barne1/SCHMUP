using UnityEngine;

public class SingleShot : IShootingPattern
{
    public ObjectPool bulletPool { get; set; }
    public Transform shootPoint { get; set; }

    public void Shoot(Vector2 direction, float bulletSpeed, int damage) {
        GameObject bullet = bulletPool.GetNextObject();
        bullet.GetComponent<Bullet>().Fire(shootPoint.position, direction, bulletSpeed, damage);
    }

    public void Init(ObjectPool bulletPool, Transform shootPoint) {
        this.bulletPool = bulletPool;
        this.shootPoint = shootPoint;
    }
}
