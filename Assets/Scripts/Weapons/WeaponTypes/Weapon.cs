using System.Collections;
using UnityEngine;

public abstract class Weapon : MonoBehaviour {
    [SerializeField, Range(0f, 5f)] private float timeBetweenShots = 1f;
    private bool currentlyShooting = false;
    
    [SerializeField] public string name = "NO NAME";
    protected IShootingPattern shootingPattern;
    [SerializeField] protected bool playerWeapon = true;

    protected Vector2 direction;

    private void Awake() {
        direction = playerWeapon ? Vector2.up : Vector2.down;
        timeBetweenShots = playerWeapon ? timeBetweenShots : 0;
    }

    public void Fire() {
        if (!currentlyShooting) {
            Shoot();
            StartCoroutine(ShotCoolDown());
        }
    }

    protected abstract void Shoot();

    protected virtual IEnumerator ShotCoolDown() {
        currentlyShooting = true;
        yield return new WaitForSeconds(timeBetweenShots);
        currentlyShooting = false;
    }
}
