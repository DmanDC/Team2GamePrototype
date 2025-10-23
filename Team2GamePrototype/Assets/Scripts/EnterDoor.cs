using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class EnterDoor : MonoBehaviour
{
    private bool enterAllowed;
    private string sceneToLoad;
    private string targetSpawnId;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<DungeonEntrance>())
        {
            sceneToLoad = "Dungeon1stFloor";
            targetSpawnId = "FromEntrance";
            enterAllowed = true;
        }
        else if (collision.GetComponent<DungeonExit>())
        {
            sceneToLoad = "TutorialStage";
            targetSpawnId = "FromExit";
            enterAllowed = true;
        }

        if (collision.GetComponent<FirstDoor>())
        {
            sceneToLoad = "Dungeon1stFloorRoom2";
            targetSpawnId = "FromEntrance";
            enterAllowed = true;
        }
        else if (collision.GetComponent<FirstDoorExit>())
        {
            sceneToLoad = "Dungeon1stFloor";
            targetSpawnId = "FromExit";
            enterAllowed = true;
        }
        if (collision.GetComponent<Latter>())
        {
            sceneToLoad = "BossRoom";
            targetSpawnId = "FromEntrance";
            enterAllowed = true;
        }
        /* else if (collision.GetComponent<Latter2>())
         {
             sceneToLoad = "Dungeon1stFloorRoom2";
             targetSpawnId = "FromExit";
             enterAllowed = true;
         }
        */
        if (collision.GetComponent<ForestPath1>())
        {
            sceneToLoad = "ForestPath1";
            targetSpawnId = "Path1";
            enterAllowed = true;
        }
        else if (collision.GetComponent<ForestPathExit>())
        {
            sceneToLoad = "ForestStart";
            targetSpawnId = "Path1";
            enterAllowed = true;
        }
        if (collision.GetComponent<ForestPath2>()) {
            sceneToLoad = "ForestPath2";
            targetSpawnId = "Path2";
            enterAllowed = true;
        }
        else if (collision.GetComponent<ForestPath2Exit>()) {
            sceneToLoad = "ForestStart";
            targetSpawnId = "Path2";
            enterAllowed = true;
        }
        if (collision.GetComponent<ForestPath3>()) {
            sceneToLoad = "ForestPath3";
            targetSpawnId = "Path3";
            enterAllowed = true;
        }
        else if (collision.GetComponent<ForestPath3Exit>())
        {
            sceneToLoad = "ForestStart";
            targetSpawnId = "Path3";
            enterAllowed = true;
        }
        if (collision.GetComponent<CaveEntrance>())
        {
            sceneToLoad = "CaveBoss";
            targetSpawnId = "CaveStart";
            enterAllowed = true;
        }
       /* else if (collision.GetComponent<CaveExit>())
        {
            sceneToLoad = "ForestPath3";
            targetSpawnId = "CaveStart";
            enterAllowed = true;
        }
       */
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<DungeonEntrance>() ||
        collision.GetComponent<DungeonExit>() ||
        collision.GetComponent<FirstDoor>() ||
        collision.GetComponent<FirstDoorExit>() ||
        collision.GetComponent<Latter>() ||
        collision.GetComponent<Latter2>() ||
        collision.GetComponent<ForestPath1>() || 
        collision.GetComponent<ForestPathExit>() ||
        collision.GetComponent<ForestPath2Exit>() ||
        collision.GetComponent<ForestPath2>() || 
        collision.GetComponent<ForestPath3>() ||
        collision.GetComponent<ForestPath3Exit>() ||
        collision.GetComponent<CaveEntrance>() ||
        collision.GetComponent<CaveExit>()

        )
        {
            enterAllowed = false;
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    private void Update()
    {
        if (enterAllowed && (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter)))
        {
            SceneSpawnRouter.NextSpawnId = targetSpawnId;
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
