using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ObjectPool<T> : MonoSingleton<ObjectPool<T>> where T : MonoBehaviour
{
    [Header("Serialized Variables")]
    [SerializeField] protected T prefab;

    [Header("Private Variables")]
    private List<T> pooledObjects;
    private int amount;
    private bool isReady;

    // create the pool, with a specific amount of objects
    public void PoolObjects(int amount = 0)
    {
        if (amount < 0)
            throw new ArgumentOutOfRangeException("Amount to pool must be non-negative.");
        this.amount = amount;

        pooledObjects = new List<T>(amount);

        GameObject newObject;
        for (int i = 0; i != amount; i++)
        {
            newObject = Instantiate(prefab.gameObject,transform);
            newObject.SetActive(false);
            pooledObjects.Add(newObject.GetComponent<T>());
        }
        isReady = true;
    }

    // get an object from the pool
    public T GetPooledObject()
    {
        if (!isReady)
            PoolObjects(1);

        for (int i = 0; i != amount; i++)
            if (!pooledObjects[i].isActiveAndEnabled)
                return pooledObjects[i];

        // if we didn't find anything, make a new one
        GameObject newObject = Instantiate(prefab.gameObject, transform);
        newObject.SetActive(false);
        pooledObjects.Add(newObject.GetComponent<T>());
        ++amount;
        return newObject.GetComponent<T>();
    }

    // return an object back to the pool
    public void ReturnObjectToPool(T toBeReturned)
    {
        if (toBeReturned == null)
            return;

        if (!isReady)
        {
            PoolObjects();
            pooledObjects.Add(toBeReturned);
        }

        toBeReturned.gameObject.SetActive(false);
    }
}
