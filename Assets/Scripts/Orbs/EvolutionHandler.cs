using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvolutionHandler : MonoBehaviour
{
    //[SerializeField]
    //EvolutionListener evoListner;
    [SerializeField]
    private List<Orb> Orbs = new List<Orb>();
    private static EvolutionHandler Instance = default;
    public static event System.Action<Vector2, OrbTiers> EvolutionEvent;
    public static event System.Action EndGameEvent;
    private float pointResetTimer = 0.0f;
    private Vector2 pointHappenedAlready;
    private Vector2 pointResetPoint;
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        pointResetPoint = new Vector2(-10000.0f, -10000.0f);
    }
    public static void Evolve(Vector2 point, OrbTiers tier)
    {
        if (Instance.pointHappenedAlready == point)
            return;
        Instance.pointHappenedAlready = point;
        Orb orbSpawn = Instantiate(Instance.Orbs[((int)tier + 1)], new Vector3(point.x, point.y, 0.0f), Quaternion.identity);
        orbSpawn.DropTheFucker();
        EvolutionEvent?.Invoke(point, tier);
        if (orbSpawn.Data.Tier >= OrbTiers.tier7)
        {
            EndGameEvent?.Invoke();
        }
    }
    private void Update()
    {
        if (pointHappenedAlready.x != -10000.0f)
        {
            pointResetTimer += Time.deltaTime;
        }
        if (pointResetTimer >= 1.0f)
        {
            pointHappenedAlready = pointResetPoint;
            pointResetTimer = 0.0f;
        }
    }
}
