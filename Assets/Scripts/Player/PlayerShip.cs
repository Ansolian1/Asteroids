using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShip : MonoBehaviour
{
    public float maxVelocity = 0;
    public float rotationSpeed = 0;
    public float acceleration = 0;
    public float fireRate = 1/3;
    public Transform bulletStartPosition;

    private Rigidbody rb;
    [SerializeField]
    private ObjectPool bulletPool;
    [SerializeField]
    private ObjectReplacer objectReplacer;

    [SerializeField]
    private AudioClip bustSound;
    [SerializeField]
    private AudioClip shotSound;
    [SerializeField]
    private AudioSource audioSource;

    private float lastShotTime;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        objectReplacer.AddObject(gameObject);
    }

    public void NullifyVelocity()
    {
        rb.velocity = Vector3.zero;
        transform.rotation = Quaternion.identity;
    }

    public void AddAcceleration()
    {
        rb.AddForce(transform.localRotation * Vector3.forward * acceleration, ForceMode.Acceleration);

        if (!audioSource.isPlaying)
        {
            audioSource.clip = bustSound;
            audioSource.Play();
        }

        if (rb.velocity.sqrMagnitude > maxVelocity * maxVelocity)
        {
            rb.velocity = rb.velocity.normalized * maxVelocity;
        }
    }

    public void RotateLeft()
    {
        transform.Rotate(Vector3.up, -rotationSpeed);
    }

    public void RotateRight()
    {
        transform.Rotate(Vector3.up, rotationSpeed);
    }

    public void Shoot()
    {
        if(Time.time - lastShotTime > fireRate)
        {
            audioSource.clip = shotSound;
            audioSource.Play();

            GameObject bullet = bulletPool.GetFromPool();
            bullet.transform.position = bulletStartPosition.position;
            bullet.transform.rotation = transform.rotation;
            bullet.GetComponent<Bullet>().LaunchBullet();
            lastShotTime = Time.time;
        }
    }
}
