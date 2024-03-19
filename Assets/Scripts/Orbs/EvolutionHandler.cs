using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvolutionHandler : MonoBehaviour
{
    [SerializeField]
    OrbSpawner spawner;
    [SerializeField]
    private List<OrbData> orbs = new List<OrbData>();
    private static EvolutionHandler Instance = default;
    public static event System.Action<Vector2, OrbTiers> EvolutionEvent;
    public static event System.Action EndGameEvent;
    private float pointResetTimer = 0.0f;
    private Vector2 pointHappenedAlready;
    private Vector2 pointResetPoint;
    [SerializeField]
    private float baseLiftAmount = .2f;
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
        if (tier == OrbTiers.tier8)
            return;
        if (Instance.pointHappenedAlready == point)
            return;
        Instance.pointHappenedAlready = point;
        float liftAmount = Instance.baseLiftAmount + ((int)tier / 2);
        Vector3 spawnPos = new Vector3(point.x, point.y + liftAmount, 0.0f);
        Orb orbSpawn = Instance.spawner.SpawnSpecificOrb(Instance.orbs[((int)tier + 1)], spawnPos, Quaternion.identity);
        //Tell Orb it lives through evolution
        orbSpawn.HasEvolved();
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
