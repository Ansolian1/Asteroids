using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField]
    private GameObject prefab;
    public int poolCount = 10;
    private Queue<GameObject> availableObjects = new Queue<GameObject>();

    void Start()
    {
        GrowPool();
    }

    private void GrowPool()
    {
        for (int i = 0; i < poolCount; i++)
        {
            GameObject pooledObject = Instantiate(prefab);
            GlobalAccess.Instance.objectReplacer.AddObject(pooledObject);
            pooledObject.GetComponent<IPooledObject>().SetObjectPool(this);
            pooledObject.transform.SetParent(transform);
            AddToPool(pooledObject);
        }
    }

    public void AddToPool(GameObject pooledObject)
    {
        pooledObject.SetActive(false);
        availableObjects.Enqueue(pooledObject);
    }

    public GameObject GetFromPool()
    {
        if (availableObjects.Count == 0)
        {
            GrowPool();
        }

        GameObject pooledObject = availableObjects.Dequeue();
        pooledObject.SetActive(true);
        return pooledObject;
    }

    public void AllToPool()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            AddToPool(transform.GetChild(i).gameObject);
        }
    }
}
