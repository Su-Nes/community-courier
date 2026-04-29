using System.Collections;
using System.Collections.Generic;
using PurrNet;
using PurrNet.Modules;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadManager : NetworkBehaviour
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
        if (!isServer)
            yield break;

        yield return networkManager.sceneModule.LoadSceneAsync(sceneIndex, LoadSceneMode.Additive);
    }
}
