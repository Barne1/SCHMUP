using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigEnemy : Enemy {
    [SerializeField, Range(0f, 1f)] float timeBetweenEachSalvo;
    [SerializeField, Range(1, 30)] int salvoesInAttack;
    protected override void Attack() {
        if (!shooting) {
            StartCoroutine(FireSalvo());
        }
    }

    IEnumerator FireSalvo() {
        shooting = true;
        for (int i = 0; i < salvoesInAttack; i++) {
            weapon.Fire();
            yield return new WaitForSeconds(timeBetweenEachSalvo);
        }
        yield return new WaitForSeconds(timeBetweenShots);
        shooting = false;
    }

    protected override void Movement() {
        //todo
    }
}
