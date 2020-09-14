using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Bullet : MonoBehaviour {
    
    public enum BulletLayer {
        Playerbullet = 9,
        Enemybullet = 10
    }
    
    [SerializeField] private BulletLayer layer;
    [SerializeField]private int damage = 1;

    private Rigidbody2D body;
    private SpriteRenderer spriteRenderer;

    private void Awake() {
        body = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Fire(Vector2 position, Vector2 direction, float speed, int newDamage, bool playerBullet) {
        spriteRenderer.sprite =
            playerBullet ? ObjectPool.instance.playerBulletSprite : ObjectPool.instance.enemyBulletSprite;
        
        layer = playerBullet ? BulletLayer.Playerbullet : BulletLayer.Enemybullet;
        gameObject.layer = (int)layer;
        
        damage = newDamage;
        
        body.velocity = Vector2.zero;
        transform.position = position;
        body.AddForce(direction * speed);
    }

    private void OnCollisionEnter2D(Collision2D other) {
        
        if(layer == BulletLayer.Enemybullet)
        {
            PlayerController player = other.gameObject.GetComponent<PlayerController>();
            if(player != null)
            {
                player.TakeDamage(damage);
            }
        }
        else {
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            enemy?.TakeDamage(damage);
        }

        this.gameObject.SetActive(false);
    }
}
