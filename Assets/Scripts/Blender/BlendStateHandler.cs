using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    private bool isBlend = false;
    [SerializeField]
    private TextMeshProUGUI timerVisualizer;
    private string cleanTimerVisualizer;
    private void OnEnable()
    {
        cleanTimerVisualizer = "Blender Timer:\n{blender}";
        blenderTimer = blendTime;
        stateListner.BlendStateEvent += HandleBlend;
        SetCleanTimerVisual();
        blenderTimer = 0.0f;
    }
    private void OnDisable()
    {
        stateListner.BlendStateEvent -= HandleBlend;

    }
    private void SetCleanTimerVisual()
    {
        timerVisualizer.text = cleanTimerVisualizer;
    }
    private void UpdateTimerVisual()
    {
        Debug.Log("Updating visualizer!");
        timerVisualizer.text = timerVisualizer.text.Replace("{blender}", blenderTimer.ToString());
    }
    private void HandleBlend()
    {
        Debug.Log("Blender started!");
        isBlend = true;
        lid = Instantiate(lidObject);
        blender = Instantiate(blenderObject);
        //shaker = Instantiate(shakerObject);
    }
    private void FixedUpdate()
    {
        if (!isBlend)
            return;
        if (blenderTimer >= blendTime)
        {
            isBlend = false;
            blenderTimer = 0.0f;
            Destroy(blender.gameObject);
            Destroy(lid.gameObject);
            Debug.Log("Blender is finished and returning to gameplay!");
            stateListner.OnGameplayState();
            SetCleanTimerVisual();
        }
        else
        {
            Debug.Log("Blender timer is running: " + blenderTimer);
            blenderTimer += Time.fixedDeltaTime;
            UpdateTimerVisual();
        }
    }
}
