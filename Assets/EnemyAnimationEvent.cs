using UnityEngine;

public class EnemyAnimationEvent : MonoBehaviour {
    [SerializeField] private Collider2D collider;
    [SerializeField] private AudioClip clip;
    
    public void DestroyPart() {
        collider.enabled = false;
        AudioSource.PlayClipAtPoint(clip, transform.position, 0.8f);
    }

    public void DestroyObject() {
        //Event occurs after animation finishes playing
        Destroy(transform.parent.gameObject);
    }
}
