using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFO : MonoBehaviour
{
    [SerializeField]
    private ObjectPool ufoBulletPool;
    private float lastShotTime;

    public int score = 200;
    public float fireRate = 3f;

    void Update()
    {
        if (Time.time - lastShotTime > fireRate)
        {
            GameObject bullet = ufoBulletPool.GetFromPool();
            bullet.transform.position = transform.position;
            bullet.transform.rotation = Quaternion.LookRotation(GlobalAccess.Instance.playerShip.transform.position - transform.position);
            bullet.GetComponent<Bullet>().LaunchBullet();
            lastShotTime = Time.time;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            GlobalAccess.Instance.ufoSpawner.DestroyUFO(gameObject, score);
        }
        else if (!other.CompareTag("EnemyBullet"))
        {
            GlobalAccess.Instance.ufoSpawner.DestroyUFO(gameObject, 0);
        }
    }
}
