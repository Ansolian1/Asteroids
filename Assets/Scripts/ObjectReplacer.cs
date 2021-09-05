using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectReplacer : MonoBehaviour
{
    public float xDistance = 0f;
    public float zDistance = 0f;

    private void Start()
    {
        xDistance = GlobalAccess.Instance.xBorder;
        zDistance = GlobalAccess.Instance.zBorder;
    }

    [SerializeField]
    private List<GameObject> placedObjects;

    private void Update()
    {
        foreach (var placedObject in placedObjects)
        {
            CheckHeight(placedObject);
            CheckWidth(placedObject);
        }
    }

    public void AddObject(GameObject gameObject)
    {
        placedObjects.Add(gameObject);
    }

    public void RemoveObject(GameObject gameObject)
    {
        placedObjects.Remove(gameObject);
    }

    private void CheckHeight(GameObject placedObject)
    {
        if (placedObject.transform.position.z > zDistance)
        {
            placedObject.transform.position = new Vector3(placedObject.transform.position.x, placedObject.transform.position.y, placedObject.transform.position.z * -1 + 0.1f);
        }
        else if (placedObject.transform.position.z < -zDistance)
        {
            placedObject.transform.position = new Vector3(placedObject.transform.position.x, placedObject.transform.position.y, placedObject.transform.position.z * -1 - 0.1f);
        }
    }

    private void CheckWidth(GameObject placedObject)
    {
        if (placedObject.transform.position.x > xDistance)
        {
            placedObject.transform.position = new Vector3(placedObject.transform.position.x * -1 + 0.1f, placedObject.transform.position.y, placedObject.transform.position.z);
        }
        else if (placedObject.transform.position.x < -xDistance)
        {
            placedObject.transform.position = new Vector3(placedObject.transform.position.x * -1 - 0.1f, placedObject.transform.position.y, placedObject.transform.position.z);
        }
    }
}
