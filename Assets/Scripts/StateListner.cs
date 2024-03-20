using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StateListener")]
public class StateListner : ScriptableObject
{
    public event Action GameplayStateEvent;
    public event Action GameOverStateEvent;
    public event Action BlendStateEvent;
    public event Action MenuStateEvent;
    public event Action LateGameStateEvent;

    public void OnGameplayState()
    {
        GameplayStateEvent?.Invoke();
    }
    public void OnGameOverState()
    {
        GameOverStateEvent?.Invoke();
    }
    public void OnBlendState()
    {
        BlendStateEvent?.Invoke();
    }
    public void OnMenuState()
    {
        MenuStateEvent?.Invoke();
    }
    public void OnLateGameState()
    {
        LateGameStateEvent?.Invoke();
    }
}
