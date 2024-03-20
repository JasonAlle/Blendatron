using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lid : MonoBehaviour
{
    [SerializeField]
    private StateListner stateListener;
    private void OnEnable()
    {
        stateListener.BlendStateEvent += HandleBlend;
    }
    private void OnDisable()
    {
        stateListener.BlendStateEvent -= HandleBlend;

    }
    private void HandleBlend()
    {
        
    }
}
