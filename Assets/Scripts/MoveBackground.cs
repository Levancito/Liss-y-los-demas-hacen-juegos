using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBackground : MonoBehaviour
{
    public float velocidad = 0.1f; 
    private Material material;

    void Start()
    {
        material = GetComponent<Renderer>().material;
    }

    void Update()
    {
        float offsetY = Time.time * velocidad; 
        material.mainTextureOffset = new Vector2(0, offsetY);
    }
}