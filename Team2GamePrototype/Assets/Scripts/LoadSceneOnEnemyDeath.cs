using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LoadSceneOnEnemyDeath : MonoBehaviour
{
    // Start is called before the first frame update
    public string sceneToLoad = "";

    
    public float delay = 0f;

    // Call this from your health script, hit logic, or death animation event
    public void Trigger()
    {
        if (string.IsNullOrEmpty(sceneToLoad)) return;
        if (delay <= 0f) SceneManager.LoadScene(sceneToLoad);
        else StartCoroutine(LoadAfterDelay());
    }

    System.Collections.IEnumerator LoadAfterDelay()
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneToLoad);
    }
}
