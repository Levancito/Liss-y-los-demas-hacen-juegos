using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.UIElements;

public class Pool : MonoBehaviour
{
    [SerializeField] private uint initPoolSize = 3;
    [SerializeField] private PooledObject objectToPool;
    [SerializeField] private bulletFactory bulletFactory;

    private Stack<PooledObject> stack;

    private void Awake()
    {
        SetupPool();
    }

    private void SetupPool()
    {
        stack = new Stack<PooledObject>();

        for (int i = 0; i < initPoolSize; i++)
        {
            PooledObject instance = Instantiate(objectToPool);
            instance.Pool = this;
            instance.gameObject.SetActive(false);
            stack.Push(instance);
        }
    }

    public PooledObject GetPooledObject(Vector3 position)
    {
        if (stack.Count == 0)
        {
            PooledObject newInstance = bulletFactory.GetProduct(position) as PooledObject;
            newInstance.Pool = this;
            return newInstance;
        }

        PooledObject nextInstance = stack.Pop();
        nextInstance.transform.position = position; // Actualizar posición
        nextInstance.gameObject.SetActive(true);
        return nextInstance;
    }

    public void ReturnToPool(PooledObject pooledObject)
    {
        pooledObject.gameObject.SetActive(false);
        stack.Push(pooledObject);
    }
}
