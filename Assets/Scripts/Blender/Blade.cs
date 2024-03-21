using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blade : MonoBehaviour
{
    [SerializeField]
    private ScoreListener scoreListener;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Checking Collision with: " + collision.gameObject.name);
        if (!collision.gameObject.CompareTag("HighOrb"))
        {
            return;
        }
        Orb orbcollision = collision.gameObject.GetComponent<Orb>();
        if (orbcollision.IsBlended)
            return;
        orbcollision.IsBlended = true;
        int score = orbcollision.Data.Score;
        Debug.Log("Score From Data Added: " + score);
        scoreListener.OnScoreIncrease(score);
        Debug.Log("Orb score taken!");
        OrbManager.RemoveOrb(orbcollision);
        Destroy(orbcollision.gameObject);
    }
}
