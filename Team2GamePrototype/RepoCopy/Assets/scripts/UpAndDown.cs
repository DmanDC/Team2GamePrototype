using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpAndDown : MonoBehaviour
{
    public float moveDistance = 3f;
    public float speed = 2f;
    private Vector3 startPos;

    void Start() { startPos = transform.position; }

    void Update()
    {
        float y = startPos.y + Mathf.Sin(Time.time * speed) * moveDistance;
        transform.position = new Vector3(startPos.x, y, startPos.z);
    }
}
