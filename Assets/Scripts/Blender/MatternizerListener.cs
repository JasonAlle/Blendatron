using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "MatternizerListener")]
public class MatternizerListener : ScriptableObject
{
    [SerializeField]
    private int matter;

    private void OnEnable()
    {
        matter = 0;
    }
    public event Action<int> MatterIncreaseEvent;

    public void OnMatterIncrease(int amount)
    {
        matter += amount;
        MatterIncreaseEvent?.Invoke(matter);
    }
}
