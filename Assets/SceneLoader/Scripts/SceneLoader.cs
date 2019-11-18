using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using SceneLoadingSystem;

/// <summary>
/// Main class for sceneloader. 
/// 
/// Holds all ui and logic objects.
/// </summary>
public class SceneLoader : MonoBehaviour
{
    [SerializeField]
    private GameObject hintManager = null;
    [SerializeField]
    private GameObject loadingScreen = null;
    [SerializeField]
    private GameObject transitionManager = null;
    private Canvas loadingCanvas;
    private AsyncOperation operation;
    private Slider slider;
    private Text progressText;
    private Text hint;
    private HintManager hints;
    private int sceneIndex;
    private event LevelTransition.EventTransitionFinished OnSuchEvent;

    private void EventHandlingTransitionFinished()
    {
        LoadScene();
    }

    private void LoadScene()
    {
        operation = SceneManager.LoadSceneAsync(sceneIndex);
        StartCoroutine(LoadAsynchronously(sceneIndex));
    }

    private void Start()
    {
        loadingCanvas = loadingScreen.GetComponent<LoadingScreen>().GetLoadingCanvas();
        slider = loadingScreen.GetComponent<LoadingScreen>().GetSlider();
        progressText = loadingScreen.GetComponent<LoadingScreen>().GetSliderPercentageText();
        hint = loadingScreen.GetComponent<LoadingScreen>().GetHintText();
        hints = hintManager.GetComponent<HintManager>();

        OnSuchEvent += EventHandlingTransitionFinished;
    }

    private IEnumerator LoadAsynchronously(int sceneIndex)
    {
        hint.text = hints.GetNextHint();
        loadingCanvas.gameObject.SetActive(true);

        float now = Time.time;

        while (!operation.isDone)
        {
            if ((now + hints.GetHintTime()) <= Time.time)
            {
                hint.text = hints.GetNextHint();
                now = Time.time;
            }

            float progress = Mathf.Clamp01(operation.progress / .9f);

            progressText.text = ((int)(progress * 100)).ToString() + " %";
            slider.value = progress;

            yield return null;
        }
    }

    public void DoTransition(int index)
    {
        sceneIndex = index;
        transitionManager.GetComponent<LevelTransition>().FadeToOut(OnSuchEvent);
    }
}
