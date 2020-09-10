using UnityEngine;

public interface IShootingPattern {
    ObjectPool bulletPool { get; set; } 
    Transform shootPoint { get; set; }
    void Shoot(Vector2 direction, float bulletSpeed, int damage);

    void Init(ObjectPool bulletPool, Transform shootPoint);
}
