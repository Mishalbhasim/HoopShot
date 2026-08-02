using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> where T  : Component, IPoolable
{

    private T prefab;

    private Queue<T> _objectPool = new Queue<T>();

    public ObjectPool(T prefab)
    {
        this.prefab = prefab;
    }

    public T Get()
    {
        if(_objectPool.Count > 0)
        {
            T item = _objectPool.Dequeue();
            item.gameObject.SetActive(true);
            item.OnSpawn();
            return item;
        }else
        {
            T item = Object.Instantiate(prefab);
            item.OnSpawn();
            return item;
        }
    }

    public void Return(T item)
    {
        item.OnDespawn();
        item.gameObject.SetActive(false);
        _objectPool.Enqueue(item);
    }
}
