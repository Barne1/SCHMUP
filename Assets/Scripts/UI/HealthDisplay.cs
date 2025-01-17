﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour
{
    [SerializeField] GameObject heart;
    [SerializeField] PlayerController player;
    [SerializeField] Text lifeText;

    [SerializeField, Range(0, 14)] int maxHeartsOnScreen;

    GameObject[] hearts;
    int maxHP;
    int counter;
    bool useNumbers = false;

    private void Start()
    {
        maxHP = player.HP;
        player.OnDamage.AddListener(TakeDamage);
        player.OnHealthPickup.AddListener(Heal);
        useNumbers = maxHP > maxHeartsOnScreen;

        if (useNumbers)
        {
            lifeText.gameObject.SetActive(true);
            lifeText.text = maxHP.ToString();
            counter = maxHP;
            Instantiate(heart, this.transform);
        }
        else
        {
            hearts = new GameObject[maxHP];
            counter = hearts.Length - 1;
            for (int i = 0; i < maxHP; i++)
            {
                hearts[i] = Instantiate(heart, this.transform);
            }
        }
    }

    public void TakeDamage(int damage)
    {
        if (useNumbers)
        {
            counter -= damage;
            counter = Mathf.Max(counter, 0);
            lifeText.text = counter.ToString();
        }
        else
        {
            int originalCounter = counter;
            while (counter > originalCounter - damage)
            {
                if (counter < 0)
                {
                    return;
                }
                hearts[counter--].SetActive(false);
            }
        }
    }

    public void Heal(int healing) {
        if (useNumbers)
        {
            counter += healing;
            lifeText.text = counter.ToString();
        }
        else
        {
            Debug.Log(healing);
            int originalCounter = counter;
            while (counter < originalCounter + healing)
            {
                Debug.Log(counter + "" + originalCounter + healing);
                hearts[++counter].SetActive(true);
            }
        }
    }
}
