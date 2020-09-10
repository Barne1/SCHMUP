using System.Collections;
using UnityEngine;

public abstract class Weapon : MonoBehaviour {
    [SerializeField, Range(0f, 5f)] private float timeBetweenShots = 1f;
    private bool currentlyShooting = false;
    [SerializeField] public string name = "NO NAME";
    protected IShootingPattern shootingPattern;

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
