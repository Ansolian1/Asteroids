using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour, IPooledObject
{
    public enum Size { Big, Average, Small };
    public int score;
    public Size asteroidSize;
    private ObjectPool asteroidsPool;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet") || other.CompareTag("EnemyBullet"))
        {
            if (other.CompareTag("Bullet"))
            {
                OnBulletCollision(score);
            }
            else
            {
                OnBulletCollision(0);
            }
            other.GetComponent<Bullet>().DestroyBullet();
        }
        else if (!other.CompareTag("Asteroid"))
        {
            GlobalAccess.Instance.asteroidsSpawner.DestroyAsteroid(this);
            AddToPoolAsteroid();
        }
    }

    private void OnBulletCollision(int score)
    {
        GlobalAccess.Instance.asteroidsSpawner.DevideAsteroid(this, score);
    }

    public void SetObjectPool(ObjectPool objectPool)
    {
        asteroidsPool = objectPool;
    }

    public void AddToPoolAsteroid()
    {
        asteroidsPool.AddToPool(gameObject);
    }
}
