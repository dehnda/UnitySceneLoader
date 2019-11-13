using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    [SerializeField]
    private GameObject hintManager;
    [SerializeField]
    private GameObject loadingScreen;
    [SerializeField]
    private GameObject transitionManager;
    private Canvas loadingCanvas;
    private bool continueLoading = false;
    private AsyncOperation operation;
    private Slider slider;
    private Text progressText;
    private Text hint;
    private HintManager hints;

    //protected delegate void EventTransitionFinished();
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

    public void DoTransition(int index)
    {
        sceneIndex = index;
        transitionManager.GetComponent<LevelTransition>().FadeToOut(OnSuchEvent);
    }

    void Start()
    {
        loadingCanvas = loadingScreen.GetComponent<LoadingScreen>().GetLoadingCanvas();
        slider = loadingScreen.GetComponent<LoadingScreen>().GetSlider();
        progressText = loadingScreen.GetComponent<LoadingScreen>().GetSliderPercentageText();
        hint = loadingScreen.GetComponent<LoadingScreen>().GetHintText();
        hints = hintManager.GetComponent<HintManager>();

        OnSuchEvent += EventHandlingTransitionFinished;
    }

    private void Update()
    {
        if (!continueLoading)
            return;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("space pressed");
            operation.allowSceneActivation = true;
        }
    }
    IEnumerator LoadAsynchronously(int sceneIndex)
    {
        hint.text = hints.GetNextHint();
        Debug.Log("hint text in sceneloader: " + hint.text);
        loadingCanvas.gameObject.SetActive(true);

        // Get current time to find out when to show next hint
        float now = Time.time;

        while (!operation.isDone)
        {
            // check if next hint should be displayed
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
}
