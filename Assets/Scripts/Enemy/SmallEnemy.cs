using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallEnemy : Enemy
{
    bool firing = false;

    [SerializeField, Range(0f,100f)] float movementSpeed;
    [SerializeField, Range(0f, 10f)] float swayAmount;
    [SerializeField, Range(0f, -100f)] float pointOfDestruction = -6f;

    float swayDirection = 1f;

    //Boundries of the playing field, with a little buffer due to sprite size
    const float maxX = 3f;

    public void SetSwayDirection(bool goLeft)
    {
        swayDirection = goLeft ? -1f : 1f;
    }

    protected override void Attack()
    {
        if(!firing)
        {
            firing = true;
            StartCoroutine(Shoot());
        }
    }

    IEnumerator Shoot()
    {
        weapon.Fire();
        yield return new WaitForSeconds(timeBetweenShots);
        firing = false;
    }

    protected override void Movement()
    {
        //point of destruction should always be a negative value since ship moves downwards
        if(transform.position.y < pointOfDestruction)
        {
            enemyScoreValue = 0;
            OnDeath.Invoke(this);
            Destroy(this.gameObject);
        }

        float yMovement = transform.position.y - movementSpeed * Time.deltaTime;
        float xMovement = Mathf.Sin(transform.position.y * swayAmount) * maxX * swayDirection;
        transform.position = new Vector3(xMovement, yMovement, transform.position.z);
    }
}
