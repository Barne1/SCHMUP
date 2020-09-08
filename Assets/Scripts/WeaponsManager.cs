using System.Collections.Generic;
using UnityEngine;

public class WeaponsManager : MonoBehaviour {
    public static WeaponsManager instance;
    
    [SerializeField] List<Weapon> weapons;
    public Transform shootPoint;
    public ObjectPool bulletPool;
    private int counter = 0;
    
    private void Awake() {
        instance = this;
    }

    public Weapon GetNextWeapon() {
        if (weapons.Count > 0) {
            Weapon nextWeapon = weapons[counter % weapons.Count];
            counter++;
            if (counter > weapons.Count) {
                counter = 0;
            }

            return nextWeapon;
        }
        else {
            return null;
        }
    }

    public void AddWeapon(Weapon newWeapon) {
        if (!weapons.Contains(newWeapon)) {
            weapons.Add(newWeapon);
            newWeapon.SetUp();
            newWeapon.transform.parent = this.transform;
        }
    }
}
