using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class LevelTransition : MonoBehaviour
{
    private Animator animator;
    public delegate void EventTransitionFinished();

    private event EventTransitionFinished OnTransitionFinished;

    private SceneLoader sceneLoader;
    void Start()
    {
        animator = GetComponent<Animator>();
    }


    public void FadeToOut(EventTransitionFinished OnEvent)
    {
        OnTransitionFinished = OnEvent;
        animator.SetTrigger("FadeOut");
    }

    public void OnFadeComplete()
    {
        if (OnTransitionFinished != null)
        {
            OnTransitionFinished();
        }
    }
}
