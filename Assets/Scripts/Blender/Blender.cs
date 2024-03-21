using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blender : MonoBehaviour
{
    [SerializeField]
    private GameObject highBladeObject;
    [SerializeField]
    private GameObject midBladeObject;
    [SerializeField]
    private GameObject lowBladeObject;
    [SerializeField]
    private ScoreListener scoreListener;
    [SerializeField]
    private float highBladeSpeed;
    [SerializeField]     
    private float midBladeSpeed;
    [SerializeField]      
    private float lowBladeSpeed;

    private bool isBlending = false;


    private void OnEnable()
    {
        isBlending = false;
        //Play animation
        //Turn on collision
        Debug.Log("Starting Blender!!!");
        //Play sound
        //After animation
        isBlending = true;
    }
    private void OnDisable()
    {

    }
    private void FixedUpdate()
    {
        if (isBlending == false)
            return;
        highBladeObject.transform.Rotate(Vector3.up, highBladeSpeed * Time.fixedDeltaTime);
        midBladeObject.transform.Rotate(Vector3.up, midBladeSpeed * Time.fixedDeltaTime);
        lowBladeObject.transform.Rotate(Vector3.up, lowBladeSpeed * Time.fixedDeltaTime);

    }
}
