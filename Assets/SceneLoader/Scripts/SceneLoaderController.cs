using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneLoaderController : MonoBehaviour
{
    private SceneLoader sceneLoader;
    public int sceneIndex;


    private void Start()
    {
        sceneLoader = this.gameObject.GetComponentInChildren<SceneLoader>();
    }

    public void LoadSceneAsync()
    {
        sceneLoader.DoTransition(sceneIndex);
    }
}
