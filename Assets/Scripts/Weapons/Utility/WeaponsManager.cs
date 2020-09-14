using System.Collections.Generic;
using UnityEngine;

public class WeaponsManager : MonoBehaviour {
    public static WeaponsManager instance;

    [SerializeField] PlayerController player;
    [SerializeField] List<Weapon> weapons;
    [SerializeField] private WeaponNameDisplay weaponText;
    
    public Transform shootPoint;
    public ObjectPool bulletPool;
    
    private int counter = 0;
    private IShootingPattern[] shootingPatterns;
    public enum ShootingPattern {
        SINGLESHOT,
        TRISHOT,
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
        if (!weapons.Contains(newWeapon)) {
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
