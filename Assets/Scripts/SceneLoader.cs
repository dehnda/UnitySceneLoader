using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(HintManager))]
public class SceneLoader : MonoBehaviour
{
    private HintManager hintManager;
    public GameObject loadingCanvas;
    public Slider slider;
    public Text progressText;
    public Text hint;

    public void LoadScene(int sceneIndex)
    {
        hintManager = GetComponent<HintManager>();
        StartCoroutine(LoadAsynchronously(sceneIndex));

    }

    IEnumerator LoadAsynchronously(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        hint.text = hintManager.GetNextHint();
        loadingCanvas.SetActive(true);

        // Get current time to find out when to show next hint
        float now = Time.time;

        while (!operation.isDone)
        {
            // check if next hint should be displayed
            if ((now + hintManager.GetHintTime()) >= Time.time)
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
