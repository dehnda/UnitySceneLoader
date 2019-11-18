using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class for testing purposes.
/// </summary>
public class LoadLevelOnClick : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            FindObjectOfType<SceneLoaderController>().LoadSceneAsync();
        }
    }
}
