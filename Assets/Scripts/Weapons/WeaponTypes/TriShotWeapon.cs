using UnityEngine;

public class TriShotWeapon : Weapon
{
    [SerializeField, Range(0,1000)] float bulletSpeed;
    [SerializeField, Range(0, 100)] int damage;

    private void Start() {
        shootingPattern = WeaponsManager.instance.GetShootingPattern(WeaponsManager.ShootingPattern.TRISHOT);
    }
    
    protected override void Shoot() {
        shootingPattern.Shoot(direction, transform, bulletSpeed, damage, playerWeapon);
    }
}
