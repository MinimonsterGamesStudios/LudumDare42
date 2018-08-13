using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaAnimation : MonoBehaviour
{
    public float flowSpeed = 1f;
    public Vector2 uvAnimationRate;
    private Material material;
    Vector2 uvOffset = Vector2.zero;
    public bool animateTexture = true;
    public bool beginFlow = false;
    // Use this for initialization
    void Start()
    {
        material = GetComponent<MeshRenderer>().materials[0];
    }

    // Update is called once per frame
    void Update()
    {
        if (animateTexture)
        {
            uvOffset += uvAnimationRate * Time.deltaTime;
            material.mainTextureOffset = uvOffset;
        }
        else if (beginFlow)
        {
            transform.position += Vector3.up * flowSpeed * Time.deltaTime;
        }
    }
}
