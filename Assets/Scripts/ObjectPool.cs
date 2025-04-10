using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool
{
    GameObject prototype;
    List<GameObject> pool;
    public bool canGrow;

    public ObjectPool(GameObject prefab, bool canGrow, int count)
    {
        prototype = prefab;
        pool = new List<GameObject>();
        this.canGrow = canGrow;
        for (int i = 0; i < count; i++)
        {
            GameObject next = GameObject.Instantiate(prototype);
            next.SetActive(false);
            pool.Add(next);
        }
    }

    public GameObject GetObject()
    {
        for (int i = 0;i < pool.Count; i++)
        {
            if (!pool[i].activeSelf)
            {
                pool[i].SetActive(true);
                return pool[i];
            }
        }
        if (canGrow)
        {
            GameObject next = GameObject.Instantiate(prototype);
            pool.Add(next);
            return next;
        }
        return null;
    }
}

