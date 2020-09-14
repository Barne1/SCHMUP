using UnityEngine;

public interface IShootingPattern {
    void Shoot(Vector2 direction, Transform shootPoint, float bulletSpeed, int damage, bool playerBullet);
}
