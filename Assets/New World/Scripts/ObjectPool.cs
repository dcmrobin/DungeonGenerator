using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public GameObject prefab;
    public int initialPoolSize = 10;

    private Queue<GameObject> pool = new Queue<GameObject>();

    private void Start()
    {
        for (int i = 0; i < initialPoolSize; i++)
        {
            GameObject obj = Instantiate(prefab);
            obj.SetActive(false);
            pool.Enqueue(obj);
        }
    }

    public GameObject GetObject(Vector3 position, Quaternion rotation)
    {
        if (pool.Count > 0)
        {
            GameObject obj = pool.Dequeue();
            obj.transform.position = position;
            obj.transform.rotation = rotation;
            obj.SetActive(true);
            ResetWalls(obj);
            return obj;
        }
        else
        {
            GameObject obj = Instantiate(prefab, position, rotation);
            return obj;
        }
    }

    public void ReturnObject(GameObject obj)
    {
        obj.SetActive(false);
        pool.Enqueue(obj);
    }

    private void ResetWalls(GameObject segment)
    {
        Transform walls = segment.transform.Find("Walls");
        int numWalls = walls.childCount;
        for (int i = 0; i < numWalls; i++)
        {
            walls.GetChild(i).gameObject.SetActive(true);
            walls.GetChild(i).gameObject.SetActive(true);
        }
    }
}
