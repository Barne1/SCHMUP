using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public abstract class Weapon : MonoBehaviour {
    [SerializeField, Range(0f, 5f)] private float timeBetweenShots = 1f;
    protected bool currentlyShooting = false;

    private AudioPlayer audioPlayer;
    
    [SerializeField] public string name = "NO NAME";
    protected IShootingPattern shootingPattern;
    [SerializeField] protected bool playerWeapon = true;

    protected Vector2 direction;

    protected virtual void Awake() {
        audioPlayer = GetComponent<AudioPlayer>();
        direction = playerWeapon ? Vector2.up : Vector2.down;
        timeBetweenShots = playerWeapon ? timeBetweenShots : 0;
    }

    public void Fire() {
        if (!currentlyShooting) {
            Shoot();
            audioPlayer.PlayClip();
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
