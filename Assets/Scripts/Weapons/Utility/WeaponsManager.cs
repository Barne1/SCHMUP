using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WeaponsManager : MonoBehaviour {
    public static WeaponsManager instance;

    [SerializeField] PlayerController player;
    [SerializeField] List<Weapon> weapons;
    [SerializeField] private WeaponNameDisplay weaponText;
    
    public Transform shootPoint;
    
    private int counter = 0;
    private IShootingPattern[] shootingPatterns;
    public enum ShootingPattern {
        SINGLESHOT,
        TRISHOT,
        FIVESHOT,
        EXPLOSION,
        MAX,
    }
    
    private void Awake() {
        instance = this;
        shootingPatterns = new IShootingPattern[(int)ShootingPattern.MAX];
        SetUpShootingPatterns();
    }

    public void SetUpShootingPatterns() {
        shootingPatterns[(int)ShootingPattern.SINGLESHOT] = new SingleShot();
        shootingPatterns[(int)ShootingPattern.TRISHOT] = new TriShot();
        shootingPatterns[(int)ShootingPattern.FIVESHOT] = new FiveShot();
        shootingPatterns[(int)ShootingPattern.EXPLOSION] = new Explosion();
    }

    public IShootingPattern GetShootingPattern(ShootingPattern pattern) {
        return shootingPatterns[(int)pattern];
    }

    public Weapon SwapWeapon(int nextOrPrevious) {
        if (weapons.Count > 0) {
            counter += nextOrPrevious;
            if (counter >= weapons.Count) {
                counter -= weapons.Count;
            }
            else if (counter < 0) {
                counter += weapons.Count;
            }
            Weapon nextWeapon = weapons[counter % weapons.Count];
            weaponText.SetText(nextWeapon.name);
            return nextWeapon;
        }
        else {
            return null;
        }
    }

    public void AddWeapon(Weapon newWeapon) {
        Type type = newWeapon.GetType();
        if (weapons.Count(weapon => weapon.GetType() == type) < 1) {
            weapons.Add(newWeapon);
            newWeapon.transform.parent = this.transform;
            newWeapon.transform.localPosition = Vector3.zero;

            counter = weapons.Count - 1;
            Weapon nextWeapon = weapons[counter];
            player.currentWeapon = nextWeapon;
            weaponText.SetText(nextWeapon.name);
        }
    }
}
