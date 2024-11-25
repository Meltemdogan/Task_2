using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] GameObject poolObject;
    [SerializeField] private int poolSize = 20;
    
    List<GameObject> pool = new List<GameObject>();
    
    private void Start()
    {
        for(int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(poolObject);
            obj.SetActive(false);
            pool.Add(obj);
        }
    }
    public GameObject GetObject()
    {
        foreach (GameObject obj in pool)
        {
            if(!obj.activeSelf)
            {
                obj.SetActive(true);
                return obj;
            }
        }
        GameObject newObj = Instantiate(poolObject);
        newObj.SetActive(true);
        pool.Add(newObj);  
        return newObj;
    }
}
