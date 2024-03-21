using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MatternizerController : MonoBehaviour
{
    [SerializeField]
    private MatternizerListener matterListener;
    [SerializeField]
    private Slider slider;
    [SerializeField]
    private StateListner stateListner;
    private void OnEnable()
    {
        slider.value = 0;
        slider.maxValue = matterListener.MaxMatter;
        matterListener.MatterIncreaseEvent += HandleMatterIncrease;
    }
    private void OnDisable()
    {
        matterListener.MatterIncreaseEvent -= HandleMatterIncrease;
    }
    private void HandleMatterIncrease(int amount)
    {
        //Change Amount
        slider.value = amount;
        if (slider.value >= slider.maxValue)
        {
            Debug.Log("Entering Blend Mode!");
        stateListner.OnBlendState();
        }
    }
}
