using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Events;

public abstract class Enemy : MonoBehaviour {
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private const string animatorDeathTrigger = "Death";
    private const int playerLayer = 11;

    public static UnityEvent OnDeath;

    protected bool alive = true;

    #region COLOR

    private Color defaultColor;
    [SerializeField]private Color flashOnDamageColour = Color.red;

    #endregion

    [SerializeField, Range(0f, 1f)] private float damageFlashTime = 0.1f;
    [SerializeField, Range(1, 100)] private int collisionDamage;
    [SerializeField, Range(1, 100)]private int hp;

    protected bool shooting = false;
    [SerializeField, Range(0f, 1f)] protected float timeBetweenShots;
    protected Weapon weapon;
    
    private void Awake() {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponentInChildren<Animator>();
        weapon = GetComponentInChildren<Weapon>();

        defaultColor = spriteRenderer.color;
    }

    private void Update() {
        if (alive) {
            Attack();
            Movement();
        }
    }

    protected abstract void Movement();

    #region DEALING_DAMAGE

    protected abstract void Attack();

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.layer == playerLayer) {
            other.gameObject.GetComponent<PlayerController>().TakeDamage(collisionDamage);
        }
    }

    #endregion

    #region TAKING_DAMAGE
    
    public void TakeDamage(int damage) {
        if (alive) {
            hp -= damage;
            if (hp < 1) {
                alive = false;
                OnDeath?.Invoke();
                animator.SetTrigger(animatorDeathTrigger);
            }
            else {
                StartCoroutine(DamageFlash());
            }
        }
    }

    IEnumerator DamageFlash() {
        spriteRenderer.color = flashOnDamageColour;
        yield return new WaitForSeconds(damageFlashTime);
        spriteRenderer.color = defaultColor;
    }
    
    #endregion
}
