using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Asteroid;

public class AsteroidsSpawner : MonoBehaviour
{
    public float xSpawnArea = 0f;
    public float zSpawnArea = 0f;

    public float minAsteroidVelocity = 5f;
    public float maxAsteroidVelocity = 5f;

    private AudioSource audioSource;
    private float asteroidVelocity;
    private int currentAsteroidsCount = 0;
    private int beginAsteroidsCount = 2;

    private List<Asteroid> asteroids = new List<Asteroid>();

    private void Awake()
    {
        xSpawnArea = GlobalAccess.Instance.xBorder;
        zSpawnArea = GlobalAccess.Instance.zBorder;
        audioSource = GetComponent<AudioSource>();
    }

    public void RestartGame()
    {
        foreach (var asteroid in asteroids)
        {
            DestroyAsteroid(asteroid, false);
        }
        asteroids = new List<Asteroid>();
        beginAsteroidsCount = 2;
        currentAsteroidsCount = 0;
        SpawnBeginAsteroids(beginAsteroidsCount);
    }

    public void SpawnBeginAsteroids(int count)
    {
        asteroidVelocity = Random.Range(minAsteroidVelocity, maxAsteroidVelocity);
        for (int i = 0; i < count; i++)
        {
            SpawnAsteroid(GlobalAccess.Instance.bigAsteroidsPool, new Vector3(Random.Range(-xSpawnArea, xSpawnArea), transform.position.y, Random.Range(-zSpawnArea, zSpawnArea)), new Vector3(Random.Range(-1.0f, 1.0f), 0f, Random.Range(-1.0f, 1.0f)));
        }
    }

    public void DevideAsteroid(Asteroid asteroid, int score)
    {
        GlobalAccess.Instance.scoreManager.AddScore(score);
        if (asteroid.asteroidSize == Size.Small)
        {
            DestroyAsteroid(asteroid);
            return;
        }

        Vector3 direction = asteroid.GetComponent<Rigidbody>().velocity;
        direction.Normalize();
        Vector3 leftAsteroidDirection = Quaternion.AngleAxis(-45, Vector3.up) * direction;
        Vector3 rightAsteroidDirection = Quaternion.AngleAxis(45, Vector3.up) * direction;
        ObjectPool currentAsteroidsPool = null;

        switch (asteroid.asteroidSize)
        {
            case Size.Big:
                currentAsteroidsPool = GlobalAccess.Instance.averageAsteroidsPool;
                break;
            case Size.Average:
                currentAsteroidsPool = GlobalAccess.Instance.smallAsteroidsPool;
                break;
        }

        SpawnAsteroid(currentAsteroidsPool, asteroid.transform.position, leftAsteroidDirection);
        SpawnAsteroid(currentAsteroidsPool, asteroid.transform.position, rightAsteroidDirection);
        DestroyAsteroid(asteroid);
    }

    public void DestroyAsteroid(Asteroid asteroid, bool isWinChecking = true)
    {
        asteroid.AddToPoolAsteroid();
        if (isWinChecking)
        {
            audioSource.Play();
            currentAsteroidsCount--;
            TryRestart();
        }
    }

    private void SpawnAsteroid(ObjectPool asteroidsPool, Vector3 position, Vector3 direction)
    {
        GameObject asteroidObject = asteroidsPool.GetFromPool();
        asteroidObject.transform.position = position;
        asteroidObject.GetComponent<Rigidbody>().AddForce(direction * asteroidVelocity, ForceMode.VelocityChange);
        asteroids.Add(asteroidObject.GetComponent<Asteroid>());
        currentAsteroidsCount++;
    }

    private void TryRestart()
    {
        if (currentAsteroidsCount == 0)
            StartCoroutine(Restart());
    }

    IEnumerator Restart()
    {
        beginAsteroidsCount++;
        yield return new WaitForSeconds(2f);
        SpawnBeginAsteroids(beginAsteroidsCount);
    }

}
