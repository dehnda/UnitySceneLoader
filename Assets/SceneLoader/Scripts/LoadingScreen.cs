using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SceneLoadingSystem;

public class LoadingScreen : MonoBehaviour
{
    [SerializeField]
    private Canvas canvas = null;
    [SerializeField]
    private Slider slider = null;
    [SerializeField]
    private Text sliderPercentageText = null;
    [SerializeField]
    private Text hintText = null;


    public Canvas GetLoadingCanvas()
    {
        return canvas;
    }

    public Slider GetSlider()
    {
        return slider;
    }

    public Text GetHintText()
    {
        return hintText;
    }

    public Text GetSliderPercentageText()
    {
        return sliderPercentageText;
    }
}
