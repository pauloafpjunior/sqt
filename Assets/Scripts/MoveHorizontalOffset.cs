using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveHorizontalOffset : MonoBehaviour
{
    private Material material;
    public float velocityX, inc;
    private float offset;


    void Start()
    {
        material = GetComponent<Renderer>().material;
    }

    void Update()
    {
        offset += inc;
        material.SetTextureOffset("_MainTex", new Vector2(offset * velocityX, 0));
    }
}
