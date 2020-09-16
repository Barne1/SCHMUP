using System.Collections;
using UnityEngine;

public class BigEnemy : Enemy {
    [SerializeField, Range(0f, 1f)] float timeBetweenEachSalvo;
    [SerializeField, Range(1, 30)] int salvoesInAttack;

    [SerializeField, Range(0f, 10)] float amountToGoDown = 2f;

    [SerializeField, Range(0f, 10)] float timeToReachPositionFromSpawn;
    Vector3 velocity = Vector3.zero;
    Vector3 desiredPosition;

    protected override void Awake()
    {
        base.Awake();
        desiredPosition = transform.position - new Vector3(0, amountToGoDown, 0);
    }

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
        Vector3 currentPosition = transform.position;
        if((currentPosition-desiredPosition).sqrMagnitude > 0.01)
        {
            transform.position = Vector3.SmoothDamp(currentPosition, desiredPosition, ref velocity, timeToReachPositionFromSpawn);
        }
    }
}
