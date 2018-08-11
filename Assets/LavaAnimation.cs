using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaAnimation : MonoBehaviour
{
    public Vector2 uvAnimationRate;
    private Material material;
    Vector2 uvOffset = Vector2.zero;
    // Use this for initialization
    void Start()
    {
        material = GetComponent<MeshRenderer>().materials[0];
    }

    // Update is called once per frame
    void Update()
    {
        uvOffset += uvAnimationRate * Time.deltaTime;
        material.mainTextureOffset = uvOffset;
    }
}
