using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{
    [SerializeField]
    private GameObject PlatformerPlayer;

    // NEW: assign this in the Inspector to your start-of-map marker
    [SerializeField] private Transform mapStart;

    [SerializeField] private string spawnId = "FromEntrance";

    private static bool firstSpawnDone = false;
// Start is called before the first frame update
    void Start()
    {
        // Prevent duplicates if something already spawned a Player
        var existing = GameObject.FindGameObjectWithTag("Player");
        if (existing != null) return;

        string route = SceneSpawnRouter.NextSpawnId;
        bool hasRoute = !string.IsNullOrEmpty(route);

        // If a door specified a spawn, only the matching SpawnPlayer should run
        if (hasRoute && route != spawnId) return;

        // If no route is provided (e.g., first ever entry), you can either:
        //   - return;                         // require a door route, OR
        //   - allow a single default spawn (your existing firstSpawnDone logic)
        // Here we allow your first-time start behavior:
        if (!hasRoute && firstSpawnDone) return;


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
