using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlendStateHandler : MonoBehaviour
{
    [SerializeField]
    private StateListner stateListner;
    [SerializeField]
    private Blender blenderObject;
    private Blender blender;
    [SerializeField]
    private Lid lidObject;
    private Lid lid;
    //[SerializeField]
   // private Shaker shakerObject;
    //private Shaker shaker;
    [SerializeField]
    private float blendTime;
    private float blenderTimer;
    private void OnEnable()
    {
        blenderTimer = blendTime;
        stateListner.BlendStateEvent += HandleBlend;
    }
    private void OnDisable()
    {
        stateListner.BlendStateEvent -= HandleBlend;

    }
    private void HandleBlend()
    {
        lid = Instantiate(lidObject);
        blender = Instantiate(blenderObject);
        //shaker = Instantiate(shakerObject);
    }
}
