using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectPool : MonoBehaviour
{
    public bool PersistentObject = false;
    public PooledObject Prefab;

    private List<PooledObject> _availableObjects = new List<PooledObject>();

    private void Awake()
    {
        if (PersistentObject)
            DontDestroyOnLoad(gameObject);
    }

    public void AddObject(PooledObject obj)
    {
        obj.gameObject.SetActive(false);
        _availableObjects.Add(obj);
    }

    public PooledObject GetObject()
    {
        PooledObject obj;
        int lastAvailableIndex = _availableObjects.Count - 1;
        if (lastAvailableIndex >= 0)
        {
            obj = _availableObjects[lastAvailableIndex];
            _availableObjects.RemoveAt(lastAvailableIndex);
            obj.gameObject.SetActive(true);
        }
        else
        {
            obj = Instantiate<PooledObject>(Prefab);
            obj.transform.SetParent(transform, false);
            obj.Pool = this;
            if (PersistentObject)
                DontDestroyOnLoad(obj.gameObject);
        }
        return obj;
    }
}
