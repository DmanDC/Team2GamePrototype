using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

public class EnterDoor : MonoBehaviour
{
    private bool enterAllowed;
    private string sceneToLoad;
   private void OnTriggerEnter2D(Collider2D collision)
    {
      if (collision.GetComponent<DungeonEntrance>())
        {
            sceneToLoad = "Dungeon1stFloor";
            enterAllowed = true;
        }
else if (collision.GetComponent<DungeonExit>())
        {
            sceneToLoad = "TutorialStage";
                enterAllowed = true;
        }

      /*  if (collision.GetComponent<FirstDoor>())
        {
            sceneToLoad = "Dungeon1stFloorRoom2";
            enterAllowed = true;
        }
        else if (collision.GetComponent<FirstDoorExit>())
        {
            sceneToLoad = "Dungeon1stFloor";
            enterAllowed = true;
        }
      */
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<DungeonEntrance>() || collision.GetComponent<DungeonExit>())
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
      if(enterAllowed && Input.GetKey(KeyCode.Return))
        {
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
