using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterController : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform parent;
    [SerializeField] float speed;
    [SerializeField] float delay = 0.5f;
    [SerializeField] int maxBullets;

    BulletController[] bullets;
    GameObject[] bulletPool;
    float lifeTime;

    // Start is called before the first frame update
    void Start()
    {
        lifeTime = delay * maxBullets;
        bulletPool = new GameObject[maxBullets];
        bullets = new BulletController[maxBullets];

        for (int i = 0; i < maxBullets; i++)
        {
            Vector3 position = this.transform.position;
            GameObject newBulletGO = Instantiate<GameObject>(bulletPrefab, parent) as GameObject;
            bulletPool[i] = newBulletGO;
            bullets[i] = newBulletGO.GetComponent<BulletController>();
            bullets[i].lifeTime = this.lifeTime;
            bullets[i].speed = this.speed;
            bullets[i].delay = this.delay * (i+1);
        }
    }
}
