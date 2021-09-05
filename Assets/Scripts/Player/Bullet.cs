using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour, IPooledObject
{
    public float velocity;
    private Rigidbody rb;
    private ObjectPool bulletPool;

    private float bulletLifeTime;
    private float bulletStartTime;

    public void LaunchBullet()
    {
        rb.AddForce(transform.localRotation * Vector3.forward * velocity, ForceMode.VelocityChange);
        bulletStartTime = Time.time;
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        bulletLifeTime = GlobalAccess.Instance.xBorder * 2 / velocity;
    }

    private void Update()
    {
        if (Time.time - bulletStartTime > bulletLifeTime)
        {
            DestroyBullet();
        }
    }

    public void DestroyBullet()
    {
        bulletPool.AddToPool(gameObject);
        rb.velocity = new Vector3();
    }

    public void SetObjectPool(ObjectPool objectPool)
    {
        bulletPool = objectPool;
    }
}
