using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;

    private void Start()
    {
        transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
    }
    private void Update()
    {
        transform.position += transform.right * speed * Time.deltaTime;
        if (Vector3.Distance(transform.position, Vector3.zero) > 100f)
        {
            Destroy(gameObject);
        }
    }
}