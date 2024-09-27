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
    [SerializeField] public bool PowerUp = false; // Agregar referencia a PowerUp

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

    public PooledObject GetPooledObject(Vector3 position, Quaternion rotation)
    {
        PooledObject nextInstance = GetOrCreateObject(position, rotation);

        // Si el PowerUp está activo, instanciar dos balas más
        if (PowerUp)
        {
            float spreadAngle = 15f;

            Quaternion leftRotation = Quaternion.AngleAxis(-spreadAngle, Vector3.up) * rotation;
            Quaternion rightRotation = Quaternion.AngleAxis(spreadAngle, Vector3.up) * rotation;

            GetOrCreateObject(position, leftRotation);
            GetOrCreateObject(position, rightRotation);
        }

        return nextInstance;
    }

    private PooledObject GetOrCreateObject(Vector3 position, Quaternion rotation)
    {
        if (stack.Count == 0)
        {
            PooledObject newInstance = bulletFactory.GetProduct(position) as PooledObject;
            newInstance.Pool = this;
            newInstance.transform.position = position;
            newInstance.transform.rotation = rotation;
            return newInstance;
        }

        PooledObject nextInstance = stack.Pop();
        nextInstance.transform.position = position;
        nextInstance.transform.rotation = rotation;
        nextInstance.gameObject.SetActive(true);
        return nextInstance;
    }

    public void ReturnToPool(PooledObject pooledObject)
    {
        pooledObject.gameObject.SetActive(false);
        stack.Push(pooledObject);
    }
}