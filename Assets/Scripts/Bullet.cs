using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Bullet : MonoBehaviour {
    
    public enum BulletLayer {
        Playerbullet = 9,
        Enemybullet = 10
    }
    
    [SerializeField] private BulletLayer layer;
    private int damage;

    private Rigidbody2D body;

    private void Awake() {
        body = GetComponent<Rigidbody2D>();
        this.gameObject.layer = (int)layer;
    }

    public void Fire(Vector2 position, Vector2 direction, float speed, int newDamage) {
        body.velocity = Vector2.zero;
        damage = newDamage;
        transform.position = position;
        body.AddForce(direction * speed);
    }

    private void OnCollisionEnter2D(Collision2D other) {
        //todo damage
        Debug.Log("lmao");
        this.gameObject.SetActive(false);
    }
}
