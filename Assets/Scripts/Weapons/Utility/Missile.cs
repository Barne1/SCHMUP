using UnityEngine;

public class Missile : MonoBehaviour
{
    [SerializeField] private int bulletDamage = 1;
    [SerializeField] private float bulletSpeed = 300f;

    [SerializeField, Range(0f,1f)] float initialOrientationSlowDown;
    [SerializeField, Range(1f, 10f)] float initialOrientationRotationSpeedModifer;

    [SerializeField] float rotationSpeed;
    [SerializeField, Range(0f, 180f)] float orientationPrecisionAngles;
    [SerializeField] private float missileSpeed = 10f;
    [SerializeField] private int collisionDamage = 1;

    bool initialOrientationDone = false;
    float slowDown = 1f;

    private Rigidbody2D body;
    private SpriteRenderer spriteRenderer;
    IShootingPattern shootingPattern;
    Transform target = null;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        shootingPattern = WeaponsManager.instance.GetShootingPattern(WeaponsManager.ShootingPattern.EXPLOSION);
    }

    public void Fire(Vector2 position, Transform enemy)
    {
        initialOrientationDone = false;
        body.velocity = Vector2.zero;
        transform.position = position;
        target = enemy;
    }

    private void FixedUpdate()
    {
        if (target == null)
        {
            body.velocity = transform.up * missileSpeed;
        }
        else
        {
            Quaternion rotationTowardsTarget = transform.position.LookAtTargetTopDown(target.position);

                        if (initialOrientationDone)
                        {
                            slowDown = 1f; //no slowdown
                            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotationTowardsTarget, rotationSpeed);
                        }
                        else //Orient missile
                        {
                            slowDown = initialOrientationSlowDown;
                            transform.rotation = Quaternion.RotateTowards(
                                transform.rotation,
                                rotationTowardsTarget,
                                rotationSpeed * initialOrientationRotationSpeedModifer
                                );

                            //angle between missile up and target direction
                            float angle = Vector2.Angle(transform.up, (target.position - transform.position).normalized);
                            if(angle < orientationPrecisionAngles)
                            {
                                initialOrientationDone = true;
                            }
                        }
                        
            body.velocity = transform.up * missileSpeed * slowDown;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Enemy enemy = other.gameObject.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.TakeDamage(collisionDamage);
            if(enemy is SmallEnemy)
            {
                enemy.GetComponent<Collider2D>().enabled = false;
            }
        }
        shootingPattern.Shoot(Vector2.zero, this.transform, bulletSpeed, bulletDamage, true);
        
        this.gameObject.SetActive(false);
    }
}
