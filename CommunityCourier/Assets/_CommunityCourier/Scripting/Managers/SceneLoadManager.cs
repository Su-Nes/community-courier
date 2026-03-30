using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadManager : MonoBehaviour
{
    public static SceneLoadManager instance;

    [SerializeField] private int[] islandIndexes;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else 
            Destroy(gameObject);
    }

    public void UnloadScene(int sceneIndex)
    {
        SceneManager.UnloadSceneAsync(sceneIndex);
    }

    public void LoadRandomIsland()
    {
        StartCoroutine(LoadScene(islandIndexes[Random.Range(0, islandIndexes.Length)]));
    }

    private IEnumerator LoadScene(int sceneIndex)
    {
        yield return SceneManager.LoadSceneAsync(sceneIndex, LoadSceneMode.Additive);
        SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(sceneIndex));
    }
}
