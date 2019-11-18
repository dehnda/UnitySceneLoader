using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Main class for prefab usage.
/// </summary>
[RequireComponent(typeof(SceneLoader))]
public class SceneLoaderController : MonoBehaviour
{
    private SceneLoader sceneLoader;
    public int sceneIndex;


    private void Start()
    {
        sceneLoader = this.gameObject.GetComponentInChildren<SceneLoader>();
    }

    /// <summary>
    /// Starts async scene loading.
    /// </summary>
    public void LoadSceneAsync()
    {
        sceneLoader.DoTransition(sceneIndex);
    }
}
