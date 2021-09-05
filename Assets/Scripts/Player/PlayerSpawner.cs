using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public int startLivesCount;
    public Vector3 startPosition;
    [SerializeField]
    private GameMenu gameMenu;
    [SerializeField]
    private Material playerMaterial;

    [HideInInspector]
    public int livesCount;
    private bool isInvulnerable = false;

    public void RestartGame()
    {
        SpawnPlayerShip();
        livesCount = startLivesCount;
        gameMenu.SetLivesCount(livesCount);
    }

    private void SpawnPlayerShip()
    {
        transform.position = startPosition;
        StartCoroutine(InvulnerabilityEffect());
    }

    IEnumerator InvulnerabilityEffect()
    {
        isInvulnerable = true;
        StartCoroutine(InvulnerableTimer());
        while (isInvulnerable)
        {
            StartCoroutine(FadeIn());
            yield return new WaitForSeconds(0.25f);
            StartCoroutine(FadeOut());
        }
    }

    IEnumerator InvulnerableTimer()
    {
        yield return new WaitForSeconds(3f);
        isInvulnerable = false;
    }

    IEnumerator FadeIn()
    {
        float t = 0.25f;

        while (t > 0f)
        {
            t -= Time.deltaTime;
            float a = 1 * t / 0.25f;
            playerMaterial.color = new Color(playerMaterial.color.r, playerMaterial.color.g, playerMaterial.color.b, a);
            yield return 0;
        }
    }

    IEnumerator FadeOut()
    {
        float t = 0;

        while (t < 0.25f)
        {
            t += Time.deltaTime;
            float a = 1 * t / 0.25f;
            playerMaterial.color = new Color(playerMaterial.color.r, playerMaterial.color.g, playerMaterial.color.b, a);
            yield return 0;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Bullet") && !isInvulnerable)
        {
            livesCount--;
            Bullet bullet = other.GetComponent<Bullet>();
            if (bullet)
            {
                bullet.DestroyBullet();
            }
            gameMenu.SetLivesCount(livesCount);
            if (livesCount == 0)
            {
                gameMenu.GameOver();
            }
            else
            {
                SpawnPlayerShip();
            }
        }
    }
}
