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
        // If someone already spawned a Player in this scene, do nothing
        var existing = GameObject.FindGameObjectWithTag("Player");
        if (existing != null) return;

        // Route logic: only the matching spawner should fire when a door set NextSpawnId
        string route = SceneSpawnRouter.NextSpawnId;
        bool hasRoute = !string.IsNullOrEmpty(route);

        if (hasRoute && route != spawnId) return;

        // Decide spawn position:
        // - If a route was provided, use THIS spawner’s transform
        // - If no route (fresh load via Restart), prefer mapStart if assigned; else this transform
        Vector3 spawnPos = (hasRoute || mapStart == null) ? transform.position : mapStart.position;

        var playerClone = Instantiate(PlatformerPlayer, spawnPos, Quaternion.identity);

        var cam = Camera.main ? Camera.main.GetComponent<CamFollowPlayer>() : null;
        if (cam != null) cam.player = playerClone;

        // Clear the router after the correct spawner has fired
        SceneSpawnRouter.NextSpawnId = null;
    }



}
