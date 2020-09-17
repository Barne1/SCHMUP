using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileWeapon : Weapon
{
    Missile missile;
    GameObject missileObject;

    protected override void Shoot()
    {
        missile = ObjectPool.instance.missile;
        missileObject = missile.gameObject;

        Transform target = GetTarget();
        missileObject.SetActive(true);
        missileObject.transform.rotation = transform.rotation;
        missile.Fire(transform.position, target);
    }

    protected override IEnumerator ShotCoolDown()
    {
        currentlyShooting = true;
        yield return new WaitUntil(() => missileObject.activeSelf == false);
        currentlyShooting = false;
    }

    private Transform GetTarget()
    {
        List<Enemy> potentialTargets = EnemySpawner.enemiesAlive;

        Transform target = null;
        foreach (Enemy enemy in potentialTargets)
        {
            target = enemy.transform;
            if(enemy is SmallEnemy)
            {
                Debug.Log(target.gameObject.name);
                return target;
            }
        }

        return target;
    }
}
