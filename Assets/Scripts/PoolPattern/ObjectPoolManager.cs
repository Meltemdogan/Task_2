using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct PoolID
{
    public string poolName;
    public GameObject prefab;
    public int initialSize;
}
public class ObjectPoolManager : MonoBehaviour
{
    public static ObjectPoolManager Instance;

    private Dictionary<string, Queue<GameObject>> pools = new Dictionary<string, Queue<GameObject>>();
    public Dictionary<string, GameObject> prefabDict = new Dictionary<string, GameObject>();
    [SerializeField] private List<PoolID> poolIDs = new List<PoolID>();
    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        foreach (PoolID poolID in poolIDs)
        {
            CreatePool(poolID.poolName, poolID.prefab, poolID.initialSize);
        }
    }

    public void CreatePool(string poolName, GameObject prefab, int initialSize)
    {
        if (!pools.ContainsKey(poolName))
        {
            Queue<GameObject> newPool = new Queue<GameObject>();
            pools[poolName] = newPool;
            prefabDict[poolName] = prefab;

            for (int i = 0; i < initialSize; i++)
            {
                GameObject obj = Instantiate(prefab);
                obj.SetActive(false);
                newPool.Enqueue(obj);
            }
        }
    }

    public GameObject GetObject(string poolName)
    {
        if (pools.ContainsKey(poolName) && pools[poolName].Count > 0)
        {
            GameObject obj = pools[poolName].Dequeue();
            obj.SetActive(true);
            return obj;
        }

        // Eğer havuz boşsa yeni bir nesne oluştur.
        if (prefabDict.ContainsKey(poolName))
        {
            GameObject newObj = Instantiate(prefabDict[poolName]);
            newObj.SetActive(true);
            pools [poolName].Enqueue(newObj);
            return newObj;
        }

        Debug.LogWarning("Pool or prefab not found: " + poolName);
        return null;
    }

    public void ReturnObject(string poolName, GameObject obj)
    {
        if (pools.ContainsKey(poolName))
        {
            obj.SetActive(false);
            pools[poolName].Enqueue(obj);
        }
    }
}
