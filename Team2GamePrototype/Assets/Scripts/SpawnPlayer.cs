using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{
    [SerializeField]
    private GameObject PlatformerPlayer;

    // NEW: assign this in the Inspector to your start-of-map marker
    [SerializeField] private Transform mapStart;
    
    private static bool firstSpawnDone = false;
// Start is called before the first frame update
    void Start()
    {
        // Use mapStart ONLY the first time; after that, use this object's position
        Vector3 spawnPos = (!firstSpawnDone && mapStart != null) ? mapStart.position : transform.position;

        GameObject playerClone = Instantiate(PlatformerPlayer, spawnPos, Quaternion.identity);

        // Tell the camera to follow the freshly spawned clone (unchanged behavior)
        CamFollowPlayer cam = Camera.main.GetComponent<CamFollowPlayer>();
        if (cam != null) cam.player = playerClone;

        // Mark that we've done the first-start spawn
        firstSpawnDone = true;
    }


}
