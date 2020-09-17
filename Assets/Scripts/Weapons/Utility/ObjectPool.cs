using UnityEngine;

public class ObjectPool : MonoBehaviour {
    [SerializeField] private GameObject prefab;
    [SerializeField, Range(0, 1000)] private int poolSize = 100;
    private int counter = 0;
    private GameObject[] pool;

    public static ObjectPool instance;
    
    [SerializeField] public Sprite playerBulletSprite;
    [SerializeField] public Sprite enemyBulletSprite;

    [SerializeField] public Missile missile;

    protected virtual void Awake() {
        pool = new GameObject[poolSize];
        for (int i = 0; i < poolSize; i++) {
            GameObject bullet = Instantiate(prefab);
            pool[i] = bullet;
            bullet.SetActive(false);
        }

        instance = this;
    }

    public GameObject GetNextObject() {
        //if pool size 5, will get 0,1,2,3,4,0,1,2,3,4,0... etc
        GameObject nextObject = pool[counter % poolSize];
        counter++;
        if (counter > int.MaxValue - 1) {
            counter = 0;
        }
        nextObject.SetActive(true);
        return nextObject;
    }
}
