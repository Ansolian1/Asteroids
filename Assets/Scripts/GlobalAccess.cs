using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalAccess : MonoBehaviour
{
    public ObjectReplacer objectReplacer;
    public AsteroidsSpawner asteroidsSpawner;
    public UFOSpawner ufoSpawner;

    public PlayerShip playerShip;
    public ScoreManager scoreManager;

    public ObjectPool bulletsPool;
    public ObjectPool enemyBulletsPool;
    public ObjectPool bigAsteroidsPool;
    public ObjectPool averageAsteroidsPool;
    public ObjectPool smallAsteroidsPool;

    public float xBorder = 0f;
    public float zBorder = 0f;

    public static GlobalAccess Instance;

    private void Awake()
    {
        Instance = this;

        float width = Camera.main.pixelWidth;
        float height = Camera.main.pixelHeight;

        Vector2 worldPoint = Camera.main.ScreenToWorldPoint(new Vector2(width, height));
        xBorder = worldPoint.x;
        zBorder = worldPoint.y + 3f;
    }


}
