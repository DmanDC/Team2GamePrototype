using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollowPlayer : MonoBehaviour
{
    //Set this reference in the inspector
    public GameObject player;
    [SerializeField]
    private float _minX;
    [SerializeField]
    private float _minY;
    [SerializeField]
    private float _maxX;

    [SerializeField]
    private float _maxY;


    // Update is called once per frame
    void LateUpdate()
    {
        if (!player) return;

        // Follow player but never let camera center go below min X/Y
        float targetX = Mathf.Clamp(player.transform.position.x, _minX, _maxX);
        float targetY = Mathf.Clamp(player.transform.position.y, _minY, _maxY);

        transform.position = new Vector3(targetX, targetY, transform.position.z);
    }

}
