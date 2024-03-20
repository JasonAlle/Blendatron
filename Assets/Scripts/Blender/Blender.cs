using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blender : MonoBehaviour
{
    [SerializeField]
    private StateListner stateListener;
    [SerializeField]
    private BoxCollider2D blenderTrigger;
    [SerializeField]
    private ScoreListener scoreListener;

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
        //Play animation
        //Swap collision to trigger
        blenderTrigger.enabled = true;
        //Play sound
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Orb"))
        {
            return;
        }
        else
        {
            Orb orbcollision = collision.gameObject.GetComponentInChildren<Orb>();
            if (orbcollision.Data.Tier < OrbTiers.tier6)
            {
                return;
            }
            else
            {
                int score = orbcollision.Data.Score;
                scoreListener.OnScoreIncrease(score);
            }
        }
    }
}
