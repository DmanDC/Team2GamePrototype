using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhoSeesMe : MonoBehaviour
{
    void OnWillRenderObject()
    {
        var cam = Camera.current;
        if (cam != null)
            Debug.Log($"Renderer seen by: {cam.name} (Depth={cam.depth})");
    }
}
