using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire_world_lava : MonoBehaviour {
    float scrollSpeed = 0.1f;
    public Renderer rend;
    float offset_time = 0;
    Vector2 offset;
    void Start()
    {
        rend = GetComponent<Renderer>();
    }
    void Update()
    {
        if (Time.time - offset_time >= 0.02f)
        {
            offset.Set(-1 * Time.time * scrollSpeed, 0);
            rend.material.SetTextureOffset("_MainTex", offset);
        }
    }
}
