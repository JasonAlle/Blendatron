using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dropper : MonoBehaviour
{
    [SerializeField]
     OrbSpawner spawner;
    [SerializeField]
    private Controls_Listener input;
    [SerializeField]
    private StateListner stateListner;
    private bool isDropping;
    private Orb orbToDrop;
    private float spawnOrbTimer = 0.0f;
    bool isDisabled = false;

    private void OnEnable()
    {
        input.DropEvent += HandleDrop;
        stateListner.BlendStateEvent += HandleBlend;
        stateListner.GameplayStateEvent += HandleGame;
    }
    private void OnDisable()
    {
        input.DropEvent -= HandleDrop;
        stateListner.BlendStateEvent -= HandleBlend;
        stateListner.GameplayStateEvent -= HandleGame;

    }
    private void HandleBlend()
    {
        isDisabled = true;
    }
    private void HandleGame()
    {
        isDisabled = false;
    }
    private void Start()
    {
        spawner.PickOrb();
        FillDropper();
    }
    private void HandleDrop()
    {
        if (isDisabled)
            return;
        isDropping = true;
        orbToDrop.transform.SetParent(null);
        orbToDrop.DropTheFucker();
        spawner.PickOrb();
        
    }
    private void FillDropper()
    {
        orbToDrop = spawner.Spawn();
        orbToDrop.transform.SetPositionAndRotation(this.transform.position, Quaternion.identity);
        orbToDrop.transform.SetParent(this.gameObject.transform);
        
    }
    private void Update()
    {
        if (isDropping == true)
        {
            if (spawnOrbTimer >= 1.0f)
            {
                spawnOrbTimer = 0.0f;
                isDropping = false;
                FillDropper();
            }
            else
            {
                spawnOrbTimer += Time.deltaTime;
            }
        }
    }
}
