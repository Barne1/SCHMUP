using UnityEngine;

public class EnemyAnimationEvent : MonoBehaviour {
    [SerializeField] private Collider2D collider;
    
    public void DestroyPart() {
        collider.enabled = false;
    }

    public void DestroyObject() {
        //Event occurs after animation finishes playing
        Destroy(transform.parent.gameObject);
    }
}
