using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFOSpawner : MonoBehaviour
{
    public float timeBetweenUfo = 20f;
    public float ufoSpeed = 3f;

    [SerializeField]
    private GameObject ufo;

    [SerializeField]
    private AudioSource audioSource;

    private float lastUfoTime;
    private GameObject spawnedUFO;

    public void RestartGame()
    {
        lastUfoTime = Time.time;
        DestroyUFO(spawnedUFO, 0);
    }

    void Update()
    {
        if(Time.time - lastUfoTime > timeBetweenUfo && spawnedUFO == null)
        {
            SpawnUFO();
        }
    }

    private void SpawnUFO()
    {
        float xPosition = Random.value < 0.5f ? -GlobalAccess.Instance.xBorder : GlobalAccess.Instance.xBorder;
        float zPosition = Random.Range(-GlobalAccess.Instance.zBorder * 0.8f, GlobalAccess.Instance.zBorder * 0.8f);
        Vector3 direction = xPosition > 0 ? Vector3.left : Vector3.right;
        ufo.SetActive(true);
        ufo.transform.position = new Vector3(xPosition, transform.position.y, zPosition);
        ufo.GetComponent<Rigidbody>().AddForce(transform.localRotation * direction * ufoSpeed, ForceMode.VelocityChange);
        ufo.GetComponent<UFO>().enabled = true;
        spawnedUFO = ufo;
        GlobalAccess.Instance.objectReplacer.AddObject(ufo);
    }

    public void DestroyUFO(GameObject ufo, int score)
    {
        audioSource.Play();
        GlobalAccess.Instance.scoreManager.AddScore(score);
        if (ufo == null)
            return;
        lastUfoTime = Time.time;
        GlobalAccess.Instance.objectReplacer.RemoveObject(ufo);
        ufo.GetComponent<Rigidbody>().velocity = Vector3.zero;
        ufo.GetComponent<UFO>().enabled = false;
        spawnedUFO = null;
        ufo.SetActive(false);
    }
}
