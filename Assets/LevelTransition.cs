using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class LevelTransition : MonoBehaviour
{
    public Animator animator;
    public GameObject loadingScreen;
    public int sceneIndex;

    private SceneLoader sceneLoader;
    void Start()
    {
        sceneLoader = loadingScreen.GetComponent<SceneLoader>();
        if (sceneLoader == null) Debug.LogError("sceneloader reference in LevelTransition NULL");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            FadeToLevel(1);
        }
    }

    public void FadeToLevel(int levelIndex)
    {
        animator.SetTrigger("FadeOut");
    }

    public void OnFadeComplete()
    {
        sceneLoader.LoadScene(sceneIndex);
    }
}
