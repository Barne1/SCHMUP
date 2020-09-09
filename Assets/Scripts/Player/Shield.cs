using UnityEngine;
using System.Collections;

public class Shield : MonoBehaviour
{
    [SerializeField] GameObject shield;
    [SerializeField, Range(0f, 10f)] float shieldDuration;
    [SerializeField, Range(0f, 10f)] float shieldCooldown;
    public bool active { get; private set; } = false;


    public void ActivateShield()
    {
        if(!active)
        {
            active = true;
            shield.SetActive(true);
            StartCoroutine(ShieldTimer());
        }
    }

    public void DisableShield()
    {
        shield.SetActive(false);
        active = false;
    }

    IEnumerator ShieldTimer()
    {
        yield return new WaitForSeconds(shieldDuration);
        DisableShield();
        yield return new WaitForSeconds(shieldCooldown);
        shield.SetActive(false);
    }
}
