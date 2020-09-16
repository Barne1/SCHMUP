using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupSpawner : MonoBehaviour
{
    [SerializeField] Transform spawnPoint;

    [SerializeField] private PowerUp[] powerUps;
    /*[SerializeField] GameObject[] powerUps;
    [SerializeField] int[] weightPerItem;*/
    [SerializeField, Range(0f, 100f)] float chanceOfPowerUp = 50;
    
    [System.Serializable]
    public struct PowerUp {
        public GameObject prefab;
        public int weight;
    }


    private void Start()
    {
        EnemySpawner.WaveOver.AddListener(CheckForPowerUpSpawn);

    }

    public void CheckForPowerUpSpawn()
    {
        if(Random.Range(0f, 100f) < chanceOfPowerUp)
        {
            int powerUpSelected = SelectPowerUp();
            Instantiate(powerUps[powerUpSelected].prefab, spawnPoint.position, Quaternion.identity);
        }
    }

    public int SelectPowerUp()
    {
        int totalWeight = 0;
        for (int i = 0; i < powerUps.Length; i++)
        {
            totalWeight += powerUps[i].weight;
        }

        int numberForSelection = Random.Range(0, totalWeight+1);
        int previousWeight = 0;
        for (int i = 0; i < powerUps.Length; i++)
        {
            if(numberForSelection <= powerUps[i].weight + previousWeight)
            {
                return i;
            }
            else
            {
                previousWeight += powerUps[i].weight;
            }
        }

        //this should never happen
        return -1;
    }
}
