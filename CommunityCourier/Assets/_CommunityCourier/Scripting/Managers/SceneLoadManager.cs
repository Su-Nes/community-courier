using System.Collections;
using System.Collections.Generic;
using PurrNet;
using PurrNet.Modules;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadManager : NetworkBehaviour
{
    public static SceneLoadManager instance;

    [PurrScene, SerializeField] private string islandScene;

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
        StartCoroutine(LoadScene(islandScene));
    }

    private IEnumerator LoadScene(string sceneName)
    {
        if (!isServer)
            yield break;

        yield return networkManager.sceneModule.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
    }
}
