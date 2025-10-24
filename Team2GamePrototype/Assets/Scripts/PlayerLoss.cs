using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerLoss : MonoBehaviour
{
    public TMP_Text textbox;
    public static bool gameOver;
    public float lowestY;
    public float reachThreashold = 0.1f;
    public Vector2 targetPosition;
    [Header("Restart targets")]
    [SerializeField] private string startSceneName = "TutorialStage"; // ← set your beginning scene name in Inspector
    private string pendingRestartScene = null;

    [Header("Win when a specific boss dies")]
    public GameObject bossTarget;      // drag your boss here
    public bool winOnBossDeath = false; // toggle this on/off in Inspector


    private Transform playerTr;
    private PlayerHealth playerHealth;
    private bool bossArmed = false;
    public bool respawnToStartScene = true;

    void Start()
    {
        gameOver = false;
        TryResolvePlayer();
        if (textbox) textbox.text = "GO!  --> ";
    }

    void Update()
    {
        // re-acquire if the player clone was (re)spawned
        if (playerTr == null || playerHealth == null) TryResolvePlayer();

        // ARM when the boss is present (alive) in this scene
        if (!bossArmed && bossTarget != null && bossTarget.activeInHierarchy)
            bossArmed = true;

        // ---- Lose: real player death ----
        if (!gameOver && playerHealth != null && playerHealth.death)
        {
            gameOver = true;
            if (textbox) textbox.text = "You died! \n Press R to try again";
        }

        // ---- Lose: fell below threshold (check the PLAYER, not this object) ----
        if (!gameOver && playerTr != null && playerTr.position.y < lowestY)
        {
            gameOver = true;
            if (textbox) textbox.text = "You died! \n Press R to try again";
        }

        if (!gameOver && winOnBossDeath && bossArmed &&
       (bossTarget == null || !bossTarget.activeInHierarchy))
        {
            gameOver = true;
            if (textbox) textbox.text = "You win! \n Press R to try again!";
        }

        // Not over: keep HUD and make sure R can't trigger yet
        if (!gameOver)
        {
            if (textbox) textbox.text = "GO!  --> ";
            return;
        }

        // ---- Win by reaching target (use player position; DO NOT disable this script) ----
        if (playerTr != null)
        {
            float distance = Vector2.Distance(playerTr.position, targetPosition);
            if (distance < reachThreashold)
            {
                Debug.Log("The end has been reached!");
                // enabled = false;  // ← REMOVE this from your file
                gameOver = true;
                if (textbox) textbox.text = "You win! \n Press R to try again!";
            }
        }

        // ---- R only works after gameOver ----
        if (Input.GetKeyDown(KeyCode.R))
        {
            string targetScene = respawnToStartScene
                ? startSceneName                         // chose which scene
                : UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;  // same scene
            
            UnityEngine.SceneManagement.SceneManager.LoadScene(targetScene);
            Debug.Log($"[Respawn] respawnToStartScene={respawnToStartScene}, start='{startSceneName}', loading '{targetScene}'");
        }


    }

    void TryResolvePlayer()
    {
        var playerGo = GameObject.FindGameObjectWithTag("Player");
        playerTr = playerGo ? playerGo.transform : null;
        playerHealth = playerGo ? playerGo.GetComponent<PlayerHealth>() : null;
    }

    //ensure static resets every scene load
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    static void ResetFlag() { gameOver = false; }
}
