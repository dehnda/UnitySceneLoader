using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(HintManager))]
public class SceneLoader : MonoBehaviour
{
    private HintManager hintManager;
    private bool continueLoading = false;
    private AsyncOperation operation;
    public Canvas canvas;
    public Slider slider;
    public Text progressText;
    public Text hint;

    public void LoadScene(int sceneIndex)
    {
        hintManager = GetComponent<HintManager>();
        operation = SceneManager.LoadSceneAsync(sceneIndex);
        StartCoroutine(LoadAsynchronously(sceneIndex));
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
        hint.text = hintManager.GetNextHint();
        canvas.gameObject.SetActive(true);

        // Get current time to find out when to show next hint
        float now = Time.time;

        while (!operation.isDone)
        {
            // check if next hint should be displayed
            if ((now + hintManager.GetHintTime()) <= Time.time)
            {
                hint.text = hintManager.GetNextHint();
                now = Time.time;
            }

            float progress = Mathf.Clamp01(operation.progress / .9f);

            progressText.text = ((int)(progress * 100)).ToString() + " %";
            slider.value = progress;

            yield return null;
        }
    }
}
